using System;

namespace Auth.Dto.Message
{
    public class CreateMessageDto
    {
        public Guid ReceiverId { get; set; }
        public string Content { get; set; }
    }
}