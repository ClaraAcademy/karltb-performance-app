using Microsoft.OpenApi.Models;

namespace PerformanceApp.Server.Startup.Swagger.Schemes.Constants;

public static class BearerSecuritySchemeConstants
{
    public const string Name = "Authorization";
    public const SecuritySchemeType Type = SecuritySchemeType.Http;
    public const string Scheme = "Bearer";
    public const string BearerFormat = "JWT";
    public const ParameterLocation In = ParameterLocation.Header;
    public const string Description
        = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"";
}