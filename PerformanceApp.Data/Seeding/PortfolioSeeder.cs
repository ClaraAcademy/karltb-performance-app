
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using PerformanceApp.Data.Context;
using PerformanceApp.Data.Models;
using PerformanceApp.Data.Repositories;

namespace PerformanceApp.Data.Seeding;

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
        var userA = (await GetUser("UserA"))!;
        var userB = (await GetUser("UserB"))!;

        var dtos = new List<Dto>
        {
            new ("Portfolio A", userA),
            new ("Benchmark A", userA),
            new ("Portfolio B", userB),
            new ("Benchmark B", userB)
        };

        var portfolios = dtos.Select(MapToPortfolio).ToList();

        await _portfolioRepository.AddPortfoliosAsync(portfolios);
    }
}