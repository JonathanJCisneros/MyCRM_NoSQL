using MyCRMNoSQL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCRMNoSQL.Core.Interfaces
{
    public interface IUpcomingTaskRepository : ICRUDRepository<UpcomingTask>, IBRepository<UpcomingTask>
    {
        Task<List<UpcomingTask>> GetAllByType(string type);

        Task<List<UpcomingTask>> GetAllByStatus(string status);

        Task<List<UpcomingTask>> GetAllByUser(string id);

        Task<List<UpcomingTask>> GetAllPastDue();

        Task<List<UpcomingTask>> GetAllUpcoming();
    }
}
