using PerformanceApp.Infrastructure.Context;
using PerformanceApp.Infrastructure.Repositories;

namespace PerformanceApp.Seeder.Services;

public interface IInstrumentTypeService
{
    Task<int> GetInstrumentTypeIdAsync(string name);
}

public class InstrumentTypeService(PadbContext context) : IInstrumentTypeService
{
    private readonly IInstrumentTypeRepository _instrumentTypeRepository = new InstrumentTypeRepository(context);

    public async Task<int> GetInstrumentTypeIdAsync(string name)
    {
        var instrumentTypes = await _instrumentTypeRepository.GetInstrumentTypesAsync();

        var instrumentType = instrumentTypes.FirstOrDefault(it => it.Name == name)
            ?? throw new KeyNotFoundException($"InstrumentType with name '{name}' not found.");

        return instrumentType.Id;
    }
}