using MyCRMNoSQL.Core;
using MyCRMNoSQL.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCRMNoSQL.Repository
{
    public class BusinessRepository : IBusinessRepository
    {
        public async Task<bool> CheckByName(string name)
        {
            return false;
        }

        public async Task<Business> Get(string id)
        {
            return null;
        }

        public async Task<List<Business>> GetAll()
        {
            return null;
        }

        public async Task<List<Business>> GetAllByIndustry(string industry)
        {
            return null;
        }

        public async Task<bool> Create(Business business)
        {
            return false;
        }

        public async Task<bool> Update(Business business)
        {
            return false;
        }

        public async Task<bool> Delete(string id)
        {
            return false;
        }
    }
}
