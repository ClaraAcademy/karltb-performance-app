using PerformanceApp.Data.Models;
using PerformanceApp.Data.Builders.Interface;
using PerformanceApp.Data.Builders.Defaults;

namespace PerformanceApp.Data.Builders;

public class InstrumentBuilder : IBuilder<Instrument>
{
    private string _name = InstrumentBuilderDefaults.Name;
    private InstrumentType _instrumentTypeNavigation = new InstrumentTypeBuilder().Build();
    private List<InstrumentPrice> _instrumentPriceNavigation = [];

    public InstrumentBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    public InstrumentBuilder WithInstrumentTypeNavigation(InstrumentType instrumentType)
    {
        _instrumentTypeNavigation = instrumentType;
        return this;
    }

    public InstrumentBuilder WithInstrumentPriceNavigation(IEnumerable<InstrumentPrice> instrumentPrices)
    {
        _instrumentPriceNavigation.AddRange(instrumentPrices);
        return this;
    }

    public InstrumentBuilder WithInstrumentPriceNavigation(InstrumentPrice instrumentPrice)
    {
        _instrumentPriceNavigation.Add(instrumentPrice);
        return this;
    }

    public Instrument Build()
    {
        return new Instrument
        {
            Name = _name,
            InstrumentTypeNavigation = _instrumentTypeNavigation,
            InstrumentPricesNavigation = _instrumentPriceNavigation
        };
    }

    public Instrument Clone()
    {
        return new InstrumentBuilder()
            .WithName(_name)
            .WithInstrumentTypeNavigation(_instrumentTypeNavigation)
            .WithInstrumentPriceNavigation(_instrumentPriceNavigation)
            .Build();
    }

    public IEnumerable<Instrument> Many(int count)
    {
        for (int i = 1; i <= count; i++)
        {
            yield return new InstrumentBuilder()
                .WithName($"{_name} {i}")
                .WithInstrumentTypeNavigation(_instrumentTypeNavigation)
                .WithInstrumentPriceNavigation(_instrumentPriceNavigation)
                .Build();
        }
    }
}