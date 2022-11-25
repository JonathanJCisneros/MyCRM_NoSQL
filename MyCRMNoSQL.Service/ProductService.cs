using MyCRMNoSQL.Core;
using MyCRMNoSQL.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCRMNoSQL.Service
{
    public class ProductService : IProductService
    {
        public bool CheckById(string id)
        {
            return false;
        }

        public Product GetProductWithCustomers(string id)
        {
            return null;
        }

        public List<Product> GetAllProductsWithCustomers()
        {
            return null;
        }

        public Product Get(string id)
        {
            return null;
        }

        public List<Product> GetAll()
        {
            return null;
        }

        public string Create(Product entity)
        {
            return null;
        }

        public string Update(Product entity)
        {
            return null;
        }

        public bool Delete(string id)
        {
            return false;
        }
    }
}
