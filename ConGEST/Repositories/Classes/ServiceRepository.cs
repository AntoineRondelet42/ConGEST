using ConGEST.CongestDbContext;
using ConGEST.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConGEST.Repositories.Classes
{
    public class ServiceRepository : IServiceRepository
    {
        public readonly CongestContext _context;

        public object[] ServiceId { get; private set; }

        public ServiceRepository(CongestContext context)
        {
            _context = context;
        }
        public void CreateService(Service service)
        {
            _context.Service.Add(service); // équivalent du insert into
            _context.SaveChanges(); // envoie la requete 
        }

        public IEnumerable<Service> GetServices()
        {
            return _context.Service.ToList(); // select
        }

        public void DeleteService(int serviceId)
        {
            Service service = _context.Service.Find(serviceId);
            _context.Service.Remove(service);
            _context.SaveChanges();
        }

        public void UpdateService(int serviceId, Service service)
        {
            Service serviceEntity = _context.Service.Find(serviceId);
            _context.Update(serviceEntity);
            _context.SaveChanges();
        }
    }
}
