using Microsoft.AspNetCore.Identity;

namespace PerformanceApp.Server.Models;

public class ApplicationUser : IdentityUser
{
    public virtual ICollection<Portfolio> PortfoliosNavigation { get; set; } = [];
}