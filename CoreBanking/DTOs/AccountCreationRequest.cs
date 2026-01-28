namespace CoreBanking.DTOs
{
    public class AccountCreationRequest
    {
        public string accountType { get; set; }
        public decimal amount { get; set; }
    }
}
