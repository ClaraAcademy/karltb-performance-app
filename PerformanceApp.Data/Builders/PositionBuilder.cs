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
    private Instrument _instrumentNavigation = PositionBuilderDefaults.InstrumentNavigation;
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
        _instrumentNavigation = new InstrumentBuilder().WithId(instrumentId).Build();
        return this;
    }
    public PositionBuilder WithPortfolioId(int portfolioId)
    {
        _portfolioId = portfolioId;
        _portfolioNavigation = new PortfolioBuilder().WithId(portfolioId).Build();
        return this;
    }
    public PositionBuilder WithBankday(DateOnly bankday)
    {
        _bankday = bankday;
        _bankdayNavigation = new DateInfoBuilder().WithBankday(bankday).Build();
        return this;
    }
    public PositionBuilder WithInstrumentNavigation(Instrument instrument)
    {
        _instrumentNavigation = instrument;
        _instrumentId = instrument.Id;
        return this;
    }
    public PositionBuilder WithPortfolioNavigation(Portfolio portfolio)
    {
        _portfolioNavigation = portfolio;
        _portfolioId = portfolio.Id;
        return this;
    }
    public PositionBuilder WithBankdayNavigation(DateInfo dateInfo)
    {
        _bankdayNavigation = dateInfo;
        _bankday = dateInfo.Bankday;
        return this;
    }
    public PositionBuilder WithPositionValuesNavigation(List<PositionValue> positionValues)
    {
        _positionValuesNavigation = positionValues;
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
            .Build();
    }

    public IEnumerable<Position> Many(int count)
    {
        for (int i = 0; i < count; i++)
        {
            yield return new PositionBuilder()
                .WithId(_id + i)
                .WithInstrumentId(_instrumentId)
                .WithPortfolioId(_portfolioId)
                .WithBankday(_bankday.AddDays(i))
                .WithInstrumentNavigation(_instrumentNavigation)
                .WithPortfolioNavigation(_portfolioNavigation)
                .WithBankdayNavigation(_bankdayNavigation)
                .WithPositionValuesNavigation(
                    [
                        new PositionValueBuilder()
                            .WithBankday(_bankday.AddDays(i))
                            .WithId(_id + i)
                            .Build()
                    ]
                )
                .Build();
        }
    }
}