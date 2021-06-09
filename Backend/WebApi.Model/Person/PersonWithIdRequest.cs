using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel.Person
{
    public class PersonWithIdRequest
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string FirstName { get; set; }
        public string NewPassword { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }
}
