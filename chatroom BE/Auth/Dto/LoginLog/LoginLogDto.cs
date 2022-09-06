using System;

namespace Auth.Dto.Account
{
    public class LoginLogDto
    {
        public Guid Id { get; set; }
        public string TimeStamp { get; set; }
        public string IpAddress { get; set; }
        public bool Succeeded { get; set; }
    }
}