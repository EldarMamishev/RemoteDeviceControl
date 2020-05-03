using Core.Entities;
using Data.Contracts.DataAccess;
using Data.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories
{
    public class ConnectionRepository : Repository<Connection>
    {
        public ConnectionRepository(IUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
        }
    }
}
