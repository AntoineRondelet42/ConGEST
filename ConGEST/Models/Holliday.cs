using ConGEST.Models;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConGEST.CongestDbContext
{
    public class Holliday
    {
        public int HollidayId { get; set; }
        public DateTime DateAsk { get; set; }
        public DateTime DateBegin { get; set; }
        public DateTime DateEnd { get; set; }
        public string Commentaire { get; set; }
        public int ValidStateId { get; set; }
        [ForeignKey("ValidStateId")]
        public ValidState ValidState { get; set; }
        public Guid UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}