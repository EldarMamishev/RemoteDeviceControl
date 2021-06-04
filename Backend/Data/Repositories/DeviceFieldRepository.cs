using Core.Entities;
using Data.Contracts.DataAccess;
using Data.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Repositories
{
    public class DeviceFieldRepository : Repository<DeviceField>
    {
        public DeviceFieldRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public IEnumerable<DeviceField> GetDeviceFieldsByDeviceId(int deviceId)
        {
            return this.GetAsQuery()
                .Where(x => x.DeviceId == deviceId)
                .Include(d => d.Field)
                .ToList();
        }
    }
}
