using Microsoft.AspNetCore.Identity;

namespace PerformanceApp.Data.Models;

public class ApplicationUser : IdentityUser
{
    public virtual ICollection<Portfolio> PortfoliosNavigation { get; set; } = [];
}