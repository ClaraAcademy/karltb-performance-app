using PerformanceApp.Data.Models;

namespace PerformanceApp.Data.Builders.Defaults;

public static class InstrumentBuilderDefaults
{
    public static readonly InstrumentType InstrumentTypeNavigation = new InstrumentTypeBuilder().Build();
    public static readonly int Id = 1;
    public static readonly string Name = "Default Instrument";
    public static readonly int TypeId = InstrumentTypeNavigation.Id;

}