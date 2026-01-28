namespace CoreBanking.Entites
{
    public class AuditLog
    {
        public Guid Id { get; set; }
        public string EntityName { get; set; } = null!;
        public Guid EntityId { get; set; }
        public string Action { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string? OldValue { get; set; }
        public string? NewValue { get; set; }
        public string PerformedBy { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
    }
}
