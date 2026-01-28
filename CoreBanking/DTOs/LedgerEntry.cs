namespace CoreBanking.DTOs
{
    public class LedgerEntry
    {
        public Guid AccountId { get; set; }
        public Guid? TransferId { get; set; }
        public decimal Amount { get; set; }
        public string TransactionType { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
    }
}
