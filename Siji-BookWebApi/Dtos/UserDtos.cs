using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Siji_BookWebApi.Dtos
{
    public class UserDtos
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public string Password { get; set; }
    }

    public class LoginDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
