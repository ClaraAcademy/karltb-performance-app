
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using PerformanceApp.Data.Context;
using PerformanceApp.Data.Models;
using PerformanceApp.Data.Repositories;
using PerformanceApp.Data.Seeding.Constants;

namespace PerformanceApp.Data.Seeding.Entities;

public class PortfolioSeeder(PadbContext context, UserManager<ApplicationUser> userManager)
{
    private readonly PadbContext _context = context;
    private readonly PortfolioRepository _portfolioRepository = new(context);
    private readonly UserManager<ApplicationUser> _userManager = userManager;

    private record Dto(string PortfolioName, ApplicationUser User);

    private async Task<ApplicationUser?> GetUser(string username) => await _userManager.FindByNameAsync(username);
    private static Portfolio MapToPortfolio(Dto dto) => new Portfolio { PortfolioName = dto.PortfolioName, UserID = dto.User.Id };

    public async Task Seed()
    {
        var userA = (await GetUser(UserData.UsernameA))!;
        var userB = (await GetUser(UserData.UsernameB))!;

        var dtos = new List<Dto>
        {
            new (PortfolioData.PortfolioA, userA),
            new (PortfolioData.BenchmarkA, userA),
            new (PortfolioData.PortfolioB, userB),
            new (PortfolioData.BenchmarkB, userB)
        };

        var portfolios = dtos.Select(MapToPortfolio).ToList();

        await _portfolioRepository.AddPortfoliosAsync(portfolios);
    }
}