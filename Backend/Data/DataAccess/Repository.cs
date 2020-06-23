using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities.Base;
using Data.Contracts.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Data.DataAccess
{
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class, IBaseEntity
    {
        private readonly DbSet<TEntity> _dbSet;

        public Repository(IUnitOfWork unitOfWork)
        {
            _dbSet = unitOfWork.Context.Set<TEntity>();
        }

        public async Task Add(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public TEntity Update(TEntity entity)
        {
             return _dbSet.Update(entity).Entity;
        }

        public void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public IEnumerable<TEntity> Get()
        {
            return _dbSet.AsEnumerable();
        }

        public IQueryable<TEntity> GetAsQuery()
        {
            return _dbSet;
        }

        public TEntity GetById(int id)
        {
            return _dbSet.FirstOrDefault(e => e.Id == id);
        }

        public Task<IEnumerable<TEntity>> GetAsync()
        {
            return _dbSet.ToListAsync() as Task<IEnumerable<TEntity>>;
        }
    }
}