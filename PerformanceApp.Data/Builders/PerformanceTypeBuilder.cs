using PerformanceApp.Data.Builders.Defaults;
using PerformanceApp.Data.Builders.Interface;
using PerformanceApp.Data.Models;

namespace PerformanceApp.Data.Builders;

public class PerformanceTypeBuilder : IBuilder<PerformanceType>
{
    private string _name = PerformanceTypeBuilderDefaults.Name;


    public PerformanceTypeBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    public PerformanceType Build()
    {
        return new PerformanceType
        {
            Name = _name
        };
    }

    public PerformanceType Clone()
    {
        return new PerformanceTypeBuilder()
            .WithName(_name)
            .Build();
    }

    public IEnumerable<PerformanceType> Many(int count)
    {
        for (int i = 1; i <= count; i++)
        {
            yield return new PerformanceTypeBuilder()
                .WithName($"Performance Type {i}")
                .Build();
        }
    }
}