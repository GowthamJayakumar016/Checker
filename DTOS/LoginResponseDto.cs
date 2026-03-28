namespace HacakthanChecker.DTOs
{
    public class LoginResponseDto
    {
        public string Token { get; set; }   // JWT Token
        public string Role { get; set; }    // "User" or "Admin"
    }
}