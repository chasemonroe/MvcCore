using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialHub.Models
{
    // The reason for using this interface is for dependency injection
    // without it, it would make the app difficult to change & maintain

    // This repository only contains the operations it SHOULD be able to do
    // Its an interface so its a contract. 

    public interface IUserRepository
    {
        User GetUser(int id);
        IEnumerable<User> GetAllUsers();
        User Add(User user);
        User Update(User newUser);
        User Delete(int id);
    }
}
