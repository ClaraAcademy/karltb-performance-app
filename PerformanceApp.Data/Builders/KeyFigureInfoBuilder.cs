using PerformanceApp.Data.Builders.Defaults;
using PerformanceApp.Data.Builders.Interface;
using PerformanceApp.Data.Models;

namespace PerformanceApp.Data.Builders;

public class KeyFigureInfoBuilder : IBuilder<KeyFigureInfo>
{ 
    private string _name = KeyFigureInfoBuilderDefaults.Name;


    public KeyFigureInfoBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    public KeyFigureInfo Clone()
    {
        return new KeyFigureInfoBuilder()
            .WithName(_name)
            .Build();
    }

    public IEnumerable<KeyFigureInfo> Many(int count)
    {
        return Enumerable.Range(1, count).Select(i => new KeyFigureInfoBuilder()
            .WithName($"{_name} {i}")
            .Build());
        
    }

    public KeyFigureInfo Build()
    {
        return new KeyFigureInfo
        {
            Name = _name
        };
    }

}