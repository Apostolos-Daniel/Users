namespace Users.Backend
{
    public interface IUserValidator
    {
        bool IsValid(User user, out string result);

        bool HasValidEmailAddress(string emailaddress);
    }
}