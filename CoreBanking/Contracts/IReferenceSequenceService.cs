using CoreBanking.Entites;

namespace CoreBanking.Contracts
{
    public interface IReferenceSequenceService
    {
        Task<ReferenceSequence?> GetByDateAsync(DateTime date);
        Task AddAsync(ReferenceSequence sequence);
        Task UpdateAsync(ReferenceSequence sequence);
    }
}
