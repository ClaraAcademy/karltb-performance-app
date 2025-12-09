using Microsoft.OpenApi.Models;

namespace PerformanceApp.Server.Swagger.References;

public static class BearerReference
{
    private static readonly OpenApiReference _reference = new()
    {
        Type = Constants.BearerReferenceConstants.Type,
        Id = Constants.BearerReferenceConstants.Id
    };
    public static OpenApiReference Reference => _reference;
}