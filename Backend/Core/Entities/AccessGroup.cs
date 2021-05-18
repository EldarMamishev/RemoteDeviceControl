using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    class AccessGroup
    {
        public int Id { get; set; }
        public string AccessIdentifiers { get; set; }
        public virtual ICollection<Person> People { get; set; }
    }
}
