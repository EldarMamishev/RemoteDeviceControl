using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities.Base;

namespace Core.Entities
{
    public class FieldPossibleValue : IBaseEntity
    {
        public int Id { get; set; }
        public int? FieldId { get; set; }
        public virtual Field Field { get; set; }
    }
}
