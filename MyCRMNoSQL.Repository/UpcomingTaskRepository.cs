using MyCRMNoSQL.Core;
using MyCRMNoSQL.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCRMNoSQL.Repository
{
    public class UpcomingTaskRepository : IUpcomingTaskRepository
    {
        public bool CheckById(string id)
        {
            var R = RethinkDb.Driver.RethinkDB.R;
            var Conn = R.Connection().Hostname("localhost").Port(28015).Timeout(60).Connect();
            bool Check = R.Db("MyCRM").Table("Tasks").Get(id).IsEmpty().Run(Conn);

            return Check;
        }

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

        public string Create(UpcomingTask task)
        {
            return null;
        }

        public string Update(UpcomingTask task)
        {
            return null;
        }

        public bool Delete(string id)
        {
            var R = RethinkDb.Driver.RethinkDB.R;
            var Conn = R.Connection().Hostname("localhost").Port(28015).Timeout(60).Connect();

            var Result = R.Db("MyCRM").Table("Tasks").Get(id).Delete().Run(Conn);

            if (Result.deleted == 0)
            {
                return false;
            }

            return true;
        }

        public bool DeleteAllByBusiness(string id)
        {
            var R = RethinkDb.Driver.RethinkDB.R;
            var Conn = R.Connection().Hostname("localhost").Port(28015).Timeout(60).Connect();

            var Result = R.Db("MyCRM").Table("Tasks").GetAll(id)[new { index = "BusinessId" }].Delete().Run(Conn);

            if (Result.deleted == 0)
            {
                return false;
            }

            return true;
        }
    }
}
