using MyCRMNoSQL.Core;
using MyCRMNoSQL.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCRMNoSQL.Service
{
    public class PurchaseService : IPurchaseService
    {
        public bool CheckById(string id)
        {
            return false;
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

        public Purchase Get(string id)
        {
            return null;
        }

        public List<Purchase> GetAll()
        {
            return null;
        }

        public string Create(Purchase entity)
        {
            return null;
        }

        public string Update(Purchase entity)
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
