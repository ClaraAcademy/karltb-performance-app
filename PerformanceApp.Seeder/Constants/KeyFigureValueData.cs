using PerformanceApp.Seeder.Dtos;

namespace PerformanceApp.Seeder.Constants;

public static class KeyFigureValueData
{
    public static List<KeyFigureValueDto> KeyFigureValues => _keyFigureValues;
    private static readonly List<KeyFigureValueDto> _keyFigureValues = [
new KeyFigureValueDto("Benchmark A","Standard Deviation",0.0156030116582580M),
new KeyFigureValueDto("Benchmark A","Annualised Cumulative Return",0.2076456474590860M),
new KeyFigureValueDto("Benchmark A","Half-Year Performance",0.0964637792324388M),
new KeyFigureValueDto("Benchmark B","Standard Deviation",0.0077830883062256M),
new KeyFigureValueDto("Benchmark B","Annualised Cumulative Return",0.0886509987762154M),
new KeyFigureValueDto("Benchmark B","Half-Year Performance",0.0423298778155934M),
new KeyFigureValueDto("Portfolio A","Standard Deviation",0.0226550757644645M),
new KeyFigureValueDto("Portfolio A","Tracking Error",0.024715013671581788M),
new KeyFigureValueDto("Portfolio A","Annualised Cumulative Return",0.9207460438995008M),
new KeyFigureValueDto("Portfolio A","Information Ratio",0.15706367791998857M),
new KeyFigureValueDto("Portfolio A","Half-Year Performance",0.3751824817518246M),
new KeyFigureValueDto("Portfolio B","Standard Deviation",0.0122968496343805M),
new KeyFigureValueDto("Portfolio B","Tracking Error",0.012991040151499236M),
new KeyFigureValueDto("Portfolio B","Annualised Cumulative Return",0.3878415121389174M),
new KeyFigureValueDto("Portfolio B","Information Ratio",0.1543093529266115M),
new KeyFigureValueDto("Portfolio B","Half-Year Performance",0.1734792495736210M),
    ];
}