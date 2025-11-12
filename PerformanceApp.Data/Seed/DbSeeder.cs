using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using PerformanceApp.Data.Models;
using PerformanceApp.Data.Context;
using PerformanceApp.Data.Repositories;

namespace PerformanceApp.Data.Seed;



public class Seeder(PadbContext context, UserManager<ApplicationUser> userManager)
{
    private readonly PadbContext _context = context;
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly PortfolioRepository _portfolioRepository = new(context);
    private readonly BenchmarkRepository _benchmarkRepository = new(context);
    private readonly TransactionTypeRepository _transactionTypeRepository = new(context);
    private static ApplicationUser ToUser(string username) => new() { UserName = username };

    private bool UserExists(string username) => _userManager.FindByNameAsync(username).Result != null;

    private void SeedUsers()
    {
        var usernamesPasswords = new List<(string, string)>
        {
            ("User A", "Password A"),
            ("User B", "Password B")
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

    private ApplicationUser GetUser(string username) => _userManager.FindByNameAsync(username).Result!;
    private static Portfolio MapToPortfolio(string name, ApplicationUser user) => new Portfolio { PortfolioName = name, UserID = user!.Id };

    private void SeedPortfolios()
    {
        var userA = GetUser("User A");
        var userB = GetUser("User B");

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

    private void SeedBenchmarks()
    {
        var mappingsRaw = new List<(string, string)>
        {
            ("Portfolio A", "Benchmark A"),
            ("Portfolio B", "Benchmark B")
        };
        (Portfolio, Portfolio) ToPortfolios((string, string) pair) => (
            _portfolioRepository.GetPortfolio(pair.Item1)!,
            _portfolioRepository.GetPortfolio(pair.Item2)!
        );
        Benchmark ToBenchmark((Portfolio, Portfolio) pair)
            => new Benchmark { PortfolioId = pair.Item1.PortfolioId, BenchmarkId = pair.Item2.PortfolioId };


        var mappings = mappingsRaw.Select(ToPortfolios)
            .Select(ToBenchmark)
            .ToList();


        _benchmarkRepository.AddBenchmarkMappings(mappings);

        _context.SaveChanges();
    }

    private void SeedTransactionTypes()
    {
        TransactionType Map(string name) => new TransactionType { TransactionTypeName = name };

        var raw = new List<string> { "Buy", "Sell" };

        var transactionTypes = raw.Select(Map).ToList();

        _transactionTypeRepository.AddTransactionTypes(transactionTypes);

        _context.SaveChanges();
    }

    public void Seed()
    {
        SeedUsers();
        SeedPortfolios();
        SeedBenchmarks();
        SeedTransactionTypes();
    }

}