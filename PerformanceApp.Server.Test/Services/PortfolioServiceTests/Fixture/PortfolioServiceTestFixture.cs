using Moq;
using PerformanceApp.Data.Repositories;
using PerformanceApp.Server.Services;

namespace PerformanceApp.Server.Test.Services.PortfolioServiceTests.Fixture;

public class PortfolioServiceTestFixture
{
    protected Mock<IPortfolioRepository> _portfolioRepositoryMock;
    protected IPortfolioService _portfolioService;

    public PortfolioServiceTestFixture()
    {
        _portfolioRepositoryMock = new Mock<IPortfolioRepository>();
        _portfolioService = new PortfolioService(_portfolioRepositoryMock.Object);
    }
}