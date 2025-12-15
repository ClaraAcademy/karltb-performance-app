using PerformanceApp.Seeder;
using PerformanceApp.Server.Startup.App;
using PerformanceApp.Server.Startup.App.Development;
using PerformanceApp.Server.Startup.Builder;

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
