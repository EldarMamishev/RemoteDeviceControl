using Business.Helpers;
using Core.Entities;
using Data.Contracts.DataAccess;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using ViewModel.Device;

namespace WebApi.ModelMapping
{
    public class DeviceMapper
    {
        public IEnumerable<DeviceResponse> MapFromDevicesToDeviceResponses(IEnumerable<Device> devices)
        {
            List<DeviceResponse> result = new List<DeviceResponse>();

            foreach (var device in devices)
            {
                result.Add(this.MapFromDeviceToDeviceResponse(device));
            }

            return result;
        }

        public DeviceResponse MapFromDeviceToDeviceResponse(Device device)
        {
            return new DeviceResponse()
            {
                Id = device.Id,
                Name = device.Name,
                Type = device.Type.ToString().ToLower(),
                LocationId = device.LocationId,
                LocationName = $"{device.Location?.Country} - {device.Location?.City} - {device.Location?.Name}"
            };
        }

        public Device MapFromDeviceRequestToDevice(DeviceRequest device)
        {
            var helper = new StringToEnumConverter();

            return new Device()
            {
                Name = device.Name,
                LocationId = device.LocationId,
                Type = helper.DeviceTypeStringToEnumConverter(device.Type),
            };
        }

        public IEnumerable<KeyValuePair<string, IEnumerable<DeviceResponse>>> DevicesByBuildingsMapper(IDictionary<Location, List<Device>> deviceLocations)
        {
            var response = new Dictionary<string, IEnumerable<DeviceResponse>>();

            foreach (var n in deviceLocations)
            {
                response.Add($"{n.Key.Id}:{n.Key.Name}", this.MapFromDevicesToDeviceResponses(n.Value));
            }

            return response.ToImmutableList();
        }

        public IEnumerable<KeyValuePair<string, IEnumerable<DeviceResponse>>> DevicesByBuildingsMapperAdmin(IDictionary<Location, List<Device>> deviceLocations, IUnitOfWork unitOfWork)
        {
            var response = new Dictionary<string, IEnumerable<DeviceResponse>>();
            unitOfWork.LocationRepository.Get().Where(location => !deviceLocations.Keys.Any(key => key.Id == location.Id))
                .ToList().ForEach(n => response.Add($"{n.Id}:{n.Name}", new List<DeviceResponse>() 
                { 
                    new DeviceResponse() 
                    { 
                        LocationId = n.Id, 
                        LocationName = n.Name
                    } 
                }));

            foreach (var n in deviceLocations)
            {
                response.Add($"{n.Key.Id}:{n.Key.Name}", this.MapFromDevicesToDeviceResponses(n.Value));
            }

            //foreach (var n in unitOfWork.LocationRepository.Get().ToList())
            //{
            //    response.Add($"{n.Id}:{n.Name}", new List<DeviceResponse>());
            //}

            return response.ToImmutableList();
        }
    }
}
