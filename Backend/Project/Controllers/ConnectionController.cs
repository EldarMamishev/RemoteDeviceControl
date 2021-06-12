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
using WebApi.Controllers.Base;
using System.Text;
using System.Threading;
using ViewModel.Field;
using ViewModel.Device;

namespace WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ConnectionController : BaseController
    {
        private IUnitOfWork unitOfWork;
        private IPersonMappersFacade mappersFacade;

        public ConnectionController(UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork, IPersonMappersFacade mappersFacade)
            : base(userManager)
        {
            this.unitOfWork = unitOfWork;
            this.mappersFacade = mappersFacade;
        }

        [HttpGet]
        public async Task<IActionResult> StartConnection(int personId, int deviceId)
        {
            Person person = this.unitOfWork.PersonRepository.GetById(personId);
            Device device = this.unitOfWork.DeviceRepository.GetDeviceById(deviceId);

            //if (device.Status == DeviceStatus.Maintenance)
            //    return Ok(new LogViewModel()
            //    {
            //        Log = "Device is on maintenance"
            //    });

            Connection connectionEntity = new Connection
            {
                DeviceId = deviceId,
                PersonId = personId,
                StartDateUTC = DateTime.Now
            };

            await this.unitOfWork.ConnectionRepository.Add(connectionEntity);
            await this.unitOfWork.Commit();

            var fields = device.DeviceFields.Select(field =>
                new DeviceFieldModel()
                {
                    Id = field.Id,
                    FieldTypeId = field.Field.Id,
                    Name = field.Field.Name,
                    Type = field.Field.Type,
                    Value = field.Value
                });

            var result = new DeviceDetailsModel()
            {
                Id = device.Id,
                Name = device.Name,
                Type = device.DeviceType.Name,
                Location = device.Location.Name,
                Fields = fields
            };

            return Ok(result);
        }
        
        [HttpPost]
        public async Task<IActionResult> StartMaintenance([FromBody] ConnectionMaintenanceModel model)
        {
            Person person = this.unitOfWork.PersonRepository.GetById(model.PersonId);
            Device device = this.unitOfWork.DeviceRepository.GetDeviceById(model.DeviceId);
            int multiplier = model.TimeIdentifier == TimeIdentifiersEnum.Seconds ? 1 
                : model.TimeIdentifier == TimeIdentifiersEnum.Minutes ? 60 
                : 3600;

            var connectionEntity = new Connection
            {
                DeviceId = model.DeviceId,
                PersonId = model.PersonId,
                StartDateUTC = DateTime.Now,
                FinishDateUTC = DateTime.Now.AddSeconds((long)model.Time * multiplier)
            };

            var logEntity = new LogEntity()
            {
                DeviceId = model.DeviceId,
                ActionTime = DateTime.Now,
                Comments = $"Maintenance started by: Id={person?.Id} UserName={person?.UserName}{Environment.NewLine}" 
                    + $"Start:{connectionEntity.StartDateUTC.ToShortDateString()} {connectionEntity.StartDateUTC.ToShortTimeString()}{Environment.NewLine}"
                    + $"End:{connectionEntity.FinishDateUTC.ToShortDateString()} {connectionEntity.FinishDateUTC.ToShortTimeString()}"
            };

            device.Status = DeviceStatus.Maintenance;

            if (device.Logs == null)
                device.Logs = new List<LogEntity>();

            device.Logs.Add(logEntity);

            if (device.Connections == null)
                device.Connections = new List<Connection>();

            device.Connections.Add(connectionEntity);

            this.unitOfWork.DeviceRepository.Update(device);
            await this.unitOfWork.Commit();

            var sleepTime = new TimeSpan(0, 0, multiplier * model.Time);

            Thread.Sleep(sleepTime);

            var connectionEndLog = new LogEntity()
            {
                DeviceId = model.DeviceId,
                ActionTime = DateTime.Now,
                Comments = $"Maintenance ended at: {connectionEntity.FinishDateUTC.ToShortDateString()} {connectionEntity.FinishDateUTC.ToShortTimeString()}"
            };

            device.Status = DeviceStatus.Available;
            device.Logs.Add(connectionEndLog);

            this.unitOfWork.DeviceRepository.Update(device);
            await this.unitOfWork.Commit();

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> ConnectFromDevice([FromBody] ConnectionViewModel connection)
        {
            Person person = this.unitOfWork.PersonRepository.GetById(connection.personId);
            Device device = this.unitOfWork.DeviceRepository.GetById(connection.deviceId);

            if (device.Status != DeviceStatus.Available)
                return Ok("Device is already busy.");

            device.Status = DeviceStatus.Waiting;

            LogEntity logEntity = new LogEntity()
            {
                DeviceId = connection.deviceId,
                ActionTime = DateTime.Now,
                Comments = $"Device connected.\n"
            };

            this.unitOfWork.LogEntityRepository.Add(logEntity);
            await this.unitOfWork.Commit();

            return Ok(logEntity.Id);
        }

        [HttpGet]
        //[Route("{personId:int}")]
        public IActionResult GetActiveConnections(int personId)
        {
            IEnumerable<Connection> connections = this.unitOfWork.ConnectionRepository.GetActiveConnectionsForPerson(personId);

            return Ok(this.mappersFacade.ConnectionMapper.MapResponseFromConnections(connections));
        }

    }
}
