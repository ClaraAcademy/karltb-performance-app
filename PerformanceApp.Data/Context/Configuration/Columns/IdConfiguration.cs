using PerformanceApp.Data.Context.Configuration.Constants.Columns;
using Microsoft.EntityFrameworkCore;

namespace PerformanceApp.Data.Context.Configuration.Columns;

public static class IdConfiguration
{
    public static void ConfigureIdColumns(this ModelBuilder modelBuilder)
    {
        var entityTypes = modelBuilder.Model.GetEntityTypes();

        foreach (var entityType in entityTypes)
        {
            var clr = entityType.ClrType;
            var id = clr.GetProperty(Id.Name);
            var isValid = id != null && id.PropertyType == typeof(int);
            if (isValid)
            {
                modelBuilder.Entity(clr)
                    .Property(Id.Name)
                    .UseIdentityColumn();
            }

        }

    }
}