using CoreBanking.Contracts;
using CoreBanking.Data;
using CoreBanking.Entites;
using Microsoft.EntityFrameworkCore;

namespace CoreBanking.Services
{
    public class ReferenceSequenceService : IReferenceSequenceService
    {
        private readonly BankContext _bankContext;
        public ReferenceSequenceService(BankContext bankContext)
        {
            _bankContext = bankContext;
        }
        public async Task AddAsync(ReferenceSequence sequence)
        {
            await _bankContext.ReferenceSequence.AddAsync(sequence);
            await _bankContext.SaveChangesAsync();
        }

        public async Task<ReferenceSequence?> GetByDateAsync(DateTime date)
        {
            var sequence = await _bankContext.ReferenceSequence.Where(r => r.Date == date).FirstOrDefaultAsync();
            return sequence;
        }

        public async Task UpdateAsync(ReferenceSequence sequence)
        {
             _bankContext.ReferenceSequence.Update(sequence);
            await _bankContext.SaveChangesAsync();
        }
    }
}
