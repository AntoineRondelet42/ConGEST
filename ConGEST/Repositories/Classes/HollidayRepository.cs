using ConGEST.CongestDbContext;
using ConGEST.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConGEST.Repositories.Classes
{
    public class HollidayRepository : IHollidayRepository
    {
        public readonly CongestContext _context;

        public HollidayRepository(CongestContext context) 
        {
            _context = context;
        }
        public void CreateHolliday(Holliday holliday)
        {
            _context.Holliday.Add(holliday); //équivalent du insert into
            _context.SaveChanges(); //envoie la requête
        }

        public IEnumerable<Holliday> GetHollidays()
        {
            return _context.Holliday.ToList(); // select
        }

        public void DeleteHolliday(int hollidayId)
        {
            Holliday holliday = _context.Holliday.Find(hollidayId);
            _context.Holliday.Remove(holliday);
            _context.SaveChanges();
        }

        public void UpdateHolliday(int hollidayId, Holliday holliday)
        {
            Holliday hollidayEntity = _context.Holliday.Find(hollidayId);
            _context.Update(hollidayEntity);
            _context.SaveChanges();
        }
    }
}
