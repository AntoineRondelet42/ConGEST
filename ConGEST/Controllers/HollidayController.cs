using ConGEST.CongestDbContext;
using ConGEST.DTOs.Holliday;
using ConGEST.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult<IEnumerable<Holliday>> GetAllHollidays()
        {
            return Ok(_hollidayRepository.GetHollidays());
        }

        [Authorize]
        [HttpGet("user")]
        public ActionResult<IEnumerable<Holliday>> GetAllHollidaysForUser()
        {
            return Ok(_hollidayRepository.GetHollidaysForUser(Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier))));
        }

        [Authorize]
        [HttpPost]
        public ActionResult CreateHolliday(CreateHollidayDto holliday)
        {
            Holliday hollidayEntity = new Holliday();

            hollidayEntity.DateAsk = DateTime.Now;
            hollidayEntity.ValidStateId = 1;
            hollidayEntity.DateBegin = holliday.DateBegin;
            hollidayEntity.DateEnd = holliday.DateEnd;
            hollidayEntity.UserId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            _hollidayRepository.CreateHolliday(hollidayEntity);
            return Ok();
        }

        [Authorize (Roles = "Admin,Manager,RH")]
        [HttpPost("{hollidayId}/validate")]
        public ActionResult ValidateHolliday(int hollidayId)
        {
            Holliday holliday = _hollidayRepository.GetHollidayById(hollidayId);

            if(holliday == null)
            {
                return NotFound("La demande de congés n'existe pas.");
            }

            holliday.ValidStateId = 2;

            _hollidayRepository.UpdateHolliday(holliday.HollidayId, holliday);

            return NoContent();
        }

        [Authorize (Roles = "Admin,Manager,RH")]
        [HttpPost("{hollidayId}/refuse")]
        public ActionResult RefuseHolliday(int hollidayId)
        {
            Holliday holliday = _hollidayRepository.GetHollidayById(hollidayId);

            if (holliday == null)
            {
                return NotFound("La demande de congés n'existe pas.");
            }

            holliday.ValidStateId = 3;

            _hollidayRepository.UpdateHolliday(holliday.HollidayId, holliday);

            return NoContent();
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
