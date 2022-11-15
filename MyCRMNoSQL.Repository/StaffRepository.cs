using MyCRMNoSQL.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCRMNoSQL.Repository
{
    public class StaffRepository : IRepository<Staff>
    {
        public async Task<Staff> Get(string Id)
        {
            return null;
        }

        public async Task<Staff> Create(Staff Staff)
        {
            return null;
        }

        public async Task<Staff> Update(Staff Staff)
        {
            return null;
        }

        public async Task<string> Delete(string Id)
        {
            return null;
        }
    }
}
