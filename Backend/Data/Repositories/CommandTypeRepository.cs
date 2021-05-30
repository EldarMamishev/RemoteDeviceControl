using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities;
using Data.Contracts.DataAccess;
using Data.DataAccess;

namespace Data.Repositories
{
    public class CommandTypeRepository : Repository<CommandType>
    {
        public CommandTypeRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }
    }
}
