using Core.Entities.ApplicationIdentity;
using Data.Contracts.DataAccess;
using Data.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories.Identity
{
    public class ApplicationUserRepository<TIdentityUser> : Repository<TIdentityUser>
        where TIdentityUser : ApplicationUser
    {
        public ApplicationUserRepository(IUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
        }
    }
}
