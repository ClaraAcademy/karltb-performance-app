using PerformanceApp.Data.Builders.Defaults;
using PerformanceApp.Data.Builders.Interface;
using PerformanceApp.Data.Models;

namespace PerformanceApp.Data.Builders;

public class StagingBuilder : IBuilder<Staging>
{
    private DateOnly _bankday = StagingBuilderDefaults.Bankday;
    private string _instrumentType = StagingBuilderDefaults.InstrumentType;
    private string _instrumentName = StagingBuilderDefaults.InstrumentName;
    private decimal _price = StagingBuilderDefaults.Price;

    public StagingBuilder WithBankday(DateOnly bankday)
    {
        _bankday = bankday;
        return this;
    }
    public StagingBuilder WithInstrumentType(string instrumentType)
    {
        _instrumentType = instrumentType;
        return this;
    }
    public StagingBuilder WithInstrumentName(string instrumentName)
    {
        _instrumentName = instrumentName;
        return this;
    }
    public StagingBuilder WithPrice(decimal price)
    {
        _price = price;
        return this;
    }
    public Staging Build()
    {
        return new Staging
        {
            Bankday = _bankday,
            InstrumentType = _instrumentType,
            InstrumentName = _instrumentName,
            Price = _price,
        };
    }

    public Staging Clone()
    {
        return new StagingBuilder()
            .WithBankday(_bankday)
            .WithInstrumentType(_instrumentType)
            .WithInstrumentName(_instrumentName)
            .WithPrice(_price)
            .Build();
    }

    public IEnumerable<Staging> Many(int count)
    {
        for (int i = 0; i < count; i++)
        {
            yield return new StagingBuilder()
                .WithBankday(_bankday.AddDays(i))
                .WithInstrumentType($"{_instrumentType} {i}")
                .WithInstrumentName($"{_instrumentName} {i}")
                .WithPrice(_price + i)
                .Build();
        }
    }
}