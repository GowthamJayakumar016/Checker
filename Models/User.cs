using System.ComponentModel.DataAnnotations;

namespace HackathanChecker.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }  // matches AuthService

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        public string Role { get; set; } = "User"; // Default

        public int? HotelId { get; set; } // For Admin only
    }
}