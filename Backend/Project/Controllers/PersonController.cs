using System.Threading.Tasks;
using Core.Entities;
using Core.Entities.ApplicationIdentity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Data.Contracts.DataAccess;
using System.Linq;
using System.Collections.Generic;
using ViewModel.Device;
using Services.Facades.Base;
using ViewModel.PersonalDevice;
using Business.Helpers;
using Microsoft.AspNetCore.Authorization;
using ViewModel.Person;
using ViewModel.Shared;
using WebApi.Controllers.Base;

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
        public IActionResult GetAllUsers()
        {
            var people = this.unitOfWork.ApplicationUserRepository.Get();

            return Ok(this.mappersFacade.PersonMapper.MapPeopleToViewModel(people));
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
        public async Task<IActionResult> UpdateUser([FromBody]PersonWithIdRequest model)
        {
            Person person = this.unitOfWork.PersonRepository.GetById(model.Id);
            person.FirstName = string.IsNullOrWhiteSpace(model.FirstName) ? person.FirstName : model.FirstName;
            person.LastName = string.IsNullOrWhiteSpace(model.LastName) ? person.LastName : model.LastName;
            person.UserName = string.IsNullOrWhiteSpace(model.UserName) ? person.UserName : model.UserName;
            person.Email = string.IsNullOrWhiteSpace(model.Email) ? person.Email : model.Email;
            person.PhoneNumber = string.IsNullOrWhiteSpace(model.Phone) ? person.PhoneNumber : model.Phone;
            person.Address = string.IsNullOrWhiteSpace(model.Address) ? person.Address : model.Address;
            person.City = string.IsNullOrWhiteSpace(model.City) ? person.City : model.City;
            person.Country = string.IsNullOrWhiteSpace(model.Country) ? person.Country : model.Country;
            person.Birthday = model.DateOfBirth ?? person.Birthday;

            this.unitOfWork.PersonRepository.Update(person);

            await this.unitOfWork.Commit();

            return Ok(model);
        }

        [HttpGet]
        public ActionResult<PersonWithIdRequest> GetUserInfo(int userId)
        {
            Person person = this.unitOfWork.PersonRepository.GetById(userId);
            var model = new PersonWithIdRequest
            {
                Id = person.Id,
                FirstName = person.FirstName,
                LastName = person.LastName,
                UserName = person.UserName,
                Phone = person.PhoneNumber,
                Email = person.Email,
                Address = person.Address,
                City = person.City,
                Country = person.Country,
                DateOfBirth = person.Birthday
            };

            return Ok(model);
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
