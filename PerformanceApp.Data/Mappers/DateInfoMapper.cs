using PerformanceApp.Data.Dtos;
using PerformanceApp.Data.Models;

namespace PerformanceApp.Data.Mappers;

public static class DateInfoMapper
{
    public static BankdayDTO MapToBankdayDto(DateInfo dateInfo)
    {
        return MapToBankdayDto(dateInfo.Bankday);
    }

    static BankdayDTO MapToBankdayDto(DateOnly bankday) => new() { Bankday = bankday };

    public static DateInfo Map(DateOnly bankday)
    {
        return new DateInfo { Bankday = bankday };
    }
}