using MyCRMNoSQL.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCRMNoSQL.Repository
{
    internal class BusinessRepository : IRepository<Business>
    {
        public async Task<Business> Get(string Id)
        {
            return null;
        }

        public async Task<Business> Create(Business Business)
        {
            return null;
        }

        public async Task<Business> Update(Business Business)
        {
            return null;
        }

        public async Task<string> Delete(string Id)
        {
            return null;
        }
    }
}
