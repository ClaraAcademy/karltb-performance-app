using System.ComponentModel.DataAnnotations;

namespace PerformanceApp.Server.Models;

public class DateInfo
{
    [Key]
    public DateTime Bankday { get; set; }
}