using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities.Base;
using Core.Enums;

namespace Core.Entities
{
    public class FieldCommandType : IBaseEntity
    {
        public int Id { get; set; }
        public int? CommandTypeId { get; set; }
        public virtual CommandType CommandType { get; set; }
        public int? FieldId { get; set; }
        public virtual Field Field { get; set; }
        public ActionType? DefaultAction { get; set; }
        public int? DefaultDelta { get; set; }
    }
}
