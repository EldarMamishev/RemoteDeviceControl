using Core.Entities;
using Data.Contracts.DataAccess;
using Data.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories
{
    public class AccessGroupRepository : Repository<AccessGroup>
    {
        public AccessGroupRepository(IUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
        }
    }
}
