using ConGEST.CongestDbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConGEST.Repositories.Interfaces
{
    public interface IHollidayRepository
    {
        IEnumerable<Holliday> GetHollidays();
        void CreateHolliday(Holliday holliday);
        void DeleteHolliday(int hollidayId);
        void UpdateHolliday(int hollidayId, Holliday holliday);
        Holliday GetHollidayById(int hollidayId);
    }
}
