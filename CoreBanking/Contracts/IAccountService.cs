using CoreBanking.DTOs;
using CoreBanking.Entites;
using System.Threading.Tasks;

namespace CoreBanking.Contracts
{
    public interface IAccountService
    {
        Task<GenericResponse> CreateAccount(AccountCreationRequest request, Guid customerId);
        Task<Account?> GetByAccountNumberAsync(string accountNumber);
        Task UpdateAsync(Account account);
        Task<GenericResponse> GetStatementAsync(
        string accountNumber,
        DateTime from,
        DateTime to,
        Guid customerId);
    }
}
