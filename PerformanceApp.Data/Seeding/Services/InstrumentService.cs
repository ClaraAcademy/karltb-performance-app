using PerformanceApp.Data.Repositories;
using PerformanceApp.Data.Seeding.Constants;

namespace PerformanceApp.Data.Seeding.Services;

public interface IInstrumentService
{
    Task<int> GetInstrumentIdAsync(string name);
    Task<bool> InstrumentHasCorrectWeight(int id, int? count, decimal? amount, decimal? proportion, decimal? nominal);
}

public class InstrumentService : IInstrumentService
{
    private readonly IInstrumentRepository _instrumentRepository;
    private readonly IInstrumentTypeRepository _instrumentTypeRepository;

    private static string InstrumentNameErrorMessage(string name)
    {
        return $"Instrument with name '{name}' not found.";
    }
    private static string InstrumentIdErrorMessage(int id)
    {
        return $"Instrument with id '{id}' not found.";
    }
    private static string InstrumentTypeErrorMessage(int id)
    {
        return $"InstrumentType for Instrument with id '{id}' not found.";
    }
    private static string InstrumentTypeErrorMessage(string name, int id)
    {
        return $"Unknown InstrumentType '{name}' for Instrument with id '{id}'.";
    }

    public InstrumentService(
        IInstrumentRepository instrumentRepository,
        IInstrumentTypeRepository instrumentTypeRepository
    )
    {
        _instrumentRepository = instrumentRepository;
        _instrumentTypeRepository = instrumentTypeRepository;
    }

    public async Task<int> GetInstrumentIdAsync(string name)
    {
        var instruments = await _instrumentRepository.GetInstrumentsAsync();

        var instrument = instruments.FirstOrDefault(i => i.Name == name)
            ?? throw new KeyNotFoundException(InstrumentNameErrorMessage(name));

        return instrument.Id;
    }
    public async Task<bool> InstrumentHasCorrectWeight(int id, int? count, decimal? amount, decimal? proportion, decimal? nominal)
    {
        var instruments = await _instrumentRepository.GetInstrumentsAsync();
        var instrument = instruments.FirstOrDefault(i => i.Id == id)
            ?? throw new KeyNotFoundException(InstrumentIdErrorMessage(id));

        var instrumentTypes = await _instrumentTypeRepository.GetInstrumentTypesAsync();
        var instrumentType = instrumentTypes.FirstOrDefault(it => it.Id == instrument.TypeId)
            ?? throw new KeyNotFoundException(InstrumentTypeErrorMessage(instrument.TypeId ?? 0));
        var typeName = instrumentType.Name;

        return typeName switch
        {
            InstrumentTypeData.Stock => count.HasValue,
            InstrumentTypeData.Bond => nominal.HasValue,
            InstrumentTypeData.Index => proportion.HasValue,
            _ => throw new InvalidOperationException(InstrumentTypeErrorMessage(typeName, id))
        };
    }
}