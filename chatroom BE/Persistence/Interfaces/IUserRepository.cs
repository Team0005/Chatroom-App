using System.Threading.Tasks;
using Domain;

namespace Persistence.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> FindByUsername(string username);
    }
}