using PerformanceApp.Data.Builders.Defaults;
using PerformanceApp.Data.Builders.Interface;
using PerformanceApp.Data.Models;

namespace PerformanceApp.Data.Builders;

public class TransactionTypeBuilder : IBuilder<TransactionType>
{
    private string _name = TransactionTypeBuilderDefaults.Name;

    public TransactionTypeBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    public TransactionType Build()
    {
        return new TransactionType
        {
            Name = _name
        };
    }

    public TransactionType Clone()
    {
        return new TransactionTypeBuilder()
            .WithName(_name)
            .Build();
    }

    public IEnumerable<TransactionType> Many(int count)
    {
        for (int i = 0; i < count; i++)
        {
            yield return new TransactionTypeBuilder()
                .WithName($"Transaction Type {i + 1}")
                .Build();
        }
    }
}