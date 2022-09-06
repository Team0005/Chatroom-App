using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class UserInformation
    {
        public Guid Id { get; set; }    
        public string Nationality { get; set; }
        public string Phone { get; set; }
        public DateTime BirthDate { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }
        
        public UserInformation(string nationality, string phone, DateTime birthDate, Guid userId)
        {
            Id = Guid.NewGuid();
            Nationality = nationality;
            Phone = phone;
            BirthDate = birthDate;
            UserId = userId;
        }
    }
}