using Core.Entities;
using Data.Contracts.DataAccess;
using Data.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories
{
    public class PersonalDeviceRepository : Repository<PersonalDevice>
    {
        public PersonalDeviceRepository(IUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
        }
    }
}
