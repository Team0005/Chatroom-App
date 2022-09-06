using System;

namespace Domain
{
    public class LoginLog
    {
        public Guid Id { get; set; }
        public DateTime TimeStamp { get; set; }
        public string IpAddress { get; set; }
        public bool Succeeded { get; set; }
        
        public User User { get; set; }
        public Guid UserId { get; set; }
        
        public LoginLog(Guid userId, string ipAddress, bool succeeded)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            TimeStamp = DateTime.Now;
            IpAddress = ipAddress;
            Succeeded = succeeded;
        }
    }
}