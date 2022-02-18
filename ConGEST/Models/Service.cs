using ConGEST.Models;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConGEST.CongestDbContext
{
    public class Service
    {
        public int ServiceId { get; set; }
        public Guid UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}