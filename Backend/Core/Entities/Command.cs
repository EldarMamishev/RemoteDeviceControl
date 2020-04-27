using Core.Entities.Base;
using Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities
{
    public class Command : IBaseEntity
    {
        public int Id { get; set; }
        public virtual Connection Connection { get; set; }
        public int? ConnectionId { get; set; }
        public CommandType CommandType { get; set; }
        public string MongoCommandDetailsId { get; set; }
    }
}
