using ConGEST.CongestDbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConGEST.Repositories.Interfaces
{
    interface IServiceRepository
    {
        IEnumerable<Service> GetServices();
        void CreateService(Service service);
        void DeleteService(int serviceId);
        void UpdateService(int serviceId, Service service);
    }
}
