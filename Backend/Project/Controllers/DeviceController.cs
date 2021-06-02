using Core.Entities;
using Core.Entities.ApplicationIdentity;
using Core.Enums;
using Data.Contracts.DataAccess;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ViewModel.Connection;
using Services.Facades.Base;
using ViewModel.LogEntity;
using ViewModel.Device;
using ViewModel.Shared;
using WebApi.Controllers.Base;

namespace WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DeviceController : BaseController
    {
        private IUnitOfWork unitOfWork;
        private IPersonMappersFacade mappersFacade;

        public DeviceController(UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork, IPersonMappersFacade mappersFacade)
            : base(userManager)
        {
            this.unitOfWork = unitOfWork;
            this.mappersFacade = mappersFacade;
        }

        //[HttpGet]
        //[Route("{personId:int}")]
        //public IActionResult CheckPerson(int personId)
        //{
        //    Person person = this.unitOfWork.PersonRepository.GetById(personId);
        //    if (person is null)
        //        return BadRequest();

        //    return Ok(personId);
        //}

        [HttpGet]
        public IActionResult GetAllDevices()
        {
            IEnumerable<Device> allDevices = this.unitOfWork.DeviceRepository.Get();

            return Ok(this.mappersFacade.DeviceMapper.MapFromDevicesToDeviceResponses(allDevices));
        }

        [HttpGet]
        public IActionResult GetAllDevicesPerLocations()
        {
            var devices = this.mappersFacade.DeviceMapper.DevicesByBuildingsMapperAdmin(this.unitOfWork.DeviceRepository.GetAllDevicesPerLocations(), this.unitOfWork);

            return Ok(devices);
        }

        [HttpPost]
        public IActionResult GetCurrentState([FromBody] int deviceId)
        {
            Device device = this.unitOfWork.DeviceRepository.GetById(deviceId);
            if (device is null)
                return Ok("Device is missing");

            return Ok(new LogViewModel { Log = device.ActiveState ?? string.Empty });
        }

        [HttpPost]
        public async Task<IActionResult> AddNewDevice([FromBody]DeviceRequest device)
        {
            Device newDeviceEntity = this.mappersFacade.DeviceMapper.MapFromDeviceRequestToDevice(device);
            await this.unitOfWork.GetRepository<Device>().Add(newDeviceEntity);
            await this.unitOfWork.Context.SaveChangesAsync();

            return Ok(this.mappersFacade.DeviceMapper.MapFromDeviceToDeviceResponse(newDeviceEntity));
        }

        [HttpPost]
        public async Task<IActionResult> UpdateDevice([FromBody]DeviceWithIdRequest device)
        {
            Device newDeviceEntity = this.unitOfWork.DeviceRepository.GetById(device.Id);
            newDeviceEntity.Name = string.IsNullOrWhiteSpace(device.Name) ? newDeviceEntity.Name : device.Name;
            this.unitOfWork.DeviceRepository.Update(newDeviceEntity);
            await this.unitOfWork.Commit();

            return Ok(device);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteDevice([FromBody]SingleIdRequest deviceId)
        {
            Device newDeviceEntity = this.unitOfWork.DeviceRepository.GetById(deviceId.Id);
            this.unitOfWork.DeviceRepository.Delete(newDeviceEntity);
            await this.unitOfWork.Commit();

            return Ok("Deleted");
        }
    }
}