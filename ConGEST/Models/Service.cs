using System.ComponentModel.DataAnnotations.Schema;

namespace ConGEST.CongestDbContext
{
    public class Service
    {
        public int ServiceId { get; set; }
        public int WorkerId { get; set; }
        [ForeignKey("WorkerId")]
        public Worker Worker { get; set; }

    }
}