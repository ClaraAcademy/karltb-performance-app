using PerformanceApp.Data.Models;
using PerformanceApp.Server.Dtos;

namespace PerformanceApp.Server.Services.Mappers;

public static class DateInfoMapper
{
    public static BankdayDTO MapToBankdayDto(DateInfo dateInfo)
    {
        return MapToBankdayDto(dateInfo.Bankday);
    }

    static BankdayDTO MapToBankdayDto(DateOnly bankday) => new() { Bankday = bankday };
}