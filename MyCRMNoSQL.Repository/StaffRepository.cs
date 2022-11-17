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
        public Staff Get(string id)
        {
            return null;
        }

        public List<Staff> GetAll()
        {
            return null;
        }

        public List<Staff> GetAllByBusiness(string id)
        {
            return null;
        }

        public List<Staff> GetAllByPosition(string position)
        {
            return null;
        }

        public bool Create(Staff staff)
        {
            return false;
        }

        public bool Update(Staff staff)
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
