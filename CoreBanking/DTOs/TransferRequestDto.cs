namespace CoreBanking.DTOs
{
    public class TransferRequestDto
    {
        public string SourceAccountNumber { get; set; } = null!;
        public string DestinationAccountNumber { get; set; } = null!;
        public decimal Amount { get; set; }
    }
}
