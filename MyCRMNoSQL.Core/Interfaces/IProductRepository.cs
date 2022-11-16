using MyCRMNoSQL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCRMNoSQL.Core.Interfaces
{
    public interface IProductRepository : ICRUDRepository<Product>
    {
        Task<Product> GetProductWithCustomers(string id);

        Task<List<Product>> GetAllProductsWithCustomers();
    }
}
