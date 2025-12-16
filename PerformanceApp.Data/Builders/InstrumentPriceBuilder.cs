using PerformanceApp.Data.Builders.Defaults;
using PerformanceApp.Data.Builders.Interface;
using PerformanceApp.Data.Models;

namespace PerformanceApp.Data.Builders;

public class InstrumentPriceBuilder : IBuilder<InstrumentPrice>
{
    private int _instrumentId = InstrumentPriceBuilderDefaults.InstrumentId;
    private DateOnly _bankday = InstrumentPriceBuilderDefaults.Bankday;
    private decimal _price = InstrumentPriceBuilderDefaults.Price;
    private DateInfo? _bankdayNavigation = null;
    private Instrument? _instrumentNavigation = null;
    public InstrumentPriceBuilder WithInstrumentId(int instrumentId)
    {
        _instrumentId = instrumentId;
        return this;
    }
    public InstrumentPriceBuilder WithBankday(DateOnly bankday)
    {
        _bankday = bankday;
        return this;
    }
    public InstrumentPriceBuilder WithPrice(decimal price)
    {
        _price = price;
        return this;
    }
    public InstrumentPriceBuilder WithBankdayNavigation(DateInfo bankdayNavigation)
    {
        _bankdayNavigation = bankdayNavigation;
        return this;
    }
    public InstrumentPriceBuilder WithInstrumentNavigation(Instrument instrumentNavigation)
    {
        _instrumentNavigation = instrumentNavigation;
        return this;
    }
    public InstrumentPrice Build()
    {
        return new InstrumentPrice
        {
            InstrumentId = _instrumentId,
            Bankday = _bankday,
            Price = _price,
            BankdayNavigation = _bankdayNavigation ?? new DateInfoBuilder().WithBankday(_bankday).Build(),
            InstrumentNavigation = _instrumentNavigation ?? new InstrumentBuilder().WithId(_instrumentId).Build()
        };
    }

    public InstrumentPrice Clone()
    {
        return new InstrumentPriceBuilder()
            .WithInstrumentId(_instrumentId)
            .WithBankday(_bankday)
            .WithPrice(_price)
            .WithBankdayNavigation(
                _bankdayNavigation ?? new DateInfoBuilder().WithBankday(_bankday).Build()
            )
            .WithInstrumentNavigation(
                _instrumentNavigation ?? new InstrumentBuilder().WithId(_instrumentId).Build()
            )
                .Build();
    }

    public IEnumerable<InstrumentPrice> Many(int count)
    {
        for (int i = 0; i < count; i++)
        {
            var bankday = _bankday.AddDays(i);
            var instrumentId = _instrumentId + i;
            var price = _price + i;
            yield return new InstrumentPriceBuilder()
                .WithInstrumentId(instrumentId)
                .WithBankday(bankday)
                .WithPrice(price)
                .WithBankdayNavigation(
                    new DateInfoBuilder().WithBankday(bankday).Build()
                )
                .WithInstrumentNavigation(
                    _instrumentNavigation ?? new InstrumentBuilder().WithId(instrumentId).Build()
                )
                .Build();
        }
    }
}
