using Microsoft.AspNetCore.Identity;
using PerformanceApp.Data.Context;
using PerformanceApp.Data.Models;
using PerformanceApp.Server.Services;
using PerformanceApp.Data;

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
