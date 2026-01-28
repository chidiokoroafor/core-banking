using System.ComponentModel.DataAnnotations;

namespace CoreBanking.DTOs
{
    public class UserForRegistrationDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Email field is required.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        [MinLength(11, ErrorMessage ="Phone number must be 11 digits")]
        public string PhoneNumber { get; set; }

        public DateTime? DateOfBirth { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }

        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string? ConfirmPassword { get; set; }
    }
}
