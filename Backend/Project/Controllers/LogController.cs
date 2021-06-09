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

namespace WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LogController : BaseController
    {
        private IUnitOfWork unitOfWork;
        private IPersonMappersFacade mappersFacade;

        public LogController(UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork, IPersonMappersFacade mappersFacade)
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

        [HttpPost]
        [Route("{logId:int}")]
        public IActionResult GetLog(int logId)
        {
            var log = this.unitOfWork.LogEntityRepository.GetById(logId);

            return Ok(log.Comments);
        }

        [HttpGet]
        public IActionResult GetLogsForDevice(int deviceId)
        {
            var result = new LogListModel();
            var logs = this.unitOfWork.LogEntityRepository.GetLogsByDeviceId(deviceId);
            try
            {
                result.Logs = logs.Select(x => x.Comments).ToList();
            }
            catch
            {
                return Ok("No logs for current device");
            }

            return Ok(result);
        }

    }
}
