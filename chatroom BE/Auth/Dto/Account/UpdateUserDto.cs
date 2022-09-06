using System.ComponentModel.DataAnnotations;

namespace Auth.Dto.Account
{
    public class UpdateUserDto
    {
        public string Password { get; set; }
        public string Nationality { get; set; }
        public string Phone { get; set; }
        public string ImageUrl { get; set; }
    }
}