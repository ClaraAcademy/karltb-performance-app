using PerformanceApp.Data.Models;
using PerformanceApp.Data.Builders.Interface;
using PerformanceApp.Data.Builders.Defaults;

namespace PerformanceApp.Data.Builders;

public class PositionBuilder : IBuilder<Position>
{
    protected Instrument _instrument = new InstrumentBuilder().Build();
    private Portfolio _portfolio = new PortfolioBuilder().Build();
    private DateInfo _dateInfo = new DateInfoBuilder().Build();
    private List<PositionValue> _positionValues = new PositionValueBuilder().Many(1).ToList();
    protected decimal? _amount = null;
    protected int? _count = null;
    protected decimal? _proportion = null;
    protected decimal? _nominal = null;


    public PositionBuilder WithInstrument(Instrument instrument)
    {
        _instrument = instrument;
        return this;
    }
    public PositionBuilder WithPortfolio(Portfolio portfolio)
    {
        _portfolio = portfolio;
        return this;
    }
    public PositionBuilder WithDateInfo(DateInfo dateInfo)
    {
        _dateInfo = dateInfo;
        return this;
    }

    public PositionBuilder WithBankday(DateOnly bankday)
    {
        _dateInfo = new DateInfoBuilder()
            .WithBankday(bankday)
            .Build();
        return this;
    }

    public PositionBuilder WithPositionValues(List<PositionValue> positionValues)
    {
        _positionValues = positionValues;
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
            InstrumentNavigation = _instrument,
            PortfolioNavigation = _portfolio,
            BankdayNavigation = _dateInfo,
            Amount = _amount,
            Count = _count,
            Proportion = _proportion,
            Nominal = _nominal,
            PositionValuesNavigation = _positionValues
        };
    }

    public Position Clone()
    {
        return new PositionBuilder()
            .WithInstrument(_instrument)
            .WithPortfolio(_portfolio)
            .WithDateInfo(_dateInfo)
            .WithPositionValues(_positionValues)
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
            var instrument = new InstrumentBuilder()
                .Build();
            var portfolio = new PortfolioBuilder()
                .Build();
            var dateInfo = new DateInfoBuilder()
                .WithBankday(_dateInfo.Bankday.AddDays(i + 1))
                .Build();

            yield return new PositionBuilder()
                .WithDateInfo(dateInfo)
                .WithInstrument(instrument)
                .WithPortfolio(portfolio)
                .WithDateInfo(dateInfo)
                .WithPositionValues(
                    [
                        new PositionValueBuilder()
                            .WithBankday(dateInfo.Bankday)
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