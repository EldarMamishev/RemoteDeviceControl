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
using ViewModel.Location;
using WebApi.Controllers.Base;

namespace WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LocationController : BaseController
    {
        private IUnitOfWork unitOfWork;
        private IPersonMappersFacade mappersFacade;

        public LocationController(UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork, IPersonMappersFacade mappersFacade)
            : base(userManager)
        {
            this.unitOfWork = unitOfWork;
            this.mappersFacade = mappersFacade;
        }

        [HttpGet]
        public IActionResult GetLocations()
        {
            var locations = this.unitOfWork.LocationRepository.Get();

            return Ok(this.mappersFacade.LocationMapper.MapFromEntityCollection(locations));
        }

        [HttpPost]
        public async Task<IActionResult> AddLocation([FromBody]LocationRequest location)
        {
            if (!string.IsNullOrWhiteSpace(location.Name))
            {
                var newLocation = new Location()
                {
                    City = location.City,
                    Country = location.Country,
                    Name = location.Name
                };
                await this.unitOfWork.LocationRepository.Add(newLocation);
                await this.unitOfWork.Commit();

                return Ok($"Location {location.Name} created.");
            }

            return Ok("No location entered.");
        }
    }
}
