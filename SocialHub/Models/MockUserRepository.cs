using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialHub.Models
{
    public class MockUserRepository : IUserRepository
    {

        private List<User> _userList;

        public MockUserRepository()
        {
            List<User> list = new List<User>()
            {
                new User() { Id = 1, FirstName = "Mary", LastName = "Jane", Email = "maryjane@gmail.com" },
                new User() { Id = 2, FirstName = "Chase", LastName = "Visa", Email = "djchasevisa@gmail.com" },
                new User() { Id = 3, FirstName = "Maggie", LastName = "You", Email = "maggieyou@gmail.com" }
            };
            _userList = list;
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _userList;
        }

        public User GetUser(int Id)
        {
            return _userList.FirstOrDefault(u => u.Id == Id);
        }

        public User Add(User user)
        {
            user.Id = _userList.Max(u => u.Id) + 1;
            _userList.Add(user);
            return user;
        }

        public User Update(User newUser)
        {
            User user = _userList.FirstOrDefault(u => u.Id == newUser.Id);
            if (user != null)
            {
                user.FirstName = newUser.FirstName;
                user.LastName = newUser.LastName;
                user.Email = newUser.Email;
            }
            return user;
        }

        public User Delete(int id)
        {
            User user = _userList.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                _userList.Remove(user);
            }
            return user;
        }

        
    }
}
