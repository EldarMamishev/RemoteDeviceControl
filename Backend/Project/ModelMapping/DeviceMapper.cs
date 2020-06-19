using Core.Entities;
using System;
using System.Collections.Generic;
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
                Name = device.Name 
            };
        }

        public Device MapFromDeviceRequestToDevice(DeviceRequest device)
        {
            return new Device() 
            {
                Name = device.Name 
            };
        }

        public IDictionary<string, DeviceResponse> DevicesByBuildingsMapper(IDictionary<Location, Device> deviceLocations)
        {
            var response = new Dictionary<string, DeviceResponse>();

            foreach (var n in deviceLocations)
            {
                response.Add($"{n.Key.Id}:{n.Key.Name}", this.MapFromDeviceToDeviceResponse(n.Value));
            }

            return response;
        }
    }
}
