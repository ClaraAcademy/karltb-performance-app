using PerformanceApp.Data.Seeding;
using PerformanceApp.Server.App;
using PerformanceApp.Server.App.Development;
using PerformanceApp.Server.Builder;

var builder = WebApplication.CreateBuilder(args);

builder.AddServices();

var app = builder.Build();

await DatabaseInitializer.Initialize(app.Services);

app.UseDefaultFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.AddDevelopmentServices();
}

app.AddServices();

app.Run();

// Make the implicit Program class public so test projects can access it
public partial class Program { }
