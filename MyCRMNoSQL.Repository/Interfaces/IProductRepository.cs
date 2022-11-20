using MyCRMNoSQL.Core;
using MyCRMNoSQL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCRMNoSQL.Repository.Interfaces
{
    public interface IProductRepository : ICRUDRepository<Product>
    {
        Product GetProductWithCustomers(string id);

        List<Product> GetAllProductsWithCustomers();
    }
}
