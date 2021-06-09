using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel.Device
{
    public class DeviceResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string LocationName { get; set; }
        public int? LocationId { get; set; }
    }

    //public class ValueNamePair<TValue, TName>
    //{
    //    public TValue Value { get; set; }
    //    public TName Name { get; set; }
    //}
}
