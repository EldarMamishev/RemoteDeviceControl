using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities;
using Data.Contracts.DataAccess;
using Data.DataAccess;

namespace Data.Repositories
{
    public class DeviceTypeRepository : Repository<DeviceType>
    {
        public DeviceTypeRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }
    }
}
