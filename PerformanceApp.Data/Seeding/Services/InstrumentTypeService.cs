using PerformanceApp.Data.Repositories;

namespace PerformanceApp.Data.Seeding.Services;

public interface IInstrumentTypeService
{
    Task<int> GetInstrumentTypeIdAsync(string name);
}

public class InstrumentTypeService(IInstrumentTypeRepository instrumentTypeRepository) : IInstrumentTypeService
{
    private readonly IInstrumentTypeRepository _instrumentTypeRepository = instrumentTypeRepository;

    public async Task<int> GetInstrumentTypeIdAsync(string name)
    {
        var instrumentTypes = await _instrumentTypeRepository.GetInstrumentTypesAsync();

        var instrumentType = instrumentTypes.FirstOrDefault(it => it.Name == name)
            ?? throw new KeyNotFoundException($"InstrumentType with name '{name}' not found.");

        return instrumentType.Id;
    }
}