using System.Threading.Tasks;
using Persistence.Interfaces;

namespace Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ChatDbContext _dbContext;
        public IUserRepository Users { get; }
        public IUserInformationRepository UserInformations { get; }
        public ILoginLogRepository LoginLogs { get; }
        public IMessageRepository Messages { get; }

        public UnitOfWork(ChatDbContext dbContext, IUserRepository users, IUserInformationRepository userInformations, ILoginLogRepository loginLogs, IMessageRepository messages)
        {
            _dbContext = dbContext;
            UserInformations = userInformations;
            LoginLogs = loginLogs;
            Messages = messages;
            Users = users;
        }

        public async Task Save()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}