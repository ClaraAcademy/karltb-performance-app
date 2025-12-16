using PerformanceApp.Data.Models;
using PerformanceApp.Data.Builders.Interface;
using PerformanceApp.Data.Builders.Defaults;

namespace PerformanceApp.Data.Builders;

public class PositionBuilder : IBuilder<Position>
{
    private int _id = PositionBuilderDefaults.Id;
    private int _instrumentId = PositionBuilderDefaults.InstrumentId;
    private int _portfolioId = PositionBuilderDefaults.PortfolioId;
    private DateOnly _bankday = PositionBuilderDefaults.Bankday;
    protected Instrument _instrumentNavigation = PositionBuilderDefaults.InstrumentNavigation;
    private Portfolio _portfolioNavigation = PositionBuilderDefaults.PortfolioNavigation;
    private DateInfo _bankdayNavigation = PositionBuilderDefaults.BankdayNavigation;
    private List<PositionValue> _positionValuesNavigation = PositionBuilderDefaults.PositionValuesNavigation;
    protected decimal? _amount = null;
    protected int? _count = null;
    protected decimal? _proportion = null;
    protected decimal? _nominal = null;


    public PositionBuilder WithId(int id)
    {
        _id = id;
        return this;
    }
    public PositionBuilder WithInstrumentId(int instrumentId)
    {
        _instrumentId = instrumentId;
        return this;
    }
    public PositionBuilder WithPortfolioId(int portfolioId)
    {
        _portfolioId = portfolioId;
        return this;
    }
    public PositionBuilder WithBankday(DateOnly bankday)
    {
        _bankday = bankday;
        return this;
    }
    public PositionBuilder WithInstrumentNavigation(Instrument instrument)
    {
        _instrumentNavigation = instrument;
        return this;
    }
    public PositionBuilder WithPortfolioNavigation(Portfolio portfolio)
    {
        _portfolioNavigation = portfolio;
        return this;
    }
    public PositionBuilder WithBankdayNavigation(DateInfo dateInfo)
    {
        _bankday = dateInfo.Bankday;
        return this;
    }
    public PositionBuilder WithPositionValuesNavigation(List<PositionValue> positionValues)
    {
        _positionValuesNavigation = positionValues;
        return this;
    }
    public PositionBuilder WithAmount(decimal? amount)
    {
        _amount = amount;
        return this;
    }
    public PositionBuilder WithCount(int? count)
    {
        _count = count;
        return this;
    }
    public PositionBuilder WithProportion(decimal? proportion)
    {
        _proportion = proportion;
        return this;
    }
    public PositionBuilder WithNominal(decimal? nominal)
    {
        _nominal = nominal;
        return this;
    }

    public virtual Position Build()
    {
        return new Position
        {
            Id = _id,
            InstrumentId = _instrumentId,
            PortfolioId = _portfolioId,
            Bankday = _bankday,
            InstrumentNavigation = _instrumentNavigation,
            PortfolioNavigation = _portfolioNavigation,
            BankdayNavigation = _bankdayNavigation,
            Amount = _amount,
            Count = _count,
            Proportion = _proportion,
            Nominal = _nominal,
            PositionValuesNavigation = _positionValuesNavigation
        };
    }

    public Position Clone()
    {
        return new PositionBuilder()
            .WithId(_id)
            .WithInstrumentId(_instrumentId)
            .WithPortfolioId(_portfolioId)
            .WithBankday(_bankday)
            .WithInstrumentNavigation(_instrumentNavigation)
            .WithPortfolioNavigation(_portfolioNavigation)
            .WithBankdayNavigation(_bankdayNavigation)
            .WithPositionValuesNavigation(_positionValuesNavigation)
            .WithAmount(_amount)
            .WithCount(_count)
            .WithProportion(_proportion)
            .WithNominal(_nominal)
            .Build();
    }

    public IEnumerable<Position> Many(int count)
    {
        for (int i = 0; i < count; i++)
        {
            var instrument = new InstrumentBuilder().WithId(_instrumentId + i).Build();
            var portfolio = new PortfolioBuilder().WithId(_portfolioId + i).Build();
            var bankday = _bankday.AddDays(i);
            var dateInfo = new DateInfoBuilder().WithBankday(bankday).Build();

            yield return new PositionBuilder()
                .WithId(_id + i)
                .WithInstrumentId(instrument.Id)
                .WithPortfolioId(portfolio.Id)
                .WithBankday(bankday)
                .WithInstrumentNavigation(instrument)
                .WithPortfolioNavigation(portfolio)
                .WithBankdayNavigation(dateInfo)
                .WithPositionValuesNavigation(
                    [
                        new PositionValueBuilder()
                        .WithBankday(bankday)
                        .WithId(_id + i)
                        .Build()
                    ]
                )
                .WithAmount(_amount)
                .WithCount(_count)
                .WithProportion(_proportion)
                .WithNominal(_nominal)
                .Build();
        }
    }
}