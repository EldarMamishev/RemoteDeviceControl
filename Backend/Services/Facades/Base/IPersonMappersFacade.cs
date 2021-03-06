﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.ModelMapping;

namespace Services.Facades.Base
{
    public interface IPersonMappersFacade
    {
        public DeviceMapper DeviceMapper { get; }
        public PersonMapper PersonMapper { get; }
        public LocationMapper LocationMapper{ get; }
        public LogsMapper LogsMapper { get; }
        public ConnectionMapper ConnectionMapper{ get; }
    }
}
