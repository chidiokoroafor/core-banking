using CoreBanking.Contracts;
using CoreBanking.Data;
using CoreBanking.DTOs;
using CoreBanking.Entites;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CoreBanking.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly BankContext _bankContext;
        public TransactionService(BankContext bankContext)
        {
            _bankContext = bankContext;
        }
        public async Task AddRangeAsync(IEnumerable<Transaction> entries)
        {
            await _bankContext.Transactions.AddRangeAsync(entries);
            await _bankContext.SaveChangesAsync();
        }

        public async Task<decimal> GetBalanceBeforeAsync(Guid accountId, DateTime date)
        {
            var transactions = await _bankContext.Transactions.Where(t=>t.AccountId == accountId && t.CreatedAt.Date < date.Date).ToListAsync();
            var balance = transactions.Count > 0 ? transactions.Sum(t=>t.Amount) : 0;
            return balance;
        }

        public async Task<List<Transaction>> GetEntriesAsync(Guid accountId, DateTime from, DateTime to)
        {
            var entries = await _bankContext.Transactions.Where(t =>
                        t.AccountId == accountId && from.Date <= t.CreatedAt.Date && to.Date >= t.CreatedAt.Date).ToListAsync();
            return entries;
        }
    }
}
