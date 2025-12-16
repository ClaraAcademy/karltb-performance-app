using PerformanceApp.Data.Builders.Defaults;
using PerformanceApp.Data.Builders.Interface;
using PerformanceApp.Data.Models;

namespace PerformanceApp.Data.Builders;

public class PositionValueBuilder : IBuilder<PositionValue>
{
    private int _id = PositionValueBuilderDefaults.Id;
    private DateOnly _bankday = PositionValueBuilderDefaults.Bankday;
    private decimal? _value = PositionValueBuilderDefaults.Value;

    public PositionValueBuilder WithId(int id)
    {
        _id = id;
        return this;
    }

    public PositionValueBuilder WithBankday(DateOnly bankday)
    {
        _bankday = bankday;
        return this;
    }

    public PositionValueBuilder WithValue(decimal? value)
    {
        _value = value;
        return this;
    }

    public PositionValue Build()
    {
        return new PositionValue
        {
            PositionId = _id,
            Bankday = _bankday,
            Value = _value,
        };
    }

    public PositionValue Clone()
    {
        return new PositionValueBuilder()
            .WithId(_id)
            .WithBankday(_bankday)
            .WithValue(_value)
            .Build();
    }

    public IEnumerable<PositionValue> Many(int count)
    {
        for (int i = 0; i < count; i++)
        {
            yield return new PositionValueBuilder()
                .WithId(_id + i)
                .WithBankday(_bankday.AddDays(i))
                .WithValue(_value)
                .Build();
        }
    }
}