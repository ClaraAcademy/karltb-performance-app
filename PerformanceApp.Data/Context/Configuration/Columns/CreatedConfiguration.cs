using Microsoft.EntityFrameworkCore;
using PerformanceApp.Data.Context.Configuration.Constants.Columns;

namespace PerformanceApp.Data.Context.Configuration.Columns;

public static class CreatedConfiguration
{
    public static void ConfigureCreatedColumns(this ModelBuilder modelBuilder)
    {
        var entityTypes = modelBuilder.Model.GetEntityTypes();

        foreach (var entityType in entityTypes)
        {
            var clr = entityType.ClrType;
            var created = clr.GetProperty(Created.Name);
            var isValid = created != null && created.PropertyType == typeof(DateTime);
            if (isValid)
            {
                modelBuilder.Entity(clr)
                    .Property(Created.Name)
                    .HasDefaultValueSql(Created.DefaultValue);
            }
        }
    }
}