using MyCRMNoSQL.Core;
using MyCRMNoSQL.Repository.Interfaces;
using MyCRMNoSQL.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCRMNoSQL.Service
{
    public class UpcomingTaskService : IUpcomingTaskService
    {
        private readonly IUpcomingTaskRepository _upcomingTaskRepository;

        public UpcomingTaskService(IUpcomingTaskRepository upcomingTaskRepository)
        {
            _upcomingTaskRepository = upcomingTaskRepository;
        }

        public bool CheckById(string id)
        {
            return _upcomingTaskRepository.CheckById(id);
        }

        public List<UpcomingTask> GetAllByBusiness(string id)
        {
            return _upcomingTaskRepository.GetAllByBusiness(id);
        }

        public List<UpcomingTask> GetAllByType(string type)
        {
            return _upcomingTaskRepository.GetAllByType(type);
        }

        public List<UpcomingTask> GetAllByStatus(string status)
        {
            return _upcomingTaskRepository.GetAllByStatus(status);
        }

        public List<UpcomingTask> GetAllByUser(string id)
        {
            return _upcomingTaskRepository.GetAllByUser(id);
        }

        public List<UpcomingTask> GetAllPastDue()
        {
            return _upcomingTaskRepository.GetAllPastDue();
        }

        public List<UpcomingTask> GetAllUpcoming()
        {
            return _upcomingTaskRepository.GetAllUpcoming();
        }

        public UpcomingTask Get(string id)
        {
            return _upcomingTaskRepository.Get(id);
        }

        public List<UpcomingTask> GetAll()
        {
            return _upcomingTaskRepository.GetAll();
        }

        public string Create(UpcomingTask task)
        {
            return _upcomingTaskRepository.Create(task);
        }

        public string Update(UpcomingTask task)
        {
            return _upcomingTaskRepository.Update(task);
        }

        public bool Delete(string id)
        {
            return _upcomingTaskRepository.Delete(id);
        }

        public bool DeleteAllByBusiness(string id)
        {
            return _upcomingTaskRepository.DeleteAllByBusiness(id);
        }
    }
}
