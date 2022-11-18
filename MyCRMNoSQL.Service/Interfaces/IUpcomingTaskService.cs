using MyCRMNoSQL.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCRMNoSQL.Service.Interfaces
{
    public interface IUpcomingTaskService
    {
        List<UpcomingTask> GetAllByBusiness(string id);

        List<UpcomingTask> GetAllByType(string type);

        List<UpcomingTask> GetAllByStatus(string status);

        List<UpcomingTask> GetAllByUser(string id);

        List<UpcomingTask> GetAllPastDue();

        List<UpcomingTask> GetAllUpcoming();

        UpcomingTask Get(string id);

        List<UpcomingTask> GetAll();

        string Create(UpcomingTask task);

        string Update(UpcomingTask task);

        bool Delete(string id);

        bool DeleteAllByBusiness(string id);
    }
}
