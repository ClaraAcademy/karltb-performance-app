using Microsoft.OpenApi.Models;

namespace PerformanceApp.Server.Startup.Swagger.References.Constants;

public static class BearerReferenceConstants
{
    public const ReferenceType Type = ReferenceType.SecurityScheme;
    public const string Id = "Bearer";
}