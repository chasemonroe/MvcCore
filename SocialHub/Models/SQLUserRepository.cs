using SocialHub.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace SocialHub.Models
{
    public class SQLUserRepository : IUserRepository
    {

        private readonly AppDbContext context;

        public SQLUserRepository(AppDbContext context)
        {
            this.context = context;
        }
        public User Add(User user)
        {
            context.Users.Add(user);
            context.SaveChanges();
            return user;
        }

        public User Delete(int id)
        {
            User user = context.Users.Find(id);
            if(user != null)
            {
                context.Users.Remove(user);
                context.SaveChanges();
            }
            return user;
        }

        public IEnumerable<User> GetAllUsers()
        {
            return context.Users;
        }

        public User GetUser(int id)
        {
            return context.Users.Find(id);
        }

        public User Update(User newUser)
        {
            var user = context.Users.Attach(newUser);
            user.State = EntityState.Modified;
            context.SaveChanges();
            return newUser;
        }
    }
}
