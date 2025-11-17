using PerformanceApp.Data.Context;
using PerformanceApp.Data.Repositories;
using PerformanceApp.Data.Seeding.Constants;

namespace PerformanceApp.Data.Seeding.Entities;

public class StagingSeeder(PadbContext context)
{
    private readonly StagingRepository _stagingRepository = new(context);
    private readonly FileInfo DefaultFile = new(ExcelData.ExcelFilePath);


    public async Task Seed(string? filepath = null)
    {
        var file = new FileInfo(filepath ?? DefaultFile.FullName);
        var stagings = ExcelReader.ReadExcel(file);

        await _stagingRepository.AddStagingsAsync(stagings);
    }
}