using System;
using System.ComponentModel.DataAnnotations;
using Domain;

namespace Auth.Dto.Account
{
    public class RegisterDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public UserRole UserRole { get; set; }
        public string Nationality { get; set; }
        public string Phone { get; set; }
        public DateTime BirthDate { get; set; }
        public string ImageUrl { get; set; }
    }
}