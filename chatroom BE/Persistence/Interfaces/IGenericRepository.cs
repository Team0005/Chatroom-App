using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Persistence.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> Get(Guid id);
        Task<IEnumerable<T>> GetAll();
        Task Add(T entity);
        Task Delete(T entity);
        Task Update(T entity);
    }
}