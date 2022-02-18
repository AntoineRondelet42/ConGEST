using ConGEST.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConGEST.CongestDbContext
{
    public class CongestContext : IdentityDbContext<User, Role, Guid>
    {
        public CongestContext(DbContextOptions<CongestContext> options) : base(options)
        {
            
        }

        public DbSet<Service> Service { get; set; }
        public DbSet<Holliday> Holliday { get; set; }
        public DbSet<ValidState> ValidState { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ValidState>().HasData(
                new ValidState
                {
                    ValidStateId = 1,
                    ValidStateLib = "En attente"
                },
                new ValidState
                {
                    ValidStateId = 2,
                    ValidStateLib = "Acceptée"
                },
                new ValidState
                {
                    ValidStateId = 3,
                    ValidStateLib = "Refusée"
                }
            );

            base.OnModelCreating(builder);
        }
    }
}
