using PerformanceApp.Data.Builders.Defaults;
using PerformanceApp.Data.Builders.Interface;
using PerformanceApp.Data.Models;

namespace PerformanceApp.Data.Builders;

public class DateInfoBuilder : IBuilder<DateInfo>
{
    private DateOnly _bankday = DateInfoBuilderDefaults.Bankday;

    public DateInfoBuilder WithBankday(DateOnly bankday)
    {
        _bankday = bankday;
        return this;
    }

    public DateInfo Build()
    {
        return new DateInfo{    Bankday = _bankday};
    }

    public DateInfo Clone()
    {
        return new DateInfoBuilder()
            .WithBankday(_bankday)
            .Build();
    }

    public IEnumerable<DateInfo> Many(int count)
    {
        for (int i = 0; i < count; i++)
        {
            yield return new DateInfoBuilder()
                .WithBankday(_bankday.AddDays(i))
                .Build();
        }
    }
}