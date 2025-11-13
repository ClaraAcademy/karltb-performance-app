using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using PerformanceApp.Data.Models;
using PerformanceApp.Data.Context;
using PerformanceApp.Data.Repositories;
using DocumentFormat.OpenXml.Drawing.Charts;
using Microsoft.EntityFrameworkCore;

namespace PerformanceApp.Data.Seeding;

public class DatabaseSeeder(PadbContext context, UserManager<ApplicationUser> userManager)
{
    private readonly FileInfo DefaultFile = new(@"C:\Data\Priser - portföljberäkning.xlsx");
    private readonly PadbContext _context = context;
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly PortfolioRepository _portfolioRepository = new(context);
    private readonly BenchmarkRepository _benchmarkRepository = new(context);
    private readonly TransactionTypeRepository _transactionTypeRepository = new(context);
    private readonly KeyFigureInfoRepository _keyFigureInfoRepository = new(context);
    private readonly StagingRepository _stagingRepository = new(context);
    private readonly DateInfoRepository _dateInfoRepository = new(context);
    private readonly InstrumentTypeRepository _instrumentTypeRepository = new(context);
    private readonly InstrumentRepository _instrumentRepository = new(context);
    private readonly InstrumentPriceRepository _instrumentPriceRepository = new(context);
    private static ApplicationUser ToUser(string username) => new() { UserName = username };

    private bool UserExists(string username) => _userManager.FindByNameAsync(username).Result != null;

