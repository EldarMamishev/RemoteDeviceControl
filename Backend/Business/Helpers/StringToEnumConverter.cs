using Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Helpers
{
    public class StringToEnumConverter
    {
        public UserDeviceType UserDeviceTypeStringToEnumConverter(string deviceType)
        {
            switch (deviceType)
            {
                case nameof(UserDeviceType.Laptop):
                    return UserDeviceType.Laptop;
                case nameof(UserDeviceType.PC):
                    return UserDeviceType.PC;
                case nameof(UserDeviceType.Phone):
                    return UserDeviceType.Phone;
                case nameof(UserDeviceType.Tablet):
                    return UserDeviceType.Tablet;
                default:
                    return UserDeviceType.Other;

            }
        }

        public DeviceType DeviceTypeStringToEnumConverter(string deviceType)
        {
            switch (deviceType.ToLower())
            {
                case "lift":
                    return DeviceType.Lift;
                case "lock":
                    return DeviceType.Lock;
                default:
                    return DeviceType.NotSelected;

            }
        }
    }
}
