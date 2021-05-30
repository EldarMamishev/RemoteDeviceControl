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
using ViewModel.Log;
using WebApi.Controllers.Base;

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

    }
}
