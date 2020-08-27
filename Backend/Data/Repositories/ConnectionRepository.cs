using Core.Entities;
using Core.Enums;
using Data.Contracts.DataAccess;
using Data.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Repositories
{
    public class ConnectionRepository : Repository<Connection>
    {
        public ConnectionRepository(IUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
        }

        public IEnumerable<Connection> GetActiveConnectionsForPerson(int personId)
        {
            return this.GetAsQuery().Where(c => c.PersonId != null && c.PersonId.Value.Equals(personId) && c.Device.Status == DeviceStatus.Waiting).ToList();
        }
    }
}
