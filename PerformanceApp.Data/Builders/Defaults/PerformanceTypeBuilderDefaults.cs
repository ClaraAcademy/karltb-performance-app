using PerformanceApp.Data.Models;

namespace PerformanceApp.Data.Builders.Defaults;

public static class PerformanceTypeBuilderDefaults
{
    public static int Id => 1;
    public static string Name => "Default Performance Type";

    public static PerformanceType PerformanceType => new PerformanceTypeBuilder().Build();
}