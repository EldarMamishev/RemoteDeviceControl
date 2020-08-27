using Core.Entities;
using Data.Contracts.DataAccess;
using Data.Repositories.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Repositories
{
    public class PersonRepository : ApplicationUserRepository<Person>
    {
        public PersonRepository(IUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
        }

        public bool IsPersonExist(int personId)
        {
            return this.GetAsQuery().Any(p => p.Id == personId);
        }
    }
}
