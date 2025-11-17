using PerformanceApp.Data.Repositories;
using PerformanceApp.Data.Models;

namespace PerformanceApp.Data.Test.Repositories;

public class DateInfoRepositoryTest : RepositoryTest
{
    [Fact]
    public async Task AddDateInfosAsync_AddsDateInfos()
    {
        var context = CreateContext();
        var repo = new DateInfoRepository(context);

        var dateInfos = new List<DateInfo>
        {
            new DateInfo { Bankday = new DateOnly(2024, 1, 1) },
            new DateInfo { Bankday = new DateOnly(2024, 1, 2) }
        };

        await repo.AddDateInfosAsync(dateInfos);

        Assert.Equal(dateInfos.Count(), context.DateInfos.Count());
    }

    [Fact]
    public async Task GetDateInfosAsync_ReturnsDateInfos()
    {
        var context = CreateContext();
        context.DateInfos.AddRange(new List<DateInfo>
        {
            new DateInfo { Bankday = new DateOnly(2024, 1, 1) },
            new DateInfo { Bankday = new DateOnly(2024, 1, 2) }
        });
        context.SaveChanges();

        var repo = new DateInfoRepository(context);

        var dateInfos = await repo.GetDateInfosAsync();

        Assert.Equal(2, dateInfos.Count());
    }



}