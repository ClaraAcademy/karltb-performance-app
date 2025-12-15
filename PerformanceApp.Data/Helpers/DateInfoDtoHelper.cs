using PerformanceApp.Data.Dtos;

namespace PerformanceApp.Data.Helpers;

public static class BankdayDtoHelper
{
    public static List<DateOnly> OrderedBankdays(this IEnumerable<BankdayDTO> dtos)
    {
        return dtos
            .OrderBy(dto => dto.Bankday)
            .Select(dto => dto.Bankday)
            .ToList();
    }

}