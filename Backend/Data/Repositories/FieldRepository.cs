using Core.Entities;
using Data.Contracts.DataAccess;
using Data.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories
{
    public class FieldRepository : Repository<Field>
    {
        public FieldRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }
    }
}
