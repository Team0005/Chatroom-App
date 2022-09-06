using System;
using System.Collections.Generic;

namespace Domain
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public UserRole UserRole { get; set; }
        public string ImageUrl { get; set; }
        
        public UserInformation UserInformation { get; set; }
        public List<LoginLog> LoginLogs { get; set; }
        
        public User(string username, string password, UserRole userRole, string imageUrl)
        {
            Id = Guid.NewGuid();
            Username = username;
            Password = password;
            UserRole = userRole;
            ImageUrl = imageUrl;
        }
    }
}