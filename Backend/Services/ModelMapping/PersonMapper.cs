using Core.Entities;
using Core.Entities.ApplicationIdentity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViewModel.Auth;
using ViewModel.Device;
using ViewModel.Person;

namespace Services.ModelMapping
{
    public class PersonMapper
    {
        public IEnumerable<PersonResponse> MapPeopleToViewModel(IEnumerable<ApplicationUser> people)
        {
            List<PersonResponse> result = new List<PersonResponse>();

            foreach (var person in people)
            {
                result.Add(this.MapPersonToViewModel(person));
            }

            return result;
        }

        public PersonResponse MapPersonToViewModel(ApplicationUser person)
        {
            var result = new PersonResponse()
            {
                id = person.Id,
                firstName = ((Person)person).FirstName,
                lastName = ((Person)person).LastName,
                email = person.Email,
                userName = person.UserName,
                discriminator = person.GetType().Name.Replace("Proxy", string.Empty)
            };

            return result;
        }
        
        public Person MapPersonFromRegister(Register person)
        {
            var result = new Person()
            {
                FirstName = person.FirstName,
                LastName = person.LastName,
                Email = person.Email,
                UserName = person.UserName
            };

            return result;
        }
        
        public Person MapPerson(PersonRequest person)
        {
            var result = new Person()
            {
                FirstName = person.firstName,
                LastName = person.lastName,
                Email = person.email,
                UserName = person.userName
            };

            return result;
        }
    }
}
