using Core.Entities;
using Data.Contracts.DataAccess;
using Data.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Repositories
{
    public class LogEntityRepository : Repository<LogEntity>
    {
        public LogEntityRepository(IUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {

        }

        public IEnumerable<LogEntity> GetLogsByDeviceId(int deviceId)
        {
            return this.GetAsQuery().Where(d => d.Id == deviceId).ToList();
        }
    }
}
