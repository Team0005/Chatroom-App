using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.EntityFrameworkCore;
using Persistence.Interfaces;

namespace Persistence.Repositories
{
    public class LoginLogRepository : ILoginLogRepository
    {
        private readonly ChatDbContext _dbContext;

        public LoginLogRepository(ChatDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<LoginLog> Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<LoginLog>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task Add(LoginLog entity)
        {
            await _dbContext.LoginLogs.AddAsync(entity);
        }

        public Task Delete(LoginLog entity)
        {
            throw new NotImplementedException();
        }

        public Task Update(LoginLog entity)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<LoginLog>> FindAllByUserId(Guid userId)
        {
            return await _dbContext.LoginLogs.Where(loginLog => loginLog.UserId == userId).ToListAsync();
        }
    }
}