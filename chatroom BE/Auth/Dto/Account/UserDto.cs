using System;
using Domain;

namespace Auth.Dto.Account
{
    public class UserDto
    {
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public UserRole UserRole { get; set; }
        public string ImageUrl { get; set; }
        public string UserRoleString { get; set; }
        public string Phone { get; set; }
        public string Nationality { get; set; }
    }
}