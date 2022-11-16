using MyCRMNoSQL.Core;
using MyCRMNoSQL.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCRMNoSQL.Repository
{
    public class StaffRepository : IStaffRepository
    {
        public async Task<Staff> Get(string id)
        {
            return null;
        }

        public async Task<List<Staff>> GetAll()
        {
            return null;
        }

        public async Task<List<Staff>> GetByBusiness(string id)
        {
            return null;
        }

        public async Task<List<Staff>> GetByPosition(string position)
        {
            return null;
        }

        public async Task<bool> Create(Staff staff)
        {
            return false;
        }

        public async Task<bool> Update(Staff staff)
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
