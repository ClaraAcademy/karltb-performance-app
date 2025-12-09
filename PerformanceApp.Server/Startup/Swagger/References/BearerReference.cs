using Microsoft.OpenApi.Models;
using PerformanceApp.Server.Startup.Swagger.References.Constants;

namespace PerformanceApp.Server.Startup.Swagger.References;

public static class BearerReference
{
    private static readonly OpenApiReference _reference = new()
    {
        Type = BearerReferenceConstants.Type,
        Id = BearerReferenceConstants.Id
    };
    public static OpenApiReference Reference => _reference;
}