using PerformanceApp.Data.Models;
using PerformanceApp.Data.Builders.Interface;
using PerformanceApp.Data.Builders.Defaults;

namespace PerformanceApp.Data.Builders;

public class InstrumentBuilder : IBuilder<Instrument>
{
    private int _id = InstrumentBuilderDefaults.Id;
    private string _name = InstrumentBuilderDefaults.Name;
    private int _typeId = InstrumentBuilderDefaults.TypeId;
    private InstrumentType _instrumentTypeNavigation = InstrumentBuilderDefaults.InstrumentTypeNavigation;
    private InstrumentPrice _instrumentPriceNavigation = InstrumentBuilderDefaults.InstrumentPriceNavigation;

    public InstrumentBuilder WithId(int id)
    {
        _id = id;
        return this;
    }

    public InstrumentBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    public InstrumentBuilder WithTypeId(int typeId)
    {
        _typeId = typeId;
        return this;
    }

    public InstrumentBuilder WithInstrumentTypeNavigation(InstrumentType instrumentType)
    {
        _instrumentTypeNavigation = instrumentType;
        return this;
    }

    public InstrumentBuilder WithInstrumentPriceNavigation(InstrumentPrice instrumentPrice)
    {
        _instrumentPriceNavigation = instrumentPrice;
        return this;
    }

    public Instrument Build()
    {
        return new Instrument
        {
            Id = _id,
            Name = _name,
            TypeId = _typeId,
            InstrumentTypeNavigation = _instrumentTypeNavigation,
            InstrumentPricesNavigation = [_instrumentPriceNavigation]
        };
    }

    public Instrument Clone()
    {
        return new InstrumentBuilder()
            .WithId(_id)
            .WithName(_name)
            .WithTypeId(_typeId)
            .WithInstrumentTypeNavigation(_instrumentTypeNavigation)
            .WithInstrumentPriceNavigation(_instrumentPriceNavigation)
            .Build();
    }

    public IEnumerable<Instrument> Many(int count)
    {
        for (int i = 0; i < count; i++)
        {
            yield return Build();
        }
    }
}