using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel.Auth
{
    public class PersonLoginResponse
    {
        public int Id { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
    }
}
