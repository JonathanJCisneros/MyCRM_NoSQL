using MyCRMNoSQL.Core;
using MyCRMNoSQL.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCRMNoSQL.Repository
{
    public class ClientActivityRepository : IClientActivityRepository
    {
        public ClientActivity Get(string id)
        {
            var R = RethinkDb.Driver.RethinkDB.R;
            var Conn = R.Connection().Hostname("localhost").Port(28015).Timeout(60).Connect();

            var Query = R.Db("MyCRM").Table("Activities").Get(id).Run(Conn);

            if(Query == null)
            {
                return null;
            }

            ClientActivity activity = new()
            {
                Id = Query.id.ToString(),
                UserId = Query.UserId.ToString(),
                BusinessId = Query.BusinessId.ToString(),
                StaffId = Query.StaffId.ToString(),
                Type = Query.Type.ToString(),
                Note = Query.Note.ToString(),
                CreatedDate = Query.CreatedDate.ToDateTime(),
                UpdatedDate = Query.UpdatedDate.ToDateTime()
            };

            return activity;
        }

        public List<ClientActivity> GetAll()
        {
            var R = RethinkDb.Driver.RethinkDB.R;
            var Conn = R.Connection().Hostname("localhost").Port(28015).Timeout(60).Connect();
            return null;
        }

        public List<ClientActivity> GetAllByBusiness(string id)
        {
            var R = RethinkDb.Driver.RethinkDB.R;
            var Conn = R.Connection().Hostname("localhost").Port(28015).Timeout(60).Connect();
            return null;
        }

        public List<ClientActivity> GetAllByUser(string id)
        {
            var R = RethinkDb.Driver.RethinkDB.R;
            var Conn = R.Connection().Hostname("localhost").Port(28015).Timeout(60).Connect();
            return null;
        }

        public List<ClientActivity> GetAllByType(string type)
        {
            var R = RethinkDb.Driver.RethinkDB.R;
            var Conn = R.Connection().Hostname("localhost").Port(28015).Timeout(60).Connect();
            return null;
        }

        public string Create(ClientActivity activity)
        {
            var R = RethinkDb.Driver.RethinkDB.R;
            var Conn = R.Connection().Hostname("localhost").Port(28015).Timeout(60).Connect();
            return null;
        }

        public string Update(ClientActivity activity)
        {
            var R = RethinkDb.Driver.RethinkDB.R;
            var Conn = R.Connection().Hostname("localhost").Port(28015).Timeout(60).Connect();
            return null;
        }

        public bool Delete(string id)
        {
            var R = RethinkDb.Driver.RethinkDB.R;
            var Conn = R.Connection().Hostname("localhost").Port(28015).Timeout(60).Connect();
            return false;
        }

        public bool DeleteAllByBusiness(string id)
        {
            var R = RethinkDb.Driver.RethinkDB.R;
            var Conn = R.Connection().Hostname("localhost").Port(28015).Timeout(60).Connect();
            return false;
        }
    }
}
