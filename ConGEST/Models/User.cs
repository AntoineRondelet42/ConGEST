using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConGEST.CongestDbContext;
using Microsoft.AspNetCore.Identity;

namespace ConGEST.Models
{
    public class User : IdentityUser<Guid>
    {
        public int WorkerId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public bool IsManager { get; set; }
        public ICollection<Holliday> Hollidays { get; set; }

        // rajouter un compteur de congés (5 semaines par an)
    }
}
