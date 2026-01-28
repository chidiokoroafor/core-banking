namespace CoreBanking.DTOs
{
    public class TransferResponseDto
    {
        public string Reference { get; set; } = null!;
        public string SourceAccount { get; set; } = null!;
        public string DestinationAccount { get; set; } = null!;
        public decimal Amount { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
