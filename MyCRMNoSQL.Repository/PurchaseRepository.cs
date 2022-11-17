using MyCRMNoSQL.Core;
using MyCRMNoSQL.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCRMNoSQL.Repository
{
    public class PurchaseRepository : IPurchaseRepository
    {
        public Purchase Get(string id)
        {
            return null;
        }

        public List<Purchase> GetAll()
        {
            return null;
        }

        public List<Purchase> GetAllByBusiness(string id)
        {
            return null;
        }

        public List<Purchase> GetAllByIndustry(string industry)
        {
            return null;
        }

        public List<Purchase> GetAllByCity(string city)
        {
            return null;
        }

        public List<Purchase> GetAllByZipCode(int zipCode)
        {
            return null;
        }

        public List<Purchase> GetAllByUser(string id)
        {
            return null;
        }

        public bool Create(Purchase purchase)
        {
            return false;
        }

        public bool Update(Purchase purchase)
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
