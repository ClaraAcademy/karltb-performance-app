using PerformanceApp.Data.Models;
using PerformanceApp.Server.Services;
using Moq;
using PerformanceApp.Infrastructure.Repositories;
using PerformanceApp.Data.Builders;
using PerformanceApp.Data.Helpers;

namespace PerformanceApp.Server.Test.Services;

public class DateInfoServiceTest
{
    private readonly Mock<IDateInfoRepository> _mockRepo;
    private readonly DateInfoService _service;

    public DateInfoServiceTest()
    {
        _mockRepo = new Mock<IDateInfoRepository>();
        _service = new DateInfoService(_mockRepo.Object);
    }

    [Fact]
    public async Task GetBankdayDTOsAsync_ReturnsCorrectData()
    {
        // Arrange
        var n = 3;
        var dateInfos = new DateInfoBuilder()
            .Many(n)
            .ToList();

        _mockRepo
            .Setup(repo => repo.GetDateInfosAsync())
            .ReturnsAsync(dateInfos);

        // Act
        var result = await _service.GetBankdayDTOsAsync();

        // Assert
        Assert.Equal(n, result.Count);
        var eSorted = dateInfos.OrderedBankdays();
        var aSorted = result.OrderedBankdays();
        Assert.Equal(eSorted, aSorted);
    }

    [Fact]
    public async Task GetBankdayDTOsAsync_HandlesEmptyData()
    {
        // Arrange
        _mockRepo
            .Setup(repo => repo.GetDateInfosAsync())
            .ReturnsAsync([]);

        // Act
        var result = await _service.GetBankdayDTOsAsync();

        // Assert
        Assert.Empty(result);
    }
}