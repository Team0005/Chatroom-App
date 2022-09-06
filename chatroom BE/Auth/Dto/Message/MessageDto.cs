using System;
using Domain;

namespace Auth.Dto.Message
{
    public class MessageDto
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Username { get; set; }
        public string UserImageUrl { get; set; }
    }
}