using Microsoft.AspNetCore.Mvc.Testing;

namespace PerformanceApp.Server.Test.Controllers.Integration;

[CollectionDefinition(Name)]
public class IntegrationCollection
    : ICollectionFixture<DatabaseFixture>,
        ICollectionFixture<WebApplicationFactory<Program>>
{
    public const string Name = "Integration test collection";
}