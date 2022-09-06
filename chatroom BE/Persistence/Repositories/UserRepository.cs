using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;
using Microsoft.EntityFrameworkCore;
using Persistence.Interfaces;

namespace Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ChatDbContext _dbContext;

        public UserRepository(ChatDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> Get(Guid id)
        {
            return await _dbContext.Users
                .Include(user => user.UserInformation)
                .SingleOrDefaultAsync(user => user.Id == id);
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _dbContext.Users
                .Include(user => user.UserInformation)
                .ToListAsync();
        }

        public async Task Add(User entity)
        {
            await _dbContext.Users.AddAsync(entity);
        }

        public async Task Delete(User entity)
        {
            _dbContext.Users.Remove(entity);
        }

        public async Task Update(User entity)
        {
            _dbContext.Users.Update(entity);
        }

        public async Task<User> FindByUsername(string username)
        {
            return await _dbContext.Users.SingleOrDefaultAsync(user => user.Username == username);
        }
    }
}