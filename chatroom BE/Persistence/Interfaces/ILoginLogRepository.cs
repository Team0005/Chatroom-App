using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;

namespace Persistence.Interfaces
{
    public interface ILoginLogRepository : IGenericRepository<LoginLog>
    {
        Task<IEnumerable<LoginLog>> FindAllByUserId(Guid userId);
    }
}