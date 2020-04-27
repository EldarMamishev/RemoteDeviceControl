using Core.Entities.Base;
using Microsoft.AspNetCore.Identity;
using System;

namespace Core.Entities.ApplicationIdentity
{
    public class ApplicationRole : IdentityRole<int>, IBaseEntity
    {
        #region Properties
        public DateTime CreationDateUTC { get; set; }
        #endregion

        #region Constructors
        public ApplicationRole() : base()
        {
            CreationDateUTC = DateTime.UtcNow;
        }
        public ApplicationRole(string name) : base(name)
        {
            CreationDateUTC = DateTime.UtcNow;
        }
        #endregion
    }
}