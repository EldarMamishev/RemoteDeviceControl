using System.Threading.Tasks;
using Core.Entities.Base;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Data.Contracts.DataAccess
{
    public interface IUnitOfWork
    {
        DbContext Context { get; }
        IRepository<TEntity> GetRepository<TEntity>()
            where TEntity : class, IBaseEntity;
        Task<int> Commit();

        CommandRepository CommandRepository
        {
            get;
        }
        ConnectionRepository ConnectionRepository
        {
            get;
        }
        DeviceRepository DeviceRepository
        {
            get;
        }
        LocationRepository LocationRepository
        {
            get;
        }
        LogEntityRepository LogEntityRepository
        {
            get;
        }
        PersonalDeviceRepository PersonalDeviceRepository
        {
            get;
        }
        PersonRepository PersonRepository
        {
            get;
        }
    }
}