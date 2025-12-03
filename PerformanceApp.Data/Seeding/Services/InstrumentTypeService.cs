using PerformanceApp.Data.Context;
using PerformanceApp.Data.Repositories;

namespace PerformanceApp.Data.Seeding.Services;

public interface IInstrumentTypeService
{
    Task<int> GetInstrumentTypeIdAsync(string name);
}

public class InstrumentTypeService : IInstrumentTypeService
{
    private readonly IInstrumentTypeRepository _instrumentTypeRepository;

    public InstrumentTypeService(PadbContext context)
    {
        _instrumentTypeRepository = new InstrumentTypeRepository(context);
    }

    public async Task<int> GetInstrumentTypeIdAsync(string name)
    {
        var instrumentTypes = await _instrumentTypeRepository.GetInstrumentTypesAsync();

        var instrumentType = instrumentTypes.FirstOrDefault(it => it.Name == name)
            ?? throw new KeyNotFoundException($"InstrumentType with name '{name}' not found.");

        return instrumentType.Id;
    }
}