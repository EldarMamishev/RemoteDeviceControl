using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities.Base;

namespace Core.Entities
{
    public class AccessGroup : IBaseEntity
    {
        public int Id { get; set; }
        public string AccessIdentifiers { get; set; }
        public virtual ICollection<Person> People { get; set; }
    }
}
