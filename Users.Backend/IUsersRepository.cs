using System;
using System.Collections.Generic;
using System.Text;

namespace Users.Backend
{
    public interface IUsersRepository
    {
        string AddUser(User user);
    }
}
