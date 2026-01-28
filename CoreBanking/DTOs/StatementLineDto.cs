namespace CoreBanking.DTOs
{
    public class StatementLineDto
    {
        public DateTime Date { get; set; }
        public string Reference { get; set; } = default!;
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
        public decimal Balance { get; set; }
    }
}
