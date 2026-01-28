using System.Security.Cryptography.Xml;

namespace CoreBanking.Entites
{
    public class Transaction
    {
        public Guid Id { get; set; }
        public Guid AccountId { get; set; }
        public Guid? TransferId { get; set; }
        public decimal Amount { get; set; }
        public string TransactionType { get; set; } = null!;
        public DateTime CreatedAt { get; set; }

        public Account Account { get; set; } = null!;
        public Transfer? Transfer { get; set; }
    }
}
