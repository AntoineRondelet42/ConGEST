using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConGEST.CongestDbContext
{
    public class Worker
    {
        public int WorkerId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public bool IsManager { get; set; }
        public ICollection<Holliday> Hollidays { get; set; }

        // rajouter un compteur de congés (5 semaines par an)
    }
}