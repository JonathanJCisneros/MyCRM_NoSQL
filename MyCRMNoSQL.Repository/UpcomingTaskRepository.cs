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
        public async Task<UpcomingTask> Get(string id)
        {
            return null;
        }

        public async Task<List<UpcomingTask>> GetAll()
        {
            return null;
        }

        public async Task<List<UpcomingTask>> GetAllByBusiness(string id)
        {
            return null;
        }

        public async Task<List<UpcomingTask>> GetAllByUser(string id)
        {
            return null;
        }

        public async Task<List<UpcomingTask>> GetAllByStatus(string status)
        {
            return null;
        }

        public async Task<List<UpcomingTask>> GetAllByType(string type)
        {
            return null;
        }

        public async Task<List<UpcomingTask>> GetAllPastDue()
        {
            return null;
        }

        public async Task<List<UpcomingTask>> GetAllUpcoming()
        {
            return null;
        }

        public async Task<bool> Create(UpcomingTask task)
        {
            return false;
        }

        public async Task<bool> Update(UpcomingTask task)
        {
            return false;
        }

        public async Task<bool> Delete(string id)
        {
            return false;
        }

        public async Task<bool> DeleteAllByBusiness(string id)
        {
            return false;
        }
    }
}
