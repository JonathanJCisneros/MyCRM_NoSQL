using MyCRMNoSQL.Core;
using MyCRMNoSQL.Core.Interfaces;
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
            return null;
        }

        public List<ClientActivity> GetAll()
        {
            return null;
        }

        public List<ClientActivity> GetAllByBusiness(string id)
        {
            return null;
        }

        public List<ClientActivity> GetAllByUser(string id)
        {
            return null;
        }

        public List<ClientActivity> GetAllByType(string type)
        {
            return null;
        }

        public bool Create(ClientActivity activity)
        {
            return false;
        }

        public bool Update(ClientActivity activity)
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
