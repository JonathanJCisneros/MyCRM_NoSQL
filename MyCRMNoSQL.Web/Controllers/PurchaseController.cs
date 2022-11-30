using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyCRMNoSQL.Models;
using MyCRMNoSQL.Core;
using MyCRMNoSQL.CustomExtensions;
using RethinkDb.Driver;
using System.Diagnostics.Contracts;
using System.Security.Cryptography;
using MyCRMNoSQL.Service.Interfaces;

namespace MyCRMNoSQL.Web.Controllers
{
    public class PurchaseController : Controller
    {
        private readonly ILogger<PurchaseController> _logger;
        private readonly IExtension _extension;
        private readonly IPurchaseService _purchaseService;

        public PurchaseController(ILogger<PurchaseController> logger, IExtension extension, IPurchaseService purchaseService)
        {
            _logger = logger;
            _extension = extension;
            _purchaseService = purchaseService;
        }

        public IActionResult Get(string id)
        {
            if (!_extension.LoggedIn())
            {
                return RedirectToAction("Login", "User");
            }

            Purchase purchase = _purchaseService.Get(id);

            return View(purchase);
        }

        public IActionResult GetAll()
        {
            if (!_extension.LoggedIn())
            {
                return RedirectToAction("Login", "User");
            }

            List<Purchase> purchase = _purchaseService.GetAll();

            return View(purchase);
        }

        [HttpPost]
        public IActionResult Add(string id, PurchaseFormModel Purchase)
        {
            Purchase.BusinessId = id;
            Purchase.UserId = _extension.UserId();

            if (!ModelState.IsValid)
            {
                return RedirectToAction("ViewOne", "CRM", new { id = id });
            }

            Purchase purchase = new()
            {
                BusinessId = Purchase.BusinessId,
                ProductId = Purchase.ProductId,
                AddressId = Purchase.AddressId,
                UserId = Purchase.UserId,
                CreatedDate = Purchase.CreatedDate,
                UpdatedDate = Purchase.UpdatedDate
            };

            string Id = _purchaseService.Create(purchase);

            return RedirectToAction("ViewOne", "CRM", new { id = id });
        }

        [HttpPost]
        public IActionResult Update(string id, string Bid, PurchaseFormModel Purchase)
        {
            Purchase.BusinessId = id;
            Purchase.UserId = _extension.UserId();

            if (!ModelState.IsValid)
            {
                return RedirectToAction("ViewOne", "CRM", new { id = Bid });
            }

            Purchase purchase = new()
            {
                Id = id,
                BusinessId = Purchase.BusinessId,
                ProductId = Purchase.ProductId,
                AddressId = Purchase.AddressId,
                UserId = Purchase.UserId,
                UpdatedDate = Purchase.UpdatedDate
            };

            string Id = _purchaseService.Update(purchase);

            return RedirectToAction("ViewOne", "CRM", new { id = Bid });
        }

        public IActionResult Delete(string id, string Bid)
        {
            if (!_extension.LoggedIn())
            {
                return RedirectToAction("Login", "User");
            }

            bool Success = _purchaseService.Delete(id);

            if(!Success)
            {
                return Content("Something went wrong...");
            }

            return RedirectToAction("ViewOne", "CRM", new { id = Bid });
        }
    }
}
