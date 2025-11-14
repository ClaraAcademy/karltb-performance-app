
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


    private ApplicationUser GetUser(string username) => _userManager.FindByNameAsync(username).GetAwaiter().GetResult()!;
    private static Portfolio MapToPortfolio(string name, ApplicationUser user) => new Portfolio { PortfolioName = name, UserID = user!.Id };

    public void Seed()
    {
        var userA = GetUser("UserA");
        var userB = GetUser("UserB");

        var portfolios = new List<Portfolio>
        {
            MapToPortfolio("Portfolio A", userA!),
            MapToPortfolio("Benchmark A", userA!),
            MapToPortfolio("Portfolio B", userB!),
            MapToPortfolio("Benchmark B", userB!),
        };

        _portfolioRepository.AddPortfolios(portfolios);

        _context.SaveChanges();
    }
}