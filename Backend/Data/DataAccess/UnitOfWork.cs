﻿using System;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
using Core.Entities.Base;
using Data.Contracts.DataAccess;
using Data.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Data.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private CommandRepository commandRepository;
        private ConnectionRepository connectionRepository;
        private DeviceRepository deviceRepository;
        private LocationRepository locationRepository;
        private LogEntityRepository logEntityRepository;
        private PersonRepository personRepository;


        public DbContext Context { get; }

        public bool Backup()
        {
            string dbName = "RemoteDeviceControlDb";
            try
            {
                var path = @"E:\dbBackup\";

                var query = new StringBuilder();

                query.AppendLine($"USE {dbName}");
                query.AppendLine($"BACKUP DATABASE {dbName} TO DISK = '{path}{dbName}{DateTime.Now.Month}{DateTime.Now.Day}_{DateTime.Now.Ticks}.Bak' WITH FORMAT");

                this.Context.Database.ExecuteSqlCommand(query.ToString());

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public CommandRepository CommandRepository
        {
            get
            {
                if (this.commandRepository is null)
                    this.commandRepository = new CommandRepository(this);

                return this.commandRepository;
            }
        }
        public ConnectionRepository ConnectionRepository
        {
            get
            {
                if (this.connectionRepository is null)
                    this.connectionRepository = new ConnectionRepository(this);

                return this.connectionRepository;
            }
        }
        public DeviceRepository DeviceRepository
        {
            get
            {
                if (this.deviceRepository is null)
                    this.deviceRepository = new DeviceRepository(this);

                return this.deviceRepository;
            }
        }
        public LocationRepository LocationRepository
        {
            get
            {
                if (this.locationRepository is null)
                    this.locationRepository = new LocationRepository(this);

                return this.locationRepository;
            }
        }
        public LogEntityRepository LogEntityRepository
        {
            get
            {
                if (this.logEntityRepository is null)
                    this.logEntityRepository = new LogEntityRepository(this);

                return this.logEntityRepository;
            }
        }
        public PersonRepository PersonRepository
        {
            get
            {
                if (this.personRepository is null)
                    this.personRepository = new PersonRepository(this);

                return this.personRepository;
            }
        }

        public UnitOfWork(DbContext context)
        {
            Context = context;
        }

        public async Task<int> Commit()
        {
            return await Context.SaveChangesAsync();
        }

        public void Save()
        {
            Context.SaveChanges();
        }

        public IRepository<TEntity> GetRepository<TEntity>() 
            where TEntity : class, IBaseEntity
        {
            switch (typeof(TEntity).Name + "Repository")
            {
                case nameof(this.PersonRepository):
                    return this.PersonRepository as IRepository<TEntity>;

                case nameof(this.DeviceRepository):
                    return this.DeviceRepository as IRepository<TEntity>;

                case nameof(this.ConnectionRepository):
                    return this.ConnectionRepository as IRepository<TEntity>;

                case nameof(this.CommandRepository):
                    return this.CommandRepository as IRepository<TEntity>;

                case nameof(this.LogEntityRepository):
                    return this.LogEntityRepository as IRepository<TEntity>;

                case nameof(this.LocationRepository):
                    return this.LocationRepository as IRepository<TEntity>;

                default:
                    return new Repository<TEntity>(this);
            }
        }
    }
}