using PerformanceApp.Data.Builders.Defaults;
using PerformanceApp.Data.Builders.Interface;
using PerformanceApp.Data.Models;

namespace PerformanceApp.Data.Builders;

public class InstrumentPerformanceBuilder : IBuilder<InstrumentPerformance>
{
    private DateInfo _periodStartNavigation = InstrumentPerformanceBuilderDefaults.PeriodStartNavigation;
    private DateInfo _periodEndNavigation = InstrumentPerformanceBuilderDefaults.PeriodEndNavigation;
    private decimal _value = InstrumentPerformanceBuilderDefaults.Value;
    private Instrument _instrumentNavigation = InstrumentPerformanceBuilderDefaults.InstrumentNavigation;
    private PerformanceType _performanceTypeNavigation = InstrumentPerformanceBuilderDefaults.PerformanceTypeNavigation;

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
            Value = _value,
            InstrumentNavigation = _instrumentNavigation,
            PerformanceTypeNavigation = _performanceTypeNavigation
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
            ).Build();
            var periodEnd = new DateInfoBuilder().WithBankday(
                _periodEndNavigation.Bankday.AddDays(i)
            ).Build();
            yield return new InstrumentPerformanceBuilder()
                .WithPeriodStart(periodStart)
                .WithPeriodEnd(periodEnd)
                .WithValue(_value + i)
                .WithInstrumentNavigation(_instrumentNavigation)
                .WithPerformanceTypeNavigation(_performanceTypeNavigation)
                .Build();
        }
    }
}