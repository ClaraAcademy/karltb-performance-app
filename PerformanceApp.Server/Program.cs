using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using PerformanceApp.Server.Data;
using PerformanceApp.Server.Models;
using PerformanceApp.Server.Repositories;
using PerformanceApp.Server.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<PadbContext>(
    options => options.UseSqlServer(
        builder.Configuration.GetConnectionString("PadbContext")
        ?? throw new InvalidOperationException("Connection string 'PadbContext' not found.")
    )
);

// Add Identity
builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
    .AddEntityFrameworkStores<PadbContext>()
    .AddDefaultTokenProviders();

// Add services to the container.

// Register repositories
builder.Services.AddScoped<IPortfolioRepository, PortfolioRepository>();
builder.Services.AddScoped<IPositionRepository, PositionRepository>();
builder.Services.AddScoped<IDateInfoRepository, DateInfoRepository>();
builder.Services.AddScoped<IBenchmarkRepository, BenchmarkRepository>();
builder.Services.AddScoped<IPerformanceRepository, PerformanceRepository>();

// Register services
builder.Services.AddScoped<IPortfolioService, PortfolioService>();
builder.Services.AddScoped<IPositionService, PositionService>();
builder.Services.AddScoped<IDateInfoService, DateInfoService>();
builder.Services.AddScoped<ISvgService, SvgService>();
builder.Services.AddScoped<IPerformanceService, PerformanceService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
