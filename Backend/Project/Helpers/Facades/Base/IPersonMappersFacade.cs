﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.ModelMapping;

namespace WebApi.Helpers.Facades.Base
{
    public interface IPersonMappersFacade
    {
        public DeviceMapper DeviceMapper { get; }
        public PersonalDeviceMapper PersonalDeviceMapper { get; }
        public PersonMapper PersonMapper { get; }
        public LocationMapper LocationMapper{ get; }
    }
}
