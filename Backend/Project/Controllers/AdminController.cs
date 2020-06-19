using Core.Entities.ApplicationIdentity;
using Data.Contracts.DataAccess;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Helpers.Facades.Base;

namespace WebApi.Controllers
{
    [ApiController]
    public class AdminController : BaseController
    {
        private IUnitOfWork unitOfWork;
        private IPersonMappersFacade mappersFacade;

        public AdminController(UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork, IPersonMappersFacade mappersFacade)
            : base(userManager)
        {
            this.unitOfWork = unitOfWork;
            this.mappersFacade = mappersFacade;
        }

        [HttpGet]
        [Route("devices")]
        public IActionResult GetAllDevices()
        {

            return Ok();
        }
    }
}
