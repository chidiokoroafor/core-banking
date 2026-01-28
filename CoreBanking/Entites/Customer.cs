using Microsoft.AspNetCore.Identity;

namespace CoreBanking.Entites
{
    public class Customer
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string PhoneNumber { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<Account> Accounts { get; set; } = new List<Account>();
    }
}
