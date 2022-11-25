using MyCRMNoSQL.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCRMNoSQL.Service.Interfaces
{
    public interface IUpcomingTaskService : ICRUDService<UpcomingTask>, IBService<UpcomingTask>
    {
        List<UpcomingTask> GetAllByType(string type);

        List<UpcomingTask> GetAllByStatus(string status);

        List<UpcomingTask> GetAllByUser(string id);

        List<UpcomingTask> GetAllPastDue();

        List<UpcomingTask> GetAllUpcoming();
    }
}
