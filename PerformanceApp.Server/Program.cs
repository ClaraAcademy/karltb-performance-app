using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PerformanceApp.Server.Data;
using PerformanceApp.Server.Models;
using PerformanceApp.Server.Repositories;
using PerformanceApp.Server.Services;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<PadbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("PadbContext") ?? throw new InvalidOperationException("Connection string 'PadbContext' not found.")));

// Add services to the container.

// Register repositories
builder.Services.AddScoped<IPortfolioRepository, PortfolioRepository>();
builder.Services.AddScoped<IPositionRepository, PositionRepository>();
builder.Services.AddScoped<IDateInfoRepository, DateInfoRepository>();

// Register services
builder.Services.AddScoped<IPortfolioService, PortfolioService>();
builder.Services.AddScoped<IPositionService, PositionService>();
builder.Services.AddScoped<IDateInfoService, DateInfoService>();

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
//app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
