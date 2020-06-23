using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Filters
{
    public class BuildingsFilter
    {
        public string Country { get; set; }
        public string City { get; set; }
        public string Building { get; set; }
        public int DevicesAmountFrom { get; set; }
        public int DevicesAmountTo { get; set; }
    }
}
