using AutoMapper;
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
        private readonly IMapper _mapper;

        public HollidayController(IHollidayRepository hollidayRepository, IMapper mapper)
        {
            _hollidayRepository = hollidayRepository;
            _mapper = mapper;
        }

        //[Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult<IEnumerable<Holliday>> GetAllHollidays()
        {
            return Ok(_hollidayRepository.GetHollidays());
        }

        [Authorize]
        [HttpGet("user")]
        public ActionResult<IEnumerable<HollidayDto>> GetAllHollidaysForUser()
        {
            IEnumerable<Holliday> hollidays = _hollidayRepository.GetHollidaysForUser(Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)));

            IEnumerable<HollidayDto> mappedHollidays = _mapper.Map<IEnumerable<HollidayDto>>(hollidays);

            foreach (var mappedHolliday in mappedHollidays)
            {
                mappedHolliday.CalculateWorkingDays();
            }

            return Ok(mappedHollidays);
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

        //[Authorize (Roles = "Admin,Manager,RH")]
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

        //[Authorize (Roles = "Admin,Manager,RH")]
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

        [Authorize(Roles = "Admin")]
        [HttpDelete("{hollidayId}")]
        public ActionResult DeleteHolliday(int hollidayId)
        {
            _hollidayRepository.DeleteHolliday(hollidayId);

            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{hollidayId}")]
        public ActionResult UpdateHolliday(int hollidayId, Holliday holliday)
        {
            _hollidayRepository.UpdateHolliday(hollidayId, holliday);

            return Ok();
        }
    }
}
