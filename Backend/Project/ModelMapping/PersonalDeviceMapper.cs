using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViewModel.PersonalDevice;

namespace WebApi.ModelMapping
{
    public class PersonalDeviceMapper
    {
        public PersonalDeviceResponse MapFromDeviceToDeviceResponse(PersonalDevice device)
        {
            return new PersonalDeviceResponse()
            {
                Id = device.Id,
                Name = device.Name,
                PersonId = (device.PersonId) ?? 0,
                DeviceType = nameof(device.Type)
            };
        }
    }
}
