﻿//using Business.Filters;
using Core.Entities;
using Data.Contracts.DataAccess;
using Data.DataAccess;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Data.Repositories
{
    public class DeviceRepository : Repository<Device>
    {
        public DeviceRepository(IUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
        }

        public IDictionary<Location, List<Device>> GetAllDevicesPerLocations()
        {
            return this.GetAsQuery().Include(d => d.Location).ToList().OrderBy(x => x.LocationId).GroupBy(d => d.Location).ToDictionary(d => d.Key, d => d.ToList()).ToDictionary(x => x.Key, x => x.Value);
        }

        public Device GetDeviceById(int id)
        {
            return this.GetAsQuery()
                .Include(d => d.DeviceType.Fields)
                .Include(d => d.Location)
                .Include(x => x.Logs)
                .Include(x => x.Connections)
                .Include(x => x.DeviceFields)
                .ThenInclude(x => x.Field)
                .FirstOrDefault(x => x.Id == id);
        }

        //public IDictionary<Location, Device> GetFilteredDevicesPerLocations(BuildingsFilter filter)
        //{
        //    return this.GetAsQuery().Where(d => 
        //        (d.Location.City.Equals(filter.City) || string.IsNullOrWhiteSpace(filter.City)) 
        //        && (d.Location.Country.Equals(filter.Country) || string.IsNullOrWhiteSpace(filter.Country)) 
        //        && (d.Location.Name.Equals(filter.Building) || string.IsNullOrWhiteSpace(filter.Building))
        //        && (d.Location.Devices.Count >= filter.DevicesAmountFrom && d.Location.Devices.Count <= filter.DevicesAmountTo))
        //        .ToDictionary(d => d.Location);
        //}

        public IEnumerable<Device> GetDevicesByLocationId(int locationId)
        {
            return this.GetAsQuery().Where(d => d.LocationId != null && d.LocationId.Value.Equals(locationId)).ToList();
        }
    }
}
