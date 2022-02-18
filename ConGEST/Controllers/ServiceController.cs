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
    [Route("service")]
    public class ServiceController : Controller
    {
        private readonly IServiceRepository _serviceRepository;

        public ServiceController(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Service>> GetAllServices()
        {
            return Ok(_serviceRepository.GetServices());
        }

        [HttpPost("{serviceId}/validate")]
        public ActionResult ValidateService(int serviceId)
        {
            Service service = _serviceRepository.GetServiceById(serviceId);
            return NoContent();
        }

        [HttpDelete("{serviceId}")]
        public ActionResult DeleteService(int serviceId)
        {
            _serviceRepository.DeleteService(serviceId);

            return Ok();
        }

        [HttpPut("{serviceId}")]
        public ActionResult UpdateService(int serviceId, Service service)
        {
            _serviceRepository.UpdateService(serviceId, service);

            return Ok();
        }
    }
}
