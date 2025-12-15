using PerformanceApp.Infrastructure.Repositories;
using PerformanceApp.Data.Models;

namespace PerformanceApp.Data.Test.Repositories;

public class DateInfoRepositoryTest : BaseRepositoryTest
{
    private readonly DateInfoRepository _repository;
    private readonly List<DateInfo> _dateInfos = CreateDateInfos();

    public DateInfoRepositoryTest()
    {
        _repository = new DateInfoRepository(_context);
    }

    private static List<DateInfo> CreateDateInfos()
    {
        return [
            new DateInfo { Bankday = new DateOnly(2024, 1, 1) },
            new DateInfo { Bankday = new DateOnly(2024, 1, 2) },
            new DateInfo { Bankday = new DateOnly(2024, 1, 3) },
            new DateInfo { Bankday = new DateOnly(2024, 1, 4) },
            new DateInfo { Bankday = new DateOnly(2024, 1, 5) },
            new DateInfo { Bankday = new DateOnly(2024, 1, 6) },
            new DateInfo { Bankday = new DateOnly(2024, 1, 7) },
            new DateInfo { Bankday = new DateOnly(2024, 1, 8) },
            new DateInfo { Bankday = new DateOnly(2024, 1, 9) },
            new DateInfo { Bankday = new DateOnly(2024, 1, 10) }
        ];
    }

    [Fact]
    public async Task AddDateInfosAsync_AddsDateInfos()
    {
        await _repository.AddDateInfosAsync(_dateInfos);

        Assert.Equal(_dateInfos.Count, _context.DateInfos.Count());
    }

    [Fact]
    public async Task GetDateInfosAsync_ReturnsDateInfos()
    {
        _context.DateInfos.AddRange(_dateInfos);
        _context.SaveChanges();

        var dateInfos = await _repository.GetDateInfosAsync();

        Assert.Equal(_dateInfos.Count, dateInfos.Count());
    }



}