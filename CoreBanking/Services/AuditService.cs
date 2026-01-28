using CoreBanking.Contracts;
using CoreBanking.Data;
using CoreBanking.Entites;
using Microsoft.EntityFrameworkCore;

namespace CoreBanking.Services
{

    public class AuditService : IAuditService
    {
        private readonly BankContext _bankContext;
        public AuditService(BankContext bankContext)
        {
            _bankContext = bankContext;
        }
        public async Task LogAsync(Guid? customerId, string action, string entity, Guid entityId, string description)
        {
            var log = new AuditLog
            {
                Id = Guid.NewGuid(),
                PerformedBy = customerId.ToString(),
                Action = action,
                EntityName = entity,
                EntityId = entityId,
                CreatedAt = DateTime.UtcNow,
                Description = description
            };

            _bankContext.Set<AuditLog>().Add(log);
            await _bankContext.SaveChangesAsync();
        }
    }
}
