using Core.Entities.Base;
using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities.Identity
{
    [Table("AspNetUsers")]
    public class ApplicationUser : IdentityUser<int>, IBaseEntity
    {
        public string RefreshToken { get; set; }
        public DateTime ExpirationDateAuth { get; set; }
        public DateTime CreationDateUTC { get; set; }

        public ApplicationUser()
        {
            CreationDateUTC = DateTime.UtcNow;
        }
    }
}