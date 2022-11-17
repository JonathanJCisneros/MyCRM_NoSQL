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
        public bool CheckByName(string name)
        {
            return false;
        }

        public Business Get(string id)
        {
            return null;
        }

        public List<Business> GetAll()
        {
            return null;
        }

        public List<Business> GetAllByIndustry(string industry)
        {
            return null;
        }

        public bool Create(Business business)
        {
            return false;
        }

        public bool Update(Business business)
        {
            return false;
        }

        public bool Delete(string id)
        {
            return false;
        }
    }
}
