using Microsoft.OpenApi.Models;
using PerformanceApp.Server.Startup.Swagger.Schemes.Constants;

namespace PerformanceApp.Server.Startup.Swagger.Schemes;

using Values = BearerSecuritySchemeConstants;

public static class BearerSecurityProvider
{
    private static readonly OpenApiSecurityScheme _scheme = new()
    {
        Name = Values.Name,
        Type = Values.Type,
        Scheme = Values.Scheme,
        BearerFormat = Values.BearerFormat,
        In = Values.In,
        Description = Values.Description
    };
    public static OpenApiSecurityScheme Scheme => _scheme;
}