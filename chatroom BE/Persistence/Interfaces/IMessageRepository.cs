using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;

namespace Persistence.Interfaces
{
    public interface IMessageRepository : IGenericRepository<Message>
    {
        Task<IEnumerable<Message>> GetBySenderIdAndReceiverId(Guid senderId, Guid receiverId);
    }
}