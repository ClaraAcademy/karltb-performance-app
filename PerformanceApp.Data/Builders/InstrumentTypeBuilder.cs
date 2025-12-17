using PerformanceApp.Data.Builders.Defaults;
using PerformanceApp.Data.Builders.Interface;
using PerformanceApp.Data.Models;

namespace PerformanceApp.Data.Builders;

public class InstrumentTypeBuilder : IBuilder<InstrumentType>
{
    private string _name = InstrumentTypeBuilderDefaults.Name;
    private List<Instrument> _instruments = [];

    public InstrumentTypeBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    public InstrumentTypeBuilder WithInstrument(Instrument instrument)
    {
        _instruments.Add(instrument);
        return this;
    }

    public InstrumentTypeBuilder WithInstruments(IEnumerable<Instrument> instruments)
    {
        _instruments.AddRange(instruments);
        return this;
    }

    public InstrumentType Build()
    {
        return new InstrumentType
        {
            Name = _name,
            InstrumentsNavigation = _instruments
        };
    }

    public InstrumentType Clone()
    {
        return new InstrumentTypeBuilder()
            .WithName(_name)
            .WithInstruments(_instruments)
            .Build();
    }

    public IEnumerable<InstrumentType> Many(int count)
    {
        for (int i = 0; i < count; i++)
        {
            yield return new InstrumentTypeBuilder()
                .WithName($"{_name} {i + 1}")
                .WithInstruments(_instruments)
                .Build();
        }
    }
}