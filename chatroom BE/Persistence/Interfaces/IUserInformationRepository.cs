using System;
using System.Threading.Tasks;
using Domain;

namespace Persistence.Interfaces
{
    public interface IUserInformationRepository : IGenericRepository<UserInformation>
    {
        Task<UserInformation> FindByUserId(Guid userId);
    }
}