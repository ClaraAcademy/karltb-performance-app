using PerformanceApp.Infrastructure.Context;
using PerformanceApp.Data.Models;
using PerformanceApp.Infrastructure.Repositories;
using PerformanceApp.Seeder;
using PerformanceApp.Seeder.Constants;

namespace PerformanceApp.Seeder.Entities;

public class StagingSeeder(PadbContext context)
{
    private readonly StagingRepository _stagingRepository = new(context);
    private readonly FileInfo DefaultFile = new(ExcelData.ExcelFilePath);

    private record Key(DateOnly Bankday, string InstrumentName, string InstrumentType);

    private static Key? GetKey(Staging staging)
    {
        var bankday = staging.Bankday;
        var instrumentName = staging.InstrumentName;
        var instrumentType = staging.InstrumentType;

        var notNull = bankday != null && instrumentName != null && instrumentType != null;

        if (!notNull)
        {
            return null;
        }

        return new Key(bankday!.Value, instrumentName!, instrumentType!);
    }

    private async Task<List<Staging>> GetStagings(FileInfo file)
    {
        var stagings = ExcelReader.ReadExcel(file);

        var existing = await _stagingRepository.GetStagingsAsync();

        var existingKeys = existing.Select(GetKey)
            .OfType<Key>()
            .ToHashSet();

        bool isValid(Staging staging)
        {
            var key = GetKey(staging);

            if (key == null)
            {
                return false;
            }

            return !existingKeys.Contains(key);
        }

        return stagings.Where(isValid).ToList();
    }

    public async Task Seed(string? filepath = null)
    {
        var file = new FileInfo(filepath ?? DefaultFile.FullName);

        var stagings = await GetStagings(file);

        if (!stagings.Any())
        {
            return;
        }

        await _stagingRepository.AddStagingsAsync(stagings);
    }
}