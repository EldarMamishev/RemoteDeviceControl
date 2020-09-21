using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Services.Facades.Base;
using Services.ModelMapping;

namespace Services.Facades
{
    public class PersonMappersFacade : IPersonMappersFacade
    {
        private DeviceMapper deviceMapper;
        private PersonMapper personMapper;
        private LocationMapper locationMapper;
        private LogsMapper logsMapper;
        private ConnectionMapper connectionMapper;

        public ConnectionMapper ConnectionMapper
        {
            get
            {
                if (this.connectionMapper == null)
                    this.connectionMapper = new ConnectionMapper();

                return this.connectionMapper;
            }
        }

        public DeviceMapper DeviceMapper
        {
            get
            {
                if (this.deviceMapper == null)
                    this.deviceMapper = new DeviceMapper();

                return this.deviceMapper;
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

        public LogsMapper LogsMapper
        {
            get
            {
                if (this.logsMapper == null)
                    this.logsMapper = new LogsMapper();

                return this.logsMapper;
            }
        }
    }
}
