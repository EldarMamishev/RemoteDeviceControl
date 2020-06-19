using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Helpers.Facades.Base;
using WebApi.ModelMapping;

namespace WebApi.Helpers.Facades
{
    public class PersonMappersFacade : IPersonMappersFacade
    {
        private DeviceMapper deviceMapper;
        private PersonalDeviceMapper personalDeviceMapper;

        public DeviceMapper DeviceMapper
        {
            get
            {
                if (this.deviceMapper == null)
                    this.deviceMapper = new DeviceMapper();

                return this.deviceMapper;
            }
        }

        public PersonalDeviceMapper PersonalDeviceMapper
        {
            get
            {
                if (this.personalDeviceMapper == null)
                    this.personalDeviceMapper = new PersonalDeviceMapper();

                return this.personalDeviceMapper;
            }
        }
    }
}
