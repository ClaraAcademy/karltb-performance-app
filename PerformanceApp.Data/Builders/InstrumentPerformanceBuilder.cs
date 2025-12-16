using PerformanceApp.Data.Builders.Defaults;
using PerformanceApp.Data.Builders.Interface;
using PerformanceApp.Data.Models;

namespace PerformanceApp.Data.Builders;

public class InstrumentPerformanceBuilder : IBuilder<InstrumentPerformance>
{
    private int _instrumentId = InstrumentPerformanceBuilderDefaults.InstrumentId;
    private int _typeId = InstrumentPerformanceBuilderDefaults.TypeId;
    private DateOnly _periodStart = InstrumentPerformanceBuilderDefaults.PeriodStart;
    private DateOnly _periodEnd = InstrumentPerformanceBuilderDefaults.PeriodEnd;
    private decimal _value = InstrumentPerformanceBuilderDefaults.Value;

    public InstrumentPerformanceBuilder WithInstrumentId(int instrumentId)
    {
        _instrumentId = instrumentId;
        return this;
    }

    public InstrumentPerformanceBuilder WithTypeId(int typeId)
    {
        _typeId = typeId;
        return this;
    }

    public InstrumentPerformanceBuilder WithPeriodStart(DateOnly periodStart)
    {
        _periodStart = periodStart;
        return this;
    }

    public InstrumentPerformanceBuilder WithPeriodEnd(DateOnly periodEnd)
    {
        _periodEnd = periodEnd;
        return this;
    }

    public InstrumentPerformanceBuilder WithValue(decimal value)
    {
        _value = value;
        return this;
    }

    public InstrumentPerformance Build()
    {
        return new InstrumentPerformance
        {
            InstrumentId = _instrumentId,
            TypeId = _typeId,
            PeriodStart = _periodStart,
            PeriodEnd = _periodEnd,
            Value = _value
        };
    }

    public InstrumentPerformance Clone()
    {
        return new InstrumentPerformanceBuilder()
            .WithInstrumentId(_instrumentId)
            .WithTypeId(_typeId)
            .WithPeriodStart(_periodStart)
            .WithPeriodEnd(_periodEnd)
            .WithValue(_value)
            .Build();
    }

    public IEnumerable<InstrumentPerformance> Many(int count)
    {
        for (int i = 0; i < count; i++)
        {
            yield return new InstrumentPerformanceBuilder()
                .WithInstrumentId(_instrumentId + i)
                .WithTypeId(_typeId)
                .WithPeriodStart(_periodStart.AddDays(i))
                .WithPeriodEnd(_periodEnd.AddDays(i))
                .WithValue(_value + i)
                .Build();
        }
    }
}