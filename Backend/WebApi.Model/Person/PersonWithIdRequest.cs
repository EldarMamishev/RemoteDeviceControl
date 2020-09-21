using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel.Person
{
    public class PersonWithIdRequest
    {
        public int id { get; set; }
        public string userName { get; set; }
        public string email { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
    }
}
