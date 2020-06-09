using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialHub.Models.ExtensionMethods
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    FirstName = "Chase",
                    LastName = "Parr",
                    Email = "djchasevisa@gmail.com"
                },
                new User
                {
                    Id = 2,
                    FirstName = "Maggie",
                    LastName = "yu",
                    Email = "maggie9595@gmail.com"
                },
                new User
                {
                    Id = 3,
                    FirstName = "Bob",
                    LastName = "Rs",
                    Email = "bobrunescape@gmail.com"
                }
           );
        }
    }
}
