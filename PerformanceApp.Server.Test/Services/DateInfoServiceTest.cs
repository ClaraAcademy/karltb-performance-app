using PerformanceApp.Data.Models;
using PerformanceApp.Server.Services;
using Moq;
using PerformanceApp.Infrastructure.Repositories;

namespace PerformanceApp.Server.Test.Services;

public class DateInfoServiceTest
{
    private readonly Mock<IDateInfoRepository> _mockRepo;

    public DateInfoServiceTest()
    {
        _mockRepo = new Mock<IDateInfoRepository>();
    }
    [Fact]
    public async Task GetBankdayDTOsAsync_ReturnsCorrectData()
    {
        // Arrange
        var bankdays = new List<DateInfo>
        {
            new DateInfo { Bankday = new DateOnly(2023, 1, 1) },
            new DateInfo { Bankday = new DateOnly(2023, 1, 2) }
        };

        _mockRepo.Setup(repo => repo.GetDateInfosAsync()).ReturnsAsync(bankdays);

        var service = new DateInfoService(_mockRepo.Object);

        // Act
        var result = await service.GetBankdayDTOsAsync();

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Contains(result, dto => dto.Bankday == new DateOnly(2023, 1, 1));
        Assert.Contains(result, dto => dto.Bankday == new DateOnly(2023, 1, 2));
    }

    [Fact]
    public async Task GetBankdayDTOsAsync_HandlesEmptyData()
    {
        // Arrange
        _mockRepo.Setup(repo => repo.GetDateInfosAsync()).ReturnsAsync(new List<DateInfo>());

        var service = new DateInfoService(_mockRepo.Object);

        // Act
        var result = await service.GetBankdayDTOsAsync();

        // Assert
        Assert.Empty(result);
    }
}