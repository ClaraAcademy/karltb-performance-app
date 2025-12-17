using PerformanceApp.Data.Builders.Defaults;
using PerformanceApp.Data.Builders.Interface;
using PerformanceApp.Data.Models;

namespace PerformanceApp.Data.Builders;

public class InstrumentPerformanceBuilder : IBuilder<InstrumentPerformance>
{
    private DateInfo _periodStartNavigation = new DateInfoBuilder()
        .WithBankday(DateOnly.FromDateTime(DateTime.Now))
        .Build();
    private DateInfo _periodEndNavigation = new DateInfoBuilder()
        .WithBankday(DateOnly.FromDateTime(DateTime.Now.AddMonths(1)))
        .Build();
    private decimal _value = 100m;
    private Instrument _instrumentNavigation = new InstrumentBuilder().Build();
    private PerformanceType _performanceTypeNavigation = new PerformanceTypeBuilder().Build();

    public InstrumentPerformanceBuilder WithPeriodStart(DateInfo periodStart)
    {
        _periodStartNavigation = periodStart;
        return this;
    }

    public InstrumentPerformanceBuilder WithPeriodEnd(DateInfo periodEnd)
    {
        _periodEndNavigation = periodEnd;
        return this;
    }

    public InstrumentPerformanceBuilder WithValue(decimal value)
    {
        _value = value;
        return this;
    }

    public InstrumentPerformanceBuilder WithInstrumentNavigation(Instrument instrument)
    {
        _instrumentNavigation = instrument;
        return this;
    }

    public InstrumentPerformanceBuilder WithPerformanceTypeNavigation(PerformanceType performanceType)
    {
        _performanceTypeNavigation = performanceType;
        return this;
    }

    public InstrumentPerformance Build()
    {
        return new InstrumentPerformance
        {
            PeriodStartNavigation = _periodStartNavigation,
            PeriodEndNavigation = _periodEndNavigation,
            InstrumentNavigation = _instrumentNavigation,
            PerformanceTypeNavigation = _performanceTypeNavigation,

            Value = _value
        };
    }

    public InstrumentPerformance Clone()
    {
        return new InstrumentPerformanceBuilder()
            .WithPeriodStart(_periodStartNavigation)
            .WithPeriodEnd(_periodEndNavigation)
            .WithValue(_value)
            .WithInstrumentNavigation(_instrumentNavigation)
            .WithPerformanceTypeNavigation(_performanceTypeNavigation)
            .Build();
    }

    public IEnumerable<InstrumentPerformance> Many(int count)
    {
        for (int i = 1; i <= count; i++)
        {
            var periodStart = new DateInfoBuilder().WithBankday(
                _periodStartNavigation.Bankday.AddDays(i)
            )
            .Build();
            var periodEnd = new DateInfoBuilder().WithBankday(
                _periodEndNavigation.Bankday.AddDays(i)
            )
            .Build();
            var value = _value + i;

            yield return new InstrumentPerformanceBuilder()
                .WithPeriodStart(periodStart)
                .WithPeriodEnd(periodEnd)
                .WithValue(value)
                .WithInstrumentNavigation(_instrumentNavigation)
                .WithPerformanceTypeNavigation(_performanceTypeNavigation)
                .Build();
        }
    }
}