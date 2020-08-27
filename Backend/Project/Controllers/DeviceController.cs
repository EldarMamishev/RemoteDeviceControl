using Core.Entities;
using Core.Entities.ApplicationIdentity;
using Data.Contracts.DataAccess;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
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
        public IActionResult StartConnection([FromBody] int deviceId)
        {
            LogEntity logEntity = new LogEntity()
            {
                DeviceId = deviceId,
                ActionTime = DateTime.Now
            };

            this.unitOfWork.LogEntityRepository.Update(logEntity);
            this.unitOfWork.Commit();

            return Ok(logEntity.Id);
        }

        [HttpPost]
        [Route("{logId:int}")]
        public IActionResult GetLog(int logId)
        {
            var log = this.unitOfWork.LogEntityRepository.GetById(logId);

            return Ok(log.Comments);
        }


    }
}
