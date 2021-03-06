﻿using System.Threading.Tasks;
using Core.Entities.Base;
using Microsoft.EntityFrameworkCore;

namespace Repositories.DataAccess
{
    public interface IUnitOfWork
    {
        DbContext Context { get; }
        IRepository<TEntity> GetRepository<TEntity>()
            where TEntity : class, IBaseEntity;
        Task<int> Commit();
    }
}