    private void SeedUsers()
    {
        var usernamesPasswords = new List<(string, string)>
        {
            ("UserA", "Password123!"),
            ("UserB", "Password123!")
        };

        foreach ((var username, var password) in usernamesPasswords)
        {
            if (!UserExists(username))
            {
                var result = _userManager.CreateAsync(ToUser(username), password).Result;
                if (!result.Succeeded)
                {
                    throw new Exception($"Failed to create user {username}: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                }
            }
        }
        _context.SaveChanges();
    }

    private ApplicationUser GetUser(string username) => _userManager.FindByNameAsync(username).Result;
    private static Portfolio MapToPortfolio(string name, ApplicationUser user) => new Portfolio { PortfolioName = name, UserID = user!.Id };

    private void SeedPortfolios()
    {
        var userA = GetUser("UserA");
        var userB = GetUser("UserB");

        var portfolios = new List<Portfolio>
        {
            MapToPortfolio("Portfolio A", userA!),
            MapToPortfolio("Benchmark A", userA!),
            MapToPortfolio("Portfolio B", userB!),
            MapToPortfolio("Benchmark B", userB!),
        };

        _portfolioRepository.AddPortfolios(portfolios);

        _context.SaveChanges();
    }
    private Benchmark MapToBenchmark((string, string) pair)
    {
        var portfolio = _portfolioRepository.GetPortfolio(pair.Item1)!;
        var benchmark = _portfolioRepository.GetPortfolio(pair.Item2)!;

        return new Benchmark { PortfolioId = portfolio.PortfolioId, BenchmarkId = benchmark.PortfolioId };
    }

    private void SeedBenchmarks()
    {
        List<string> portfolios = ["Portfolio A", "Portfolio B"];
        List<string> benchmarks = ["Benchmark A", "Benchmark B"];

        var benchmarkMappings = portfolios.Zip(benchmarks)
            .Select(MapToBenchmark)
            .ToList();

        _benchmarkRepository.AddBenchmarkMappings(benchmarkMappings);

        _context.SaveChanges();
    }
    TransactionType MapToTransactionType(string name) => new TransactionType { TransactionTypeName = name };

    private void SeedTransactionTypes()
    {
        var raw = new List<string> { "Buy", "Sell" };

        var transactionTypes = raw.Select(MapToTransactionType)
            .ToList();

        _transactionTypeRepository.AddTransactionTypes(transactionTypes);

        _context.SaveChanges();
    }

    KeyFigureInfo MapToKeyFigureInfo(string name) => new KeyFigureInfo { KeyFigureName = name };

    private void SeedKeyFigures()
    {
        var raw = new List<string>
        {
            "Standard Deviation",
            "Tracking Error",
            "Annualised Cumulative Return",
            "Information Ratio",
            "Half-Year Performance"
        };

        var keyFigureInfos = raw.Select(MapToKeyFigureInfo)
            .ToList();

        _keyFigureInfoRepository.AddKeyFigureInfos(keyFigureInfos);

        _context.SaveChanges();
    }

    public void SeedStagings(string? filepath = null)
    {
        var file = new FileInfo(filepath ?? DefaultFile.FullName);
        var stagings = ExcelReader.ReadExcel(file);

        _stagingRepository.AddStagings(stagings);

        _context.SaveChanges();
    }

    DateInfo MapToDateInfo(DateOnly bankday) => new DateInfo { Bankday = bankday };

    public void SeedDateInfos()
    {
        var dateInfos = _stagingRepository.GetStagings()
            .Select(s => s.Bankday)
            .OfType<DateOnly>()
            .Select(MapToDateInfo)
            .ToList();

        _dateInfoRepository.AddDateInfos(dateInfos);

        _context.SaveChanges();
    }

    InstrumentType MapToInstrumentType(string name) => new InstrumentType { InstrumentTypeName = name };
    public void SeedInstrumentTypes()
    {
        var instrumentTypes = _stagingRepository.GetStagings()
            .Select(s => s.InstrumentType)
            .OfType<string>()
            .Select(MapToInstrumentType)
            .ToList();

        _instrumentTypeRepository.AddInstrumentTypes(instrumentTypes);

        _context.SaveChanges();
    }

    public void SeedInstruments()
    {
        var stagings = _stagingRepository.GetStagings();

        var instrumentTypeNames = stagings
            .Select(s => s.InstrumentType)
            .OfType<string>()
            .Distinct()
            .ToList();

        var instrumentTypes = _instrumentTypeRepository.GetInstrumentTypes(instrumentTypeNames);
        var instruments = stagings
            .Select(s => new { s.InstrumentName, s.InstrumentType })
            .Distinct()
            .Join(
                instrumentTypes,
                s => s.InstrumentType,
                it => it.InstrumentTypeName,
                (s, it) => new Instrument
                {
                    InstrumentName = s.InstrumentName,
                    InstrumentTypeId = it.InstrumentTypeId
                }
            ).ToList();

        _instrumentRepository.AddInstruments(instruments);

        _context.SaveChanges();
    }

    public void SeedInstrumentPrices()
    {
        var instruments = _instrumentRepository.GetInstruments();

        var instrumentPrices = _stagingRepository.GetStagings()
            .Where(s => s.Price != null)
            .Join(
                instruments,
                s => s.InstrumentName,
                i => i.InstrumentName,
                (s, i) => new InstrumentPrice
                {
                    InstrumentId = i.InstrumentId,
                    Price = s.Price!.Value
                }
            ).ToList();

        _instrumentPriceRepository.AddInstrumentPrices(instrumentPrices);

        _context.SaveChanges();
    }
    private static FormattableString GetBuyQuery(
        string portfolioName,
        string instrumentName,
        DateOnly date,
        int? count = null,
        decimal? amount = null,
        decimal? proportion = null,
        decimal? nominal = null
    )
    {
        return $@"EXEC [padb].[uspBuyInstrument]
            @PortfolioName = {portfolioName},
            @InstrumentName = {instrumentName},
            @Count = {count},
            @Amount = {amount},
            @Proportion = {proportion},
            @Nominal = {nominal},
            @BuyDate = {date}";
    }

    private List<FormattableString> GetBuyQueries()
    {
        DateOnly firstDay = _dateInfoRepository
            .GetDateInfos()
            .Select(di => di.Bankday)
            .Min();

        var portfolioA = "Portfolio A";
        var portfolioB = "Portfolio B";
        var benchmarkA = "Benchmark A";
        var benchmarkB = "Benchmark B";

        var ssabB = "SSAB B";
        var astraZeneca = "Astra Zeneca";
        var statsobligation1046 = "Statsobligation 1046";
        var omx30 = "OMX30";
        var omrXtBond = "OMRXTBOND";

        return
        [
            GetBuyQuery(portfolioA, ssabB, firstDay, count : 40000),
            GetBuyQuery(portfolioA, astraZeneca, firstDay, count : 13200),
            GetBuyQuery(portfolioB, ssabB, firstDay, count : 20000),
            GetBuyQuery(portfolioB, astraZeneca, firstDay, count : 6600),
            GetBuyQuery(portfolioB, statsobligation1046, firstDay, nominal : 5000000.0m),
            GetBuyQuery(benchmarkA, omx30, firstDay, proportion : 1.0m),
            GetBuyQuery(benchmarkB, omx30, firstDay, proportion : 0.5m),
            GetBuyQuery(benchmarkB, omrXtBond, firstDay, proportion : 0.5m)
        ];

    }

    public void SeedPositions()
    {
        var queries = GetBuyQueries();

        foreach (var q in queries)
        {
            _context.Database.ExecuteSqlInterpolated(q);
        }

        _context.SaveChanges();
    }

    private List<FormattableString> GetDailyQueries(DateOnly bankday)
    {
        return [
            $@"EXEC [padb].[uspUpdatePositions] @Bankday = {bankday};",
            $@"EXEC [padb].[uspUpdatePortfolioValue] @Bankday = {bankday};",
            $@"EXEC [padb].[uspUpdateInstrumentDayPerformance] @Bankday = {bankday};",
            $@"EXEC [padb].[uspUpdatePortfolioDayPerformance] @Bankday = {bankday};",
            $@"EXEC [padb].[uspUpdatePortfolioCumulativeDayPerformance] @Bankday = {bankday};"
        ];
    }

    private List<FormattableString> GetPerformanceQueries()
    {
        return [
            $@"EXEC padb.uspUpdatePortfolioMonthPerformance;",
            $@"EXEC padb.uspUpdatePortfolioHalfYearPerformance;",
            $@"EXEC padb.uspUpdateStandardDeviation;",
            $@"EXEC padb.uspUpdateTrackingError;",
            $@"EXEC padb.uspUpdateAnnualisedCumulativeReturn;",
            $@"EXEC padb.uspUpdateInformationRatio;",
            $@"EXEC padb.uspUpdateHalfYearPerformance;"
        ];
    }

    public void SeedPerformance()
    {
        var bankdays = _dateInfoRepository.GetDateInfos()
            .Select(di => di.Bankday)
            .OrderBy(d => d)
            .ToList();

        foreach (var bankday in bankdays)
        {
            foreach (var query in GetDailyQueries(bankday))
            {
                _context.Database.ExecuteSqlInterpolated(query);
                _context.SaveChanges();
            }
        }

        foreach (var query in GetPerformanceQueries())
        {
            _context.Database.ExecuteSqlInterpolated(query);
            _context.SaveChanges();
        }
    }

    public void SeedBaseData()
    {
        SeedStagings();
        SeedDateInfos();
        SeedInstrumentTypes();
        SeedInstruments();
    }

    public void Seed()
    {
        SeedBaseData();

        SeedUsers();
        SeedPortfolios();
        SeedBenchmarks();
        SeedTransactionTypes();
        SeedKeyFigures();
        SeedPositions();
        SeedPerformance();
    }

}