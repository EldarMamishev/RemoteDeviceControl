using Business.Helpers;
using Core.Entities;
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
                Type = device.Type.ToString(),
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
    }
}
