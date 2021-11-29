using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class dbContext : IdentityDbContext<ApplicationUser>
    {
        public dbContext() : base("VidlyDb")
        {
            Database.SetInitializer<dbContext>(new CreateDatabaseIfNotExists<dbContext>());
        }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<MembershipType> MembershipTypes { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Genre> Genres { get; set; }


    }
}