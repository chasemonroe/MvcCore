using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SocialHub.Models;
using SocialHub.Models.ExtensionMethods;

namespace SocialHub.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) 
            : base(options)
        {

        }

        // DbSets for ORM 
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Added Seed() as an extension method to the modelbuilder class
            modelBuilder.Seed();
        }

        public DbSet<SocialHub.Models.Product> Product { get; set; }
    }
}
