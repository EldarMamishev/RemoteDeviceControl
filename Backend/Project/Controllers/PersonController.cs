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
