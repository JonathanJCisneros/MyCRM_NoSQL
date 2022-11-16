using MyCRMNoSQL.Core;
using MyCRMNoSQL.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCRMNoSQL.Repository
{
    public class ProductRepository : IProductRepository
    {
        public async Task<Product> Get(string id)
        {
            return null;
        }

        public async Task<Product> GetProductWithCustomers(string id)
        {
            return null;
        }

        public async Task<List<Product>> GetAll()
        {
            return null;
        }

        public async Task<List<Product>> GetAllProductsWithCustomers()
        {
            return null;
        }

        public async Task<bool> Create(Product product)
        {
            return false;
        }

        public async Task<bool> Update(Product product)
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
