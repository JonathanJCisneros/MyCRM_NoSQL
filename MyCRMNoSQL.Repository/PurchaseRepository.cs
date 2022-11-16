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
        public async Task<Purchase> Get(string id)
        {
            return null;
        }

        public async Task<List<Purchase>> GetAll()
        {
            return null;
        }

        public async Task<List<Purchase>> GetAllByBusiness(string id)
        {
            return null;
        }

        public async Task<List<Purchase>> GetAllByIndustry(string industry)
        {
            return null;
        }

        public async Task<List<Purchase>> GetAllByCity(string city)
        {
            return null;
        }

        public async Task<List<Purchase>> GetAllByZipCode(int zipCode)
        {
            return null;
        }

        public async Task<List<Purchase>> GetAllByUser(string id)
        {
            return null;
        }

        public async Task<bool> Create(Purchase purchase)
        {
            return false;
        }

        public async Task<bool> Update(Purchase purchase)
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
