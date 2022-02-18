using ConGEST.CongestDbContext;
using ConGEST.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConGEST.Controllers
{
    [ApiController]
    [Route("holliday")]

    public class HollidayController : Controller
    {
        private readonly IHollidayRepository _hollidayRepository;

        public HollidayController(IHollidayRepository hollidayRepository)
        {
            _hollidayRepository = hollidayRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Holliday>> GetAllHollidays()
        {
            return Ok(_hollidayRepository.GetHollidays());
        }

        [HttpPost]
        public ActionResult CreateHolliday(Holliday holliday)
        {
            _hollidayRepository.CreateHolliday(holliday);
            return Ok();
        }

        [HttpDelete("{hollidayId}")]
        public ActionResult DeleteHolliday(int hollidayId)
        {
            _hollidayRepository.DeleteHolliday(hollidayId);

            return Ok();
        }

        [HttpPut("{hollidayId}")]
        public ActionResult UpdateHolliday(int hollidayId, Holliday holliday)
        {
            _hollidayRepository.UpdateHolliday(hollidayId, holliday);

            return Ok();
        }
    }
}
