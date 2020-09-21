using Core.Entities;
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
        public IEnumerable<PersonResponse> MapPeopleToViewModel(IEnumerable<Person> people)
        {
            List<PersonResponse> result = new List<PersonResponse>();

            foreach (var person in people)
            {
                if (person.Id == 1)
                    continue;
                result.Add(this.MapPersonToViewModel(person));
            }

            return result;
        }

        public PersonResponse MapPersonToViewModel(Person person)
        {
            var result = new PersonResponse()
            {
                id = person.Id,
                firstName = person.FirstName,
                lastName = person.LastName,
                email = person.Email,
                userName = person.UserName
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
