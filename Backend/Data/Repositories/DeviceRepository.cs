using Core.Entities;
using Data.Contracts.DataAccess;
using Data.Repositories.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories
{
    public class DeviceRepository : ApplicationUserRepository<Device>
    {
        public DeviceRepository(IUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
        }
    }
}
