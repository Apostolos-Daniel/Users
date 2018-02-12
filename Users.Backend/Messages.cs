using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Threading.Tasks;

namespace Users.Backend
{
    public static class Messages
    {
        public static string AddUserSuccess => "User added successfully to the databsae.";

        public static string UserInformationInvalid => "User information not valid, user has not been added to the database.";

        public static string InvalidEmailAddress => "Invalid email address, user has not been added to the database.";
        
        public static string PasswordTooShort => "Password is too short, user has not been added to the database.";
        
        public static string InvalidPassword => "Invalid password, user has not been added to the database.";
        
        public static string PasswordsDoNotMatch => "Passwords don't match, user has not been added to the database.";
        
        public static string Error => "An error has occurred, user has not been added to the database.";

        public static string UserValid => "User valid, user has been added to the database.";

        public static string PasswordDoesNotMatchPattern => "Password does not match pattern, user has not been added to the database.";

        public static string PasswordNotSet => "Password not set, user has not been added to the database.";

        public static string EmailAddressNotSet => "Email address not set, user has not been added to the database.";

        public static string UserExists => "User exists already, user has not been added to the database.";
    }
}
