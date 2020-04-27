using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Repositories.DataAccess
{
    public interface IUnitOfWork
    {
        DbContext Context { get; }

        IRepository<TEntity> GetRepository<TEntity>()
            where TEntity : class;

        Task<int> Commit();
    }
}