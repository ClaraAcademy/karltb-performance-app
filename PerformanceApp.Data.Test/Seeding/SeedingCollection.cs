namespace PerformanceApp.Data.Test.Seeding;

[CollectionDefinition(Name)]
public class SeedingCollection : ICollectionFixture<DatabaseFixture>
{
    public const string Name = "Seeding collection";
}
