using System.Threading.Tasks;
using Core.Entities;
using Core.Entities.ApplicationIdentity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Data.Contracts.DataAccess;
using System.Linq;
using System.Collections.Generic;
using ViewModel.Device;
using WebApi.Helpers.Facades.Base;
using ViewModel.PersonalDevice;
using Business.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace WebApi.Controllers
{
    [ApiController]
    public class PersonController : BaseController
    {
        private IUnitOfWork unitOfWork;
        private IPersonMappersFacade mappersFacade;

        public PersonController(UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork, IPersonMappersFacade mappersFacade) 
            : base(userManager)
        {
            this.unitOfWork = unitOfWork;
            this.mappersFacade = mappersFacade;
        }

        [HttpGet]
        public IActionResult GetAllDevices()
        {
            IEnumerable<Device> allDevices = this.unitOfWork.GetRepository<Device>().Get();
            IEnumerable<Device> devices = allDevices?.Where(d => d.Connections.Any(c => c.PersonalDevice.PersonId == CurrentUser.Result.Id));

            return Ok(this.mappersFacade.DeviceMapper.MapFromDevicesToDeviceResponses(allDevices));
        }

        [HttpPost]
        public async Task<IActionResult> AddNewDevice(DeviceRequest device)
        {
            Device newDeviceEntity = this.mappersFacade.DeviceMapper.MapFromDeviceRequestToDevice(device);
            await this.unitOfWork.GetRepository<Device>().Add(newDeviceEntity);
            await this.unitOfWork.Context.SaveChangesAsync();

            return Ok(this.mappersFacade.DeviceMapper.MapFromDeviceToDeviceResponse(newDeviceEntity));
        }

        [HttpPost]
        public async Task<IActionResult> AddNewPersonalDevice(AddPersonalDeviceRequest request)
        {
            Person person = this.unitOfWork.GetRepository<Person>().GetById(request.PersonId);
            StringToEnumConverter converter = new StringToEnumConverter();

            if (person == null)
                return this.BadRequest("Person does not exist.");

            PersonalDevice personalDevice = new PersonalDevice()
            {
                PersonId = request.PersonId,
                Name = request.PersonalDeviceName,
                Type = converter.UserDeviceTypeStringToEnumConverter(request.DeviceType)
            };

            await this.unitOfWork.GetRepository<PersonalDevice>().Add(personalDevice);
            await this.unitOfWork.Context.SaveChangesAsync();

            return Ok(this.mappersFacade.PersonalDeviceMapper.MapFromDeviceToDeviceResponse(personalDevice));
        }

        [HttpGet]
        public IActionResult GetAllDevicesPerLocations()
        {
            var devices = this.mappersFacade.DeviceMapper.DevicesByBuildingsMapper(this.unitOfWork.DeviceRepository.GetAllDevicesPerLocations());

            return Ok(devices);
        }
    }
}
