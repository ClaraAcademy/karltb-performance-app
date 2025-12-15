using PerformanceApp.Data.Builders.Defaults;
using PerformanceApp.Data.Builders.Interface;
using PerformanceApp.Data.Models;

namespace PerformanceApp.Data.Builders;

public class PerformanceTypeBuilder : IBuilder<PerformanceType>
{
    private int _id = PerformanceTypeBuilderDefaults.Id;
    private string _name = PerformanceTypeBuilderDefaults.Name;

    public PerformanceTypeBuilder WithId(int id)
    {
        _id = id;
        return this;
    }

    public PerformanceTypeBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    public PerformanceType Build()
    {
        return new PerformanceType
        {
            Id = _id,
            Name = _name
        };
    }

    public PerformanceType Clone()
    {
        return new PerformanceTypeBuilder()
            .WithId(_id)
            .WithName(_name)
            .Build();
    }

    public IEnumerable<PerformanceType> Many(int count)
    {
        for (int i = 1; i <= count; i++)
        {
            yield return new PerformanceTypeBuilder()
                .WithId(i)
                .WithName($"Performance Type {i}")
                .Build();
        }
    }
}