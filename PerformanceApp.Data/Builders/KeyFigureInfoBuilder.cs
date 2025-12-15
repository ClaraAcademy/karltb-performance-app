using PerformanceApp.Data.Builders.Defaults;
using PerformanceApp.Data.Builders.Interface;
using PerformanceApp.Data.Models;

namespace PerformanceApp.Data.Builders;

public class KeyFigureInfoBuilder : IBuilder<KeyFigureInfo>
{ 
    private int _id = KeyFigureInfoBuilderDefaults.Id;
    private string _name = KeyFigureInfoBuilderDefaults.Name;

    public KeyFigureInfoBuilder WithId(int id)
    {
        _id = id;
        return this;
    }

    public KeyFigureInfoBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    public KeyFigureInfo Clone()
    {
        return new KeyFigureInfoBuilder()
            .WithId(_id)
            .WithName(_name)
            .Build();
    }

    public IEnumerable<KeyFigureInfo> Many(int count)
    {
        return Enumerable.Range(1, count).Select(i => new KeyFigureInfoBuilder()
            .WithId(_id + i - 1)
            .WithName($"{_name} {i}")
            .Build());
        
    }

    public KeyFigureInfo Build()
    {
        return new KeyFigureInfo
        {
            Id = _id,
            Name = _name
        };
    }

}