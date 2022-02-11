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

        public DbSet<Worker> Worker { get; set; }
        public DbSet<Service> Service { get; set; }
        public DbSet<Holliday> Holliday { get; set; }
        public DbSet<ValidState> ValidState { get; set; }


    }
}
