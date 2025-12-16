using PerformanceApp.Data.Builders.Defaults;
using PerformanceApp.Data.Builders.Interface;
using PerformanceApp.Data.Models;

namespace PerformanceApp.Data.Builders;

public class InstrumentTypeBuilder : IBuilder<InstrumentType>
{
    private string _name = InstrumentTypeBuilderDefaults.Name;

    public InstrumentTypeBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    public InstrumentType Build()
    {
        return new InstrumentType
        {
            Name = _name
        };
    }

    public InstrumentType Clone()
    {
        return new InstrumentTypeBuilder()
            .WithName(_name)
            .Build();
    }

    public IEnumerable<InstrumentType> Many(int count)
    {
        for (int i = 0; i < count; i++)
        {
            yield return new InstrumentTypeBuilder()
                .WithName($"{_name} {i + 1}")
                .Build();
        }
    }
}