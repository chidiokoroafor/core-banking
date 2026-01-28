using CoreBanking.DTOs;
using CoreBanking.Entites;

namespace CoreBanking.Contracts
{
    public interface ITransactionService
    {
        Task AddRangeAsync(IEnumerable<Transaction> entries);
        Task<decimal> GetBalanceBeforeAsync(Guid accountId, DateTime date);
        Task<List<Transaction>> GetEntriesAsync(
            Guid accountId,
            DateTime from,
            DateTime to);
    }
}
