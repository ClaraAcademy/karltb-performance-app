using Microsoft.AspNetCore.Identity;
using PerformanceApp.Data.Context;
using PerformanceApp.Data.Models;
using PerformanceApp.Server.Services;
using PerformanceApp.Data;
using PerformanceApp.Data.Seeding;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services from PerformanceApp.Data
builder.Services.AddDataServices(builder.Configuration);

builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
   .AddEntityFrameworkStores<PadbContext>()
   .AddDefaultTokenProviders();

// Register services
builder.Services.AddScoped<IPortfolioService, PortfolioService>();
builder.Services.AddScoped<IPositionService, PositionService>();
builder.Services.AddScoped<IDateInfoService, DateInfoService>();
builder.Services.AddScoped<ISvgService, SvgService>();
builder.Services.AddScoped<IPerformanceService, PerformanceService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IJwtService, JwtService>();

static void AddAuthenticationScheme(AuthenticationOptions options)
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}

string GetJwtSecret(IConfiguration configuration)
{
    return configuration["Jwt:Secret"] ?? throw new InvalidOperationException("JWT Secret not configured.");
}

TokenValidationParameters GetTokenValidationParameters()
{
    return new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = false,
        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(GetJwtSecret(builder.Configuration)))
    };
}

void AddJwtBearerOptions(JwtBearerOptions options) => options.TokenValidationParameters = GetTokenValidationParameters();

builder.Services.AddAuthentication(AddAuthenticationScheme).AddJwtBearer(AddJwtBearerOptions);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\""
    });

    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});


var app = builder.Build();

await DatabaseInitializer.Initialize(app.Services);

app.UseDefaultFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();

// Make the implicit Program class public so test projects can access it
public partial class Program { }