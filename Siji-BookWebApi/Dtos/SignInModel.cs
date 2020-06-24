using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Siji_BookWebApi.Dtos
{
    public class SignInModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public string Expires { get; set; }
    }
}
