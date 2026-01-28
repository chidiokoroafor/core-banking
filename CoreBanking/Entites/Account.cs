namespace CoreBanking.Entites
{
    public class Account
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public string AccountNumber { get; set; } = null!;
        public decimal Balance { get; set; }
        public string Status { get; set; } = "Active";
        public string AccountType { get; set; }
        public DateTime CreatedAt { get; set; }

        public byte[] RowVersion { get; set; } = null!;

        public Customer Customer { get; set; } = null!;
        public ICollection<Transaction> LedgerEntries { get; set; } = new List<Transaction>();
    }
}
