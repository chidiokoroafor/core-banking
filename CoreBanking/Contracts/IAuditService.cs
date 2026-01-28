namespace CoreBanking.Contracts
{
    public interface IAuditService
    {
        Task LogAsync(
        Guid? customerId,
        string action,
        string entity,
        Guid entityId,
        string description
        );
    }
}
