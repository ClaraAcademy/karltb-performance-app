using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PerformanceApp.Data.Seeding;

public static class SqlExecutor
{
    public static async Task ExecuteFilesInDirectory(DbContext context, string folderPath)
    {
        var exists = Directory.Exists(folderPath);
        if (!exists)
        {
            Console.Error.WriteLine($"Directory {folderPath} does not exist! Exiting...");
            return;
        }

        var files = Directory.GetFiles(folderPath, "*.sql")
            .OrderBy(Path.GetFileName)
            .ToList();

        foreach (var file in files)
        {
            var contents = await File.ReadAllTextAsync(file);

            var blank = string.IsNullOrWhiteSpace(contents);
            if (blank)
            {
                Console.Error.Write($"File {file} is blank.");
                continue;
            }
            await context.Database.ExecuteSqlRawAsync(contents);
        }

    }

}