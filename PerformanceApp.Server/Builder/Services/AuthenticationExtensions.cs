using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using PerformanceApp.Data.Context;
using PerformanceApp.Data.Models;

namespace PerformanceApp.Server.Builder.Services;

public static class AuthenticationExtensions
{
    public static IServiceCollection AddAppAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddIdentity<ApplicationUser, ApplicationRole>()
            .AddEntityFrameworkStores<PadbContext>()
            .AddDefaultTokenProviders();

        services
            .AddAuthentication(AddAuthenticationScheme)
            .AddJwtBearer(options => AddJwtBearerOptions(options, configuration));

        services.AddControllers();
        return services;
    }
    static void AddAuthenticationScheme(AuthenticationOptions options)
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }

    static string GetJwtSecret(IConfiguration configuration)
    {
        return configuration["Jwt:Secret"] ?? throw new InvalidOperationException("JWT Secret not configured.");
    }

    static TokenValidationParameters GetTokenValidationParameters(IConfiguration configuration)
    {
        return new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = false,
            IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(GetJwtSecret(configuration)))
        };
    }

    static void AddJwtBearerOptions(JwtBearerOptions options, IConfiguration configuration)
    {
        options.TokenValidationParameters = GetTokenValidationParameters(configuration);
    }
}