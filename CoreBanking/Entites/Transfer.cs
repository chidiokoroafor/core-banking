namespace CoreBanking.Entites
{
    public class Transfer
    {
        public Guid Id { get; set; }
        public Guid FromAccountId { get; set; }
        public Guid ToAccountId { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; } = "Pending";
        //public string IdempotencyKey { get; set; } = null!;
        public string Reference { get; set; } = null!;
        public DateTime CreatedAt { get; set; }

        public Account FromAccount { get; set; } = null!;
        public Account ToAccount { get; set; } = null!;
    }
}
