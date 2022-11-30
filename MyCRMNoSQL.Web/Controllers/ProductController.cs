using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyCRMNoSQL.Models;
using MyCRMNoSQL.Core;
using MyCRMNoSQL.CustomExtensions;
using RethinkDb.Driver;
using System.Diagnostics.Contracts;
using MyCRMNoSQL.Service.Interfaces;

namespace MyCRMNoSQL.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IExtension _extension;
        private readonly IProductService _productService;

        public ProductController(ILogger<ProductController> logger, IExtension extension, IProductService productService)
        {
            _logger = logger;
            _extension = extension;
            _productService = productService;
        }

        public IActionResult Get(string id)
        {
            if (!_extension.LoggedIn())
            {
                return RedirectToAction("Login", "User");
            }

            Product product = _productService.Get(id);

            return View(product);
        }

        public IActionResult GetAll() 
        {
            if (!_extension.LoggedIn())
            {
                return RedirectToAction("Login", "User");
            }

            List<Product> productList = _productService.GetAll();

            return View(productList); 
        }

        [HttpPost]
        public IActionResult Add(string id, ProductFormModel Product)
        {
            Product.UserId = _extension.UserId();

            if (!ModelState.IsValid)
            {
                return RedirectToAction("ViewOne", "CRM", new { id = id });
            }

            Product = ProductFormModel.DbPrep(Product);

            Product product = new()
            {
                Name = Product.Name,
                Price = Product.Price,
                Description = Product.Description,
                UserId = Product.UserId,
                CreatedDate = Product.CreatedDate,
                UpdatedDate = Product.UpdatedDate
            };

            string Id = _productService.Create(product);

            return RedirectToAction("ViewOne", "CRM", new { id = id });
        }

        [HttpPost]
        public IActionResult Update(string id, string Bid, ProductFormModel Product)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("ViewOne", "CRM", new { id = Bid });
            }

            Product = ProductFormModel.DbPrep(Product);

            Product product = new()
            {
                Id = id,
                Name = Product.Name,
                Price = Product.Price,
                Description = Product.Description,
                UserId = Product.UserId,
                UpdatedDate = Product.UpdatedDate
            };

            string Id = _productService.Update(product);

            return RedirectToAction("ViewOne", "CRM", new { id = Bid });
        }

        public IActionResult Delete(string id, string Bid)
        {
            if (!_extension.LoggedIn())
            {
                return RedirectToAction("Login", "User");
            }

            bool Success = _productService.Delete(id);

            if(!Success)
            {
                return Content("Something went wrong");
            }

            return RedirectToAction("ViewOne", "CRM", new { id = Bid });
        }
    }
}
