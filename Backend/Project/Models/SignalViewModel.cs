using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public struct SignalViewModel
    {
        public string CustomerName { get; set; }
        public string Description { get; set; }
        public string Area { get; set; }
        public string Zone { get; set; }

        public string SignalStamp { get; set; }
    }
}
