namespace PerformanceApp.Seeder.Test;

[CollectionDefinition(Name)]
public class SeedingCollection : ICollectionFixture<DatabaseFixture>
{
    public const string Name = "Seeding collection";
}
