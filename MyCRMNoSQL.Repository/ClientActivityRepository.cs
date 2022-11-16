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
        public async Task<ClientActivity> Get(string id)
        {
            return null;
        }

        public async Task<List<ClientActivity>> GetAll()
        {
            return null;
        }

        public async Task<List<ClientActivity>> GetAllByBusiness(string id)
        {
            return null;
        }

        public async Task<List<ClientActivity>> GetAllByUser(string id)
        {
            return null;
        }

        public async Task<List<ClientActivity>> GetAllByType(string type)
        {
            return null;
        }

        public async Task<bool> Create(ClientActivity activity)
        {
            return false;
        }

        public async Task<bool> Update(ClientActivity activity)
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
