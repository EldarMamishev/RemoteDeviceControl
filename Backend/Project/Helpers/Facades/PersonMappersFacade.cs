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
        private PersonMapper personMapper;
        private PersonalDeviceMapper personalDeviceMapper;
        private LocationMapper locationMapper;

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

        public PersonMapper PersonMapper
        {
            get
            {
                if (this.personMapper == null)
                    this.personMapper = new PersonMapper();

                return this.personMapper;
            }
        }

        public LocationMapper LocationMapper
        {
            get
            {
                if (this.locationMapper == null)
                    this.locationMapper = new LocationMapper();

                return this.locationMapper;
            }
        }
    }
}
