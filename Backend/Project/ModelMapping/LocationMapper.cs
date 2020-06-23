using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViewModel.Location;

namespace WebApi.ModelMapping
{
    public class LocationMapper
    {
        public LocationResponse MapFromEntity(Location location)
        {
            return new LocationResponse
            {
                Id = location.Id,
                City = location.City,
                Country = location.Country,
                Name = location.Name
            };
        }

        public IEnumerable<LocationResponse> MapFromEntityCollection(IEnumerable<Location> locations)
        {
            List<LocationResponse> result = new List<LocationResponse>();

            foreach(var location in locations)
            {
                result.Add(this.MapFromEntity(location));
            }

            return result;
        }
    }
}
