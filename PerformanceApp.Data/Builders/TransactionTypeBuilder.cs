using PerformanceApp.Data.Builders.Defaults;
using PerformanceApp.Data.Builders.Interface;
using PerformanceApp.Data.Models;

namespace PerformanceApp.Data.Builders;

public class TransactionTypeBuilder : IBuilder<TransactionType>
{
    private int _id = TransactionTypeBuilderDefaults.Id;
    private string _name = TransactionTypeBuilderDefaults.Name;

    public TransactionTypeBuilder WithId(int id)
    {
        _id = id;
        return this;
    }

    public TransactionTypeBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    public TransactionType Build()
    {
        return new TransactionType
        {
            Id = _id,
            Name = _name
        };
    }

    public TransactionType Clone()
    {
        return new TransactionTypeBuilder()
            .WithId(_id)
            .WithName(_name)
            .Build();
    }

    public IEnumerable<TransactionType> Many(int count)
    {
        for (int i = 0; i < count; i++)
        {
            yield return new TransactionTypeBuilder()
                .WithId(i + 1)
                .WithName($"Transaction Type {i + 1}")
                .Build();
        }
    }
}