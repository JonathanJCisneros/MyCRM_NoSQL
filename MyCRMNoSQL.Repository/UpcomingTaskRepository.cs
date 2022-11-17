using MyCRMNoSQL.Core;
using MyCRMNoSQL.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCRMNoSQL.Repository
{
    public class UpcomingTaskRepository : IUpcomingTaskRepository
    {
        public UpcomingTask Get(string id)
        {
            return null;
        }

        public List<UpcomingTask> GetAll()
        {
            return null;
        }

        public List<UpcomingTask> GetAllByBusiness(string id)
        {
            return null;
        }

        public List<UpcomingTask> GetAllByUser(string id)
        {
            return null;
        }

        public List<UpcomingTask> GetAllByStatus(string status)
        {
            return null;
        }

        public List<UpcomingTask> GetAllByType(string type)
        {
            return null;
        }

        public List<UpcomingTask> GetAllPastDue()
        {
            return null;
        }

        public List<UpcomingTask> GetAllUpcoming()
        {
            return null;
        }

        public bool Create(UpcomingTask task)
        {
            return false;
        }

        public bool Update(UpcomingTask task)
        {
            return false;
        }

        public bool Delete(string id)
        {
            return false;
        }

        public bool DeleteAllByBusiness(string id)
        {
            return false;
        }
    }
}
