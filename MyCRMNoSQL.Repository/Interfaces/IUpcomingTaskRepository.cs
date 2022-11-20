using MyCRMNoSQL.Repository;
using MyCRMNoSQL.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCRMNoSQL.Repository.Interfaces
{
    public interface IUpcomingTaskRepository : ICRUDRepository<UpcomingTask>, IBRepository<UpcomingTask>
    {
        List<UpcomingTask> GetAllByType(string type);

        List<UpcomingTask> GetAllByStatus(string status);

        List<UpcomingTask> GetAllByUser(string id);

        List<UpcomingTask> GetAllPastDue();

        List<UpcomingTask> GetAllUpcoming();
    }
}
