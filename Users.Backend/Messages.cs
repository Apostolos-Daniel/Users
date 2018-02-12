using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Threading.Tasks;

namespace Users.Backend
{
    public static class Messages
    {
        public static string AddUserSuccess => "User added successfully";

        public static string UserInformationInvalid => "User information not valid";

        public static string InvalidEmailAddress => "Invalid email address";
        
        public static string PasswordTooShort => "Password is too short";
        
        public static string InvalidPassword => "Invalid password";
        
        public static string PasswordsDoNotMatch => "Passwords don't match";
        
        public static string Error => "An error has occurred.";

        public static string UserValid => "User valid.";

        public static string PasswordDoesNotMatchPattern => "Password does not match pattern.";

        public static string PasswordNotSet => "Password not pattern.";

        public static string EmailAddressNotSet => "Email address not set.";

        public static string UserExists => "User exists already";
    }
}
