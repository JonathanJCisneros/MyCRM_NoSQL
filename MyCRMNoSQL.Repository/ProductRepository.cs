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
        public Product Get(string id)
        {
            return null;
        }

        public Product GetProductWithCustomers(string id)
        {
            return null;
        }

        public List<Product> GetAll()
        {
            return null;
        }

        public List<Product> GetAllProductsWithCustomers()
        {
            return null;
        }

        public string Create(Product product)
        {
            return null;
        }

        public string Update(Product product)
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
