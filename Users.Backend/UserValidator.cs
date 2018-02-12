using System;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace Users.Backend
{
    public class UserValidator : IUserValidator
    {
        private readonly string _passwordPattern;
        private readonly int _nMinPasswordLength;

        public UserValidator(string passwordPattern, int nMinPasswordLength)
        {
            _passwordPattern = passwordPattern;
            _nMinPasswordLength = nMinPasswordLength;
        }

        public bool IsValid(User user, out string result)
        {
            result = Messages.UserValid;
            if (string.IsNullOrEmpty(user?.EmailAddress))
            {
                result = Messages.EmailAddressNotSet;
                return false;
            }
            if (string.IsNullOrEmpty(user.Password))
            {
                result = Messages.PasswordNotSet;
                return false;
            }
            if (user.Password.Length < _nMinPasswordLength)
            {
                result = Messages.PasswordTooShort;
                return false;
            }
            if (!HasValidEmailAddress(user.EmailAddress))
            {
                result = Messages.InvalidEmailAddress;
                return false;
            }

            if (!Regex.IsMatch(user.Password, _passwordPattern))
            {
                result = Messages.PasswordDoesNotMatchPattern;
                return false;
            }
            if (user.Password != user.PasswordConfirmation)
            {
                result = Messages.PasswordsDoNotMatch;
                return false;
            }
            return true;
        }
        
        public bool HasValidEmailAddress(string emailaddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailaddress);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}
