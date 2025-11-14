using PerformanceApp.Data.Context;
using PerformanceApp.Data.Repositories;

namespace PerformanceApp.Data.Seeding;

public class StagingSeeder(PadbContext context)
{
    private readonly PadbContext _context = context;
    private readonly StagingRepository _stagingRepository = new(context);
    private readonly FileInfo DefaultFile = new(@"C:\Data\Priser - portföljberäkning.xlsx");


    public void Seed(string? filepath = null)
    {
        var file = new FileInfo(filepath ?? DefaultFile.FullName);
        var stagings = ExcelReader.ReadExcel(file);

        _stagingRepository.AddStagings(stagings);

        _context.SaveChanges();
    }
}