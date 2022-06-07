using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConGEST.DTOs.Holliday
{
    public class CreateHollidayDto
    {
        public DateTime DateBegin { get; set; }
        public DateTime DateEnd { get; set; }
        public string Commentaire { get; set; }
    }
}
