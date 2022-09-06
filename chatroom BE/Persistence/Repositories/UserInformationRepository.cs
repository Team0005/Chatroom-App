using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.EntityFrameworkCore;
using Persistence.Interfaces;

namespace Persistence.Repositories
{
    public class UserInformationRepository : IUserInformationRepository
    {
        private readonly ChatDbContext _dbContext;

        public UserInformationRepository(ChatDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public Task<UserInformation> Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<UserInformation>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task Add(UserInformation entity)
        {
            await _dbContext.UserInformations.AddAsync(entity);
        }

        public async Task Delete(UserInformation entity)
        {
            throw new NotImplementedException();
        }

        public async Task Update(UserInformation entity)
        {
            _dbContext.UserInformations.Update(entity);
        }

        public async Task<UserInformation> FindByUserId(Guid userId)
        {
            return await _dbContext.UserInformations
                .SingleOrDefaultAsync(userInformation => userInformation.UserId == userId);
        }
    }
}