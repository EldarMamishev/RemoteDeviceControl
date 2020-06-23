using Core.Entities;
using Data.Contracts.DataAccess;
using Data.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Repositories
{
    public class LocationRepository : Repository<Location>
    {
        public LocationRepository(IUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
        }

        //public IDictionary<Location, Device> GetAllDevicesPerLocations()
        //{
        //    return this.GetAsQuery().ToDictionary();
        //}
    }
}
