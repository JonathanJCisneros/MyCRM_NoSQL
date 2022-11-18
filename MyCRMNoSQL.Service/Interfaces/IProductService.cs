using MyCRMNoSQL.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCRMNoSQL.Service.Interfaces
{
    public interface IProductService
    {
        Product GetProductWithCustomers(string id);

        List<Product> GetAllProductsWithCustomers();

        Product Get(string id);

        List<Product> GetAll();

        string Create(Product entity);

        string Update(Product entity);

        bool Delete(string id);
    }
}
