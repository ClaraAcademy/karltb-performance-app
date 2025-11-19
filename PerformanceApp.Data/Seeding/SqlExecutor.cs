using Microsoft.EntityFrameworkCore;
using PerformanceApp.Data.Context;

namespace PerformanceApp.Data.Seeding;

public static class SqlExecutor
{
    private static readonly string BatchSeparator = "GO";
    public static async Task ExecuteQueryAsync(PadbContext context, FormattableString query)
    {
        await context.Database.ExecuteSqlInterpolatedAsync(query);
        await context.SaveChangesAsync();
    }
    private static List<string> GetFilesInDirectory(string folderPath)
    {
        return Directory.GetFiles(folderPath, "*.sql")
            .OrderBy(Path.GetFileName)
            .ToList();
    }

    private static bool IsNotBlank(string s) => !string.IsNullOrWhiteSpace(s);

    private static List<string> GetBatches(string fileContents)
    {
        return fileContents.Split(BatchSeparator)
            .Select(b => b.Trim())
            .Where(IsNotBlank)
            .ToList();
    }

    public static async Task ExecuteFilesInDirectory(DbContext context, string folderPath)
    {
        var exists = Directory.Exists(folderPath);
        if (!exists)
        {
            var errorMessage = $"Directory {folderPath} does not exist! Exiting...";
            Console.Error.WriteLine(errorMessage);
            return;
        }

        var directoryMessage = $"Executing files in directory: {folderPath}";
        Console.Error.WriteLine(directoryMessage);

        var files = GetFilesInDirectory(folderPath);

        foreach (var file in files)
        {
            var fileMessage = $"Executing file: {file}";
            Console.Error.WriteLine(fileMessage);

            var contents = await File.ReadAllTextAsync(file);

            var blank = string.IsNullOrWhiteSpace(contents);
            if (blank)
            {
                Console.Error.Write($"File {file} is blank.");
                continue;
            }
            var batches = GetBatches(contents);
            foreach (var batch in batches)
            {
                await context.Database.ExecuteSqlRawAsync(batch);
            }
        }

    }

}