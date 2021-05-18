//using Business.Filters;
using Core.Entities;
using Core.Entities.ApplicationIdentity;
using Data.Contracts.DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViewModel.Device;
using ViewModel.Location;
using ViewModel.Person;
using ViewModel.Shared;
using Services.Facades.Base;

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

        [HttpPost]
        public IActionResult Backup()
            => this.unitOfWork.Backup() ? Ok() : StatusCode(StatusCodes.Status500InternalServerError);

        [HttpGet]
        public IActionResult GetAllDevicesPerLocations()
        {
            var devices = this.mappersFacade.DeviceMapper.DevicesByBuildingsMapperAdmin(this.unitOfWork.DeviceRepository.GetAllDevicesPerLocations(), this.unitOfWork);

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
            newDeviceEntity.Name = string.IsNullOrWhiteSpace(device.Name) ?  newDeviceEntity.Name : device.Name;
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

        [HttpPost]
        public async Task<IActionResult> UpdateUser([FromBody]PersonWithIdRequest person)
        {
            Person newPerson = this.unitOfWork.PersonRepository.GetById(person.id);
            newPerson.FirstName = string.IsNullOrWhiteSpace(person.firstName) ? newPerson.FirstName : person.firstName;
            newPerson.LastName = string.IsNullOrWhiteSpace(person.lastName) ? newPerson.LastName : person.lastName;
            newPerson.UserName = string.IsNullOrWhiteSpace(person.userName) ? newPerson.UserName : person.userName;
            newPerson.Email = string.IsNullOrWhiteSpace(person.email) ? newPerson.Email : person.email;
            this.unitOfWork.PersonRepository.Update(newPerson);
            await this.unitOfWork.Commit();

            return Ok(person);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser([FromBody] SingleIdRequest deviceId)
        {
            Person newPerson = this.unitOfWork.PersonRepository.GetById(deviceId.Id);
            this.unitOfWork.PersonRepository.Delete(newPerson);
            await this.unitOfWork.Commit();

            return Ok("Deleted");
        }
    }
}
