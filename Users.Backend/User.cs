using System;
using System.Collections.Generic;
using System.Text;

namespace Users.Backend
{
    public class User
    {
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string PasswordConfirmation { get; set; }
    }
}
