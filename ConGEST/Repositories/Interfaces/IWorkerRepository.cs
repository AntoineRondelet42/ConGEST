using ConGEST.CongestDbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConGEST.Repositories.Interfaces
{
    interface IWorkerRepository
    {
        IEnumerable<Worker> GetWorkers();
        void CreateWorker(Worker worker);
        void DeleteWorker(int workerId);
        void UpdateWorker(int workerId, Worker worker);
    }
}
