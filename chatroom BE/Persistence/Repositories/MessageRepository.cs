using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.EntityFrameworkCore;
using Persistence.Interfaces;

namespace Persistence.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly ChatDbContext _dbContext;

        public MessageRepository(ChatDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task<Message> Get(Guid id)
        {
            return await _dbContext.Messages.FindAsync(id);
        }

        public async Task<IEnumerable<Message>> GetAll()
        {
            return await _dbContext.Messages.ToListAsync();
        }

        public async Task Add(Message entity)
        {
            await _dbContext.Messages.AddAsync(entity);
        }

        public async Task Delete(Message entity)
        {
            _dbContext.Messages.Update(entity);
        }

        public async Task Update(Message entity)
        {
            _dbContext.Messages.Update(entity);
        }

        public async Task<IEnumerable<Message>> GetBySenderIdAndReceiverId(Guid senderId, Guid receiverId)
        {
            return await _dbContext.Messages
                .Include(message => message.Sender)
                .Include(message => message.Receiver)
                .Where(message =>
                    (message.SenderId == senderId && message.ReceiverId == receiverId) ||
                    (message.SenderId == receiverId && message.ReceiverId == senderId))
                .ToListAsync();
        }
    }
}