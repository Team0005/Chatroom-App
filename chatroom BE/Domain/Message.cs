using System;

namespace Domain
{
    public class Message
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public DateTime TimeStamp { get; set; }
        
        public Guid SenderId { get; set; }
        public User Sender { get; set; }
        
        public Guid ReceiverId { get; set; }
        public User Receiver { get; set; }
        
        
        public Message(Guid senderId, Guid receiverId, string content)
        {
            Id = Guid.NewGuid();
            TimeStamp = DateTime.Now;
            Content = content;
            SenderId = senderId;
            ReceiverId = receiverId;
        }
    }
}