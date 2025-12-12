using PerformanceApp.Data.Models;

namespace PerformanceApp.Server.Test.Builders;

public class KeyFigureInfoBuilder
{
    private int _id = 1;
    private string _name = "Default Key Figure";

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

    public KeyFigureInfo Build()
    {
        return new KeyFigureInfo
        {
            Id = _id,
            Name = _name
        };
    }
}