using System;
using System.Threading.Tasks;

namespace Persistence.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }
        IUserInformationRepository UserInformations { get; }
        ILoginLogRepository LoginLogs { get; }
        IMessageRepository Messages { get; }
        Task Save();
    }
}