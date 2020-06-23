using Business.Filters;
using Core.Entities;
using Core.Entities.ApplicationIdentity;
using Data.Contracts.DataAccess;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViewModel.Device;
using ViewModel.Location;
using ViewModel.Person;
using WebApi.Helpers.Facades.Base;

namespace WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
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
        public IActionResult GetAllDevicesPerLocations()
        {
            var devices = this.mappersFacade.DeviceMapper.DevicesByBuildingsMapper(this.unitOfWork.DeviceRepository.GetAllDevicesPerLocations());

            return Ok(devices);
        }

        [HttpGet]
        public IActionResult GetAllDevices()
        {
            var devices = this.mappersFacade.DeviceMapper.MapFromDevicesToDeviceResponses(this.unitOfWork.DeviceRepository.Get());

            return Ok(devices);
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var people = this.unitOfWork.PersonRepository.Get();

            return Ok(this.mappersFacade.PersonMapper.MapPeopleToViewModel(people));
        }

        [HttpGet]
        public IActionResult GetLocations()
        {
            var locations = this.unitOfWork.LocationRepository.Get();

            return Ok(this.mappersFacade.LocationMapper.MapFromEntityCollection(locations));
        }
        
        [HttpPost]
        public async Task<IActionResult> AddNewUser([FromBody]PersonRequest person)
        {
            var newPerson = this.mappersFacade.PersonMapper.MapPerson(person);
            await this.unitOfWork.PersonRepository.Add(newPerson);
            await this.unitOfWork.Commit();

            return Ok(this.mappersFacade.PersonMapper.MapPersonToViewModel(newPerson));
        }
        
        [HttpPost]
        public async Task<IActionResult> AddLocation([FromBody]LocationRequest location)
        {
            var newLocation = new Location() {
                City = location.City,
                Country = location.Country,
                Name = location.Name
            };
            await this.unitOfWork.LocationRepository.Add(newLocation);
            await this.unitOfWork.Commit();

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> AddNewDevice([FromBody]DeviceRequest device)
        {
            Device newDeviceEntity = this.mappersFacade.DeviceMapper.MapFromDeviceRequestToDevice(device);
            await this.unitOfWork.GetRepository<Device>().Add(newDeviceEntity);
            await this.unitOfWork.Context.SaveChangesAsync();

            return Ok(this.mappersFacade.DeviceMapper.MapFromDeviceToDeviceResponse(newDeviceEntity));
        }
    }
}
