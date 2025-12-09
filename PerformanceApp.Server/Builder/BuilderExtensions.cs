using PerformanceApp.Server.Builder.Services;

namespace PerformanceApp.Server.Builder;

public static class BuilderExtensions
{
    public static WebApplicationBuilder AddServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddAppServices(builder.Configuration);
        builder.Services.AddAppAuthentication(builder.Configuration);
        builder.Services.AddAppCors();
        builder.Services.AddAppSwagger();

        return builder;
    }
}