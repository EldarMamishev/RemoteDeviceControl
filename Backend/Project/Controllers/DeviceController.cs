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
using WebApi.Helpers.Facades.Base;

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

        [HttpPost]
        public async Task<IActionResult> StartConnection([FromBody] ConnectionViewModel connection)
        {
            Person person = this.unitOfWork.PersonRepository.GetById(connection.personId);
            Device device = this.unitOfWork.DeviceRepository.GetById(connection.deviceId);

            if (device.Status != DeviceStatus.Sleeping)
                return Ok("Device is already busy.");

            device.Status = DeviceStatus.Waiting;

            LogEntity logEntity = new LogEntity()
            {
                DeviceId = connection.deviceId,
                ActionTime = DateTime.Now,
                Comments = $"Connection started by {person.FirstName} {person.LastName}" + Environment.NewLine + $"E-mail: {person.Email}" 
                    + Environment.NewLine + DateTime.Now.ToShortDateString() + Environment.NewLine + DateTime.Now.ToShortTimeString() + Environment.NewLine
            };

            Connection connectionEntity = new Connection
            {
                DeviceId = connection.deviceId,
                PersonId = connection.personId,
                StartDateUTC = DateTime.Now
            };

            await this.unitOfWork.LogEntityRepository.Add(logEntity);
            await this.unitOfWork.Commit();
            await this.unitOfWork.ConnectionRepository.Add(connectionEntity);
            await this.unitOfWork.Commit();
            this.unitOfWork.DeviceRepository.Update(device);
            await this.unitOfWork.Commit();

            return Ok(connection);
        }

        [HttpPost]
        public async Task<IActionResult> ConnectFromDevice([FromBody] ConnectionViewModel connection)
        {
            Person person = this.unitOfWork.PersonRepository.GetById(connection.personId);
            Device device = this.unitOfWork.DeviceRepository.GetById(connection.deviceId);

            if (device.Status != DeviceStatus.Sleeping)
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

        [HttpGet]
        [Route("{personId:int}")]
        public IActionResult CheckPerson(int personId)
        {
            Person person = this.unitOfWork.PersonRepository.GetById(personId);
            if (person is null)
                return BadRequest();

            return Ok(personId);
        }

        [HttpPost]
        [Route("{logId:int}")]
        public IActionResult GetLog(int logId)
        {
            var log = this.unitOfWork.LogEntityRepository.GetById(logId);

            return Ok(log.Comments);
        }

        [HttpPost]
        public IActionResult GetLogsForDevice([FromBody] int deviceId)
        {
            string result;
            try
            {
                result = this.mappersFacade.LogsMapper.MapStringFromLogs(this.unitOfWork.LogEntityRepository.GetLogsByDeviceId(deviceId));
            }
            catch
            {
                result = "No logs for current device";
            }

            return Ok(new LogViewModel { Log = result ?? string.Empty });
        }

        [HttpPost]
        public IActionResult GetCurrentState([FromBody] int deviceId)
        {
            Device device = this.unitOfWork.DeviceRepository.GetById(deviceId);
            if (device is null)
                return Ok("Device is missing");

            return Ok(new LogViewModel { Log = device.ActiveState ?? string.Empty });
        }


    }

    public class LogViewModel
    {
        public string Log { get; set; }
    }
}