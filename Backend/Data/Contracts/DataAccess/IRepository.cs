using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Contracts.DataAccess
{
    public interface IRepository<TEntity>
        where TEntity : class
    {
        Task Add(TEntity entity);
        void AddSync(TEntity entity);
        void Delete(TEntity entity);
        IEnumerable<TEntity> Get();
        TEntity GetById(int id);
        Task<IEnumerable<TEntity>> GetAsync();
        IQueryable<TEntity> GetAsQuery();
    }
}