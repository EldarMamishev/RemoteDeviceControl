using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Contracts.DataAccess
{
    public interface IRepository<TEntity>
        where TEntity : class
    {
        Task Add(TEntity entity);
        void Delete(TEntity entity);
        IEnumerable<TEntity> Get();
    }
}