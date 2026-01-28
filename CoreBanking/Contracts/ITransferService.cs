using CoreBanking.DTOs;

namespace CoreBanking.Contracts
{
    public interface ITransferService
    {
        Task<string> GenerateReferenceAsync();
        Task<GenericResponse> TransferAsync(TransferRequestDto dto, Guid customerId);
    }
}
