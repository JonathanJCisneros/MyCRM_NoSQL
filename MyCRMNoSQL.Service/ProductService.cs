using MyCRMNoSQL.Core;
using MyCRMNoSQL.Repository.Interfaces;
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
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public bool CheckById(string id)
        {
            return _productRepository.CheckById(id);
        }

        public Product GetProductWithCustomers(string id)
        {
            return _productRepository.GetProductWithCustomers(id);
        }

        public List<Product> GetAllProductsWithCustomers()
        {
            return _productRepository.GetAllProductsWithCustomers();
        }

        public Product Get(string id)
        {
            return _productRepository.Get(id);
        }

        public List<Product> GetAll()
        {
            return _productRepository.GetAll();
        }

        public string Create(Product product)
        {
            return _productRepository.Create(product);
        }

        public string Update(Product product)
        {
            return _productRepository.Update(product);
        }

        public bool Delete(string id)
        {
            return _productRepository.Delete(id);
        }
    }
}
