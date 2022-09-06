using System;

namespace Auth.Dto.Account
{
    public class LoginResponseDto
    {
        public string Jwt { get; set; }
        public string UserRole { get; set; }
        public string Username { get; set; }
        public Guid UserId { get; set; } 
        public string ImageUrl { get; set; } 
    }
}