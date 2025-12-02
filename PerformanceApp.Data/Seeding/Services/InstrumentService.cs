using PerformanceApp.Data.Repositories;

namespace PerformanceApp.Data.Seeding.Services;

public interface IInstrumentService
{
    Task<int> GetInstrumentIdAsync(string name);
}

public class InstrumentService(IInstrumentRepository instrumentRepository) : IInstrumentService
{
    private readonly IInstrumentRepository _instrumentRepository = instrumentRepository;

    public async Task<int> GetInstrumentIdAsync(string name)
    {
        var instruments = await _instrumentRepository.GetInstrumentsAsync();

        var instrument = instruments.FirstOrDefault(i => i.Name == name)
            ?? throw new KeyNotFoundException($"Instrument with name '{name}' not found.");

        return instrument.Id;
    }
}