using Moq;
using PerformanceApp.Infrastructure.Repositories;
using PerformanceApp.Server.Services;

namespace PerformanceApp.Server.Test.Services.PositionServiceTests.Fixture;

public class PerformanceServiceTestFixture
{
    protected Mock<IPositionRepository> _positionRepositoryMock;
    protected IPositionService _positionService;

    public PerformanceServiceTestFixture()
    {
        _positionRepositoryMock = new Mock<IPositionRepository>();
        _positionService = new PositionService(_positionRepositoryMock.Object);
    }
}