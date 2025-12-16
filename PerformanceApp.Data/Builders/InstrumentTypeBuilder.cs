using PerformanceApp.Data.Builders.Defaults;
using PerformanceApp.Data.Builders.Interface;
using PerformanceApp.Data.Models;

namespace PerformanceApp.Data.Builders;

public class InstrumentTypeBuilder : IBuilder<InstrumentType>
{
    private int _id = InstrumentTypeBuilderDefaults.Id;
    private string _name = InstrumentTypeBuilderDefaults.Name;

    public InstrumentTypeBuilder WithId(int id)
    {
        _id = id;
        return this;
    }

    public InstrumentTypeBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    public InstrumentType Build()
    {
        return new InstrumentType
        {
            Id = _id,
            Name = _name
        };
    }

    public InstrumentType Clone()
    {
        return new InstrumentTypeBuilder()
            .WithId(_id)
            .WithName(_name)
            .Build();
    }

    public IEnumerable<InstrumentType> Many(int count)
    {
        for (int i = 0; i < count; i++)
        {
            yield return new InstrumentTypeBuilder()
                .WithId(_id + i)
                .WithName($"{_name} {i + 1}")
                .Build();
        }
    }
}