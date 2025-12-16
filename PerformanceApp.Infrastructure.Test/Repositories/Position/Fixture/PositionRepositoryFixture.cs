using PerformanceApp.Infrastructure.Repositories;

namespace PerformanceApp.Infrastructure.Test.Repositories.Position.Fixture;

public class PositionRepositoryFixture : BaseRepositoryTest
{
    protected readonly PositionRepository _repository;

    public PositionRepositoryFixture()
    {
        _repository = new PositionRepository(_context);
    }
}