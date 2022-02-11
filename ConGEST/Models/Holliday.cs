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
        public int ValidStateId { get; set; }
        [ForeignKey("ValidStateId")]
        public ValidState ValidState { get; set; }
        public int WorkerId { get; set; }
        [ForeignKey("WorkerId")]
        public Worker Worker { get; set; }



    }
}