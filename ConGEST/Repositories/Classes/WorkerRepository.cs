using ConGEST.CongestDbContext;
using ConGEST.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConGEST.Repositories.Classes
{
    public class WorkerRepository : IWorkerRepository
    {
        public readonly CongestContext _context;

        public object[] WorkerId { get; private set; }

        public WorkerRepository(CongestContext context)
        {
            _context = context;
        }
        public void CreateWorker(Worker worker)
        {
            _context.Worker.Add(worker); // équivalent du insert into
            _context.SaveChanges(); // envoie la requete 
        }

        public IEnumerable<Worker> GetWorkers()
        {
            return _context.Worker.ToList(); // select
        }

        public void DeleteWorker(int workerId)
        {
            Worker worker = _context.Worker.Find(workerId);
            _context.Worker.Remove(worker);
            _context.SaveChanges();
        }

        public void UpdateWorker(int workerId, Worker worker)
        {
            Worker workerEntity = _context.Worker.Find(workerId);
            _context.Update(workerEntity);
            _context.SaveChanges();
        }
    }
}
