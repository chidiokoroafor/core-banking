namespace CoreBanking.DTOs
{
    public class AccountStatementDto
    {
        public string AccountNumber { get; set; } = default!;
        public decimal OpeningBalance { get; set; }
        public decimal ClosingBalance { get; set; }
        public List<StatementLineDto> Transactions { get; set; } = new();
    }
}
