using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Repositories.DataAccess;

namespace Data.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        public DbContext Context { get; }

        public UnitOfWork(DbContext context)
        {
            Context = context;
        }

        public IRepository<TEntity> GetRepository<TEntity>() 
            where TEntity : class
        {
            return new Repository<TEntity>(this);
        }

        public async Task<int> Commit()
        {
            return await Context.SaveChangesAsync();
        }
    }
}