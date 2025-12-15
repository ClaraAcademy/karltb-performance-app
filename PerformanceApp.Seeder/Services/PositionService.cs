using System.Numerics;
using PerformanceApp.Infrastructure.Context;
using PerformanceApp.Data.Models;
using PerformanceApp.Infrastructure.Repositories;

namespace PerformanceApp.Seeder.Services;

public interface IPositionService
{
    Task<bool> UpdatePositionsAsync(DateOnly bankday);
}

public class PositionService(PadbContext context) : IPositionService
{
    private readonly IPositionRepository _positionRepository = new PositionRepository(context);
    private readonly IDateInfoService _dateInfoService = new DateInfoService(context);
    private readonly ITransactionRepository _transactionRepository = new TransactionRepository(context);

    private record Dto(
        int PortfolioId,
        int InstrumentId,
        int? Count,
        decimal? Amount,
        decimal? Nominal,
        decimal? Proportion
    );

    private record Key(int PortfolioId, int InstrumentId);

    private static Dto MapToDto(Position position)
    {
        return new Dto(
            position.PortfolioId.GetValueOrDefault(),
            position.InstrumentId.GetValueOrDefault(),
            position.Count,
            position.Amount,
            position.Nominal,
            position.Proportion
        );
    }

    private static Dto MapToDto(Transaction transaction)
    {
        return new Dto(
            transaction.PortfolioId.GetValueOrDefault(),
            transaction.InstrumentId.GetValueOrDefault(),
            transaction.Count,
            transaction.Amount,
            transaction.Nominal,
            transaction.Proportion
        );
    }

    private static Key MapToKey(Dto dto)
    {
        return new Key(dto.PortfolioId, dto.InstrumentId);
    }

    private static Position MapToPosition(IGrouping<Key, Dto> g, DateOnly bankday)
    {
        return new Position
        {
            PortfolioId = g.Key.PortfolioId,
            InstrumentId = g.Key.InstrumentId,
            Bankday = bankday,
            Count = g.Sum(d => d.Count ?? 0),
            Amount = g.Sum(d => d.Amount ?? 0),
            Proportion = g.Sum(d => d.Proportion ?? 0),
            Nominal = g.Sum(d => d.Nominal ?? 0)
        };
    }

    static bool Check<T>(T? value) where T : struct, IComparable<T>, INumber<T>
    {
        return value.HasValue && value.Value.CompareTo(default) > 0;
    }
    private static bool HasNonZeroWeight(Position position)
    {
        return Check(position.Count)
            || Check(position.Amount)
            || Check(position.Nominal)
            || Check(position.Proportion);
    }



    public async Task<bool> UpdatePositionsAsync(DateOnly bankday)
    {
        var bankdayExists = await _dateInfoService.BankdayExistsAsync(bankday);
        if (!bankdayExists)
        {
            return false;
        }
        var previousBankday = await _dateInfoService.GetPreviousBankdayAsync(bankday);

        var positions = await _positionRepository.GetPositionsAsync();
        var previousPositions = positions
            .Where(p => p.Bankday == previousBankday)
            .Select(MapToDto)
            .ToList();

        var transactions = await _transactionRepository.GetTransactionsAsync();
        var todaysTransactions = transactions
            .Where(t => t.Bankday == bankday)
            .Select(MapToDto)
            .ToList();

        var currentPositions = previousPositions
            .Concat(todaysTransactions)
            .GroupBy(MapToKey)
            .Select(g => MapToPosition(g, bankday))
            .Where(HasNonZeroWeight)
            .ToList();

        await _positionRepository.AddPositionsAsync(currentPositions);
        return true;
    }
}