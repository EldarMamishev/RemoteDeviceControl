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
using ViewModel.Field;
using System.Text;

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
            Device device = this.unitOfWork.DeviceRepository.GetDeviceById(deviceId);

            if (device is null)
                return Ok("Device is missing");

            var logMessage = new StringBuilder();
            logMessage.AppendLine(device.Status.ToString() ?? string.Empty);

            if (device.Status == DeviceStatus.Maintenance)
            {
                var lastConnection = device.Connections.OrderByDescending(x => x.StartDateUTC).FirstOrDefault();
                var timeLeft = lastConnection.FinishDateUTC - DateTime.Now;
                logMessage.AppendLine($"{device.Name} is on maintenance from {lastConnection.StartDateUTC.ToShortDateString()} {lastConnection.StartDateUTC.ToShortTimeString()}");
                logMessage.AppendLine($"Ends in {(int)timeLeft.TotalDays} days, {timeLeft.Hours} hours, {timeLeft.Minutes} minutes, {timeLeft.Seconds} seconds");
            }

            var log = new LogViewModel { Log = logMessage.ToString() };

            return Ok(log);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewDevice([FromBody]DeviceRequest device)
        {
            Device newDeviceEntity = this.mappersFacade.DeviceMapper.MapFromDeviceRequestToDevice(device);
            await this.unitOfWork.GetRepository<Device>().Add(newDeviceEntity);
            await this.unitOfWork.Context.SaveChangesAsync();
            var user = await _userManager.FindByNameAsync(device.UserName);

            Device response = this.unitOfWork.DeviceRepository.GetDeviceById(newDeviceEntity.Id);
            //Person user = (CurrentUser.Result as Person);
            var deviceFields = new List<DeviceField>();
            foreach (var field in response.DeviceType.Fields)
            {
                deviceFields.Add(new DeviceField()
                {
                    DeviceId = response.Id,
                    FieldId = field.Id,
                    Value = string.Empty
                });
            }

            response.DeviceFields = deviceFields;
            this.unitOfWork.DeviceRepository.Update(response);
            await this.unitOfWork.Commit();

            LogEntity logEntity = new LogEntity()
            {
                DeviceId = response.Id,
                ActionTime = DateTime.Now,
                Comments = $"Created by: Id={user?.Id} UserName={user?.UserName}{Environment.NewLine} At:{DateTime.Now.ToShortDateString()} {DateTime.Now.ToShortTimeString()}"
            };

            await this.unitOfWork.LogEntityRepository.Add(logEntity);
            await this.unitOfWork.Commit();

            return Ok(this.mappersFacade.DeviceMapper.MapFromDeviceToDeviceResponse(response));
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

        [HttpGet]
        public IActionResult GetAddDeviceFieldsData(int deviceId)
        {
            Device device = this.unitOfWork.DeviceRepository.GetDeviceById(deviceId);

            var fields = device.DeviceFields.Select(field =>
                new DeviceFieldModel()
                {
                    Id = field.Id,
                    FieldTypeId = field.Field.Id,
                    Name = field.Field.Name,
                    Type = field.Field.Type,
                    Value = field.Value
                });

            var response = new DeviceDetailsModel()
            {
                Id = device.Id,
                Name = device.Name,
                Type = device.DeviceType.Name,
                Location = device.Location.Name,
                Fields = fields
            };

            return Ok(response);
        }

        [HttpPost]
        public IActionResult AddDeviceFields(DeviceFieldListModel deviceFieldList)
        {
            Device device = this.unitOfWork.DeviceRepository.GetDeviceById(deviceFieldList.DeviceId);

            foreach (var field in deviceFieldList.Fields)
            {
                device.DeviceFields.Where(x => x.Id == field.Id).ToList().ForEach(x =>
                {
                    x.Value = field.Value;
                });
                //device.DeviceFields.Add(new DeviceField()
                //{
                //    DeviceId = deviceFieldList.DeviceId,
                //    FieldId = field.FieldTypeId,
                //    Value = field.Value
                //});
            }

            this.unitOfWork.DeviceRepository.Update(device);
            this.unitOfWork.Commit();

            return Ok();
        }
    }
}