using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities.Base;
using Core.Enums;

namespace Core.Entities
{
    public class CommandType : IBaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Command> Commands { get; set; }
        public virtual ICollection<FieldCommandType> FieldCommandTypes { get; set; }
    }
}
