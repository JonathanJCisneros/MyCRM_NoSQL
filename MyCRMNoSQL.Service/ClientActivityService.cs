using MyCRMNoSQL.Core;
using MyCRMNoSQL.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCRMNoSQL.Service
{
    public class ClientActivityService : IClientActivityService
    {
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

        public ClientActivity Get(string id)
        {
            return null;
        }

        public List<ClientActivity> GetAll()
        {
            return null;
        }

        public string Create(ClientActivity activity)
        {
            return null;
        }

        public string Update(ClientActivity activity)
        {
            return null;
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
