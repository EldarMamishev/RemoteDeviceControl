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
        void Save();

        bool Backup();

        public AccessGroupRepository AccessGroupRepository
        {
            get;
        }

        public CommandRepository CommandRepository
        {
            get;
        }

        public CommandTypeRepository CommandTypeRepository
        {
            get;
        }

        public ConnectionRepository ConnectionRepository
        {
            get;
        }

        public DeviceRepository DeviceRepository
        {
            get;
        }

        public DeviceTypeRepository DeviceTypeRepository
        {
            get;
        }

        public FieldRepository FieldRepository
        {
            get;
        }

        public LocationRepository LocationRepository
        {
            get;
        }

        public LogEntityRepository LogEntityRepository
        {
            get;
        }

        public PersonRepository PersonRepository
        {
            get;
        }
    }
}