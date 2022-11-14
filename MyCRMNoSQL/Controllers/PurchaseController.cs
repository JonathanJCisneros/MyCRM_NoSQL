using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyCRMNoSQL.Models;
using MyCRMNoSQL.CustomExtensions;
using RethinkDb.Driver;
using System.Diagnostics.Contracts;
using System.Security.Cryptography;

namespace MyCRMNoSQL.Controllers
{
    public class PurchaseController : Controller
    {
        private readonly ILogger<PurchaseController> _logger;

        public PurchaseController(ILogger<PurchaseController> logger)
        {
            _logger = logger;
        }

        private string? Uid
        {
            get
            {
                return HttpContext.Session.GetString("UserId");
            }
        }

        [HttpPost]
        public IActionResult Add(string id, Purchase Purchase)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("ViewOne", "CRM", new { id = id });
            }

            var R = RethinkDb.Driver.RethinkDB.R;
            var Conn = R.Connection().Hostname("localhost").Port(28015).Timeout(60).Connect();
            var Query = R.Db("MyCRM").Table("Purchases")
                .Insert(new
                {
                    BusinessId = id,
                    UserId = Uid,
                    AddressId = Purchase.AddressId,
                    ProductId = Purchase.ProductId,
                    CreatedDate = Purchase.CreatedDate,
                    UpdatedDate = Purchase.UpdatedDate
                })
            .Run(Conn);

            return RedirectToAction("ViewOne", "CRM", new { id = id });
        }

        [HttpPost]
        public IActionResult Update(string id, string Bid, Purchase Purchase)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("ViewOne", "CRM", new { id = Bid });
            }

            var R = RethinkDb.Driver.RethinkDB.R;
            var Conn = R.Connection().Hostname("localhost").Port(28015).Timeout(60).Connect();
            bool Check = R.Db("MyCRM").Table("Purchases").Get(id).IsEmpty().Run(Conn);

            if (Check == true)
            {
                return RedirectToAction("ViewOne", "CRM", new { id = Bid });
            }

            var Query = R.Db("MyCRM").Table("Purchases")
                .Update(new
                {
                    BusinessId = Bid,
                    UserId = Uid,
                    AddressId = Purchase.AddressId,
                    ProductId = Purchase.ProductId,
                    UpdatedDate = Purchase.UpdatedDate
                })
            .Run(Conn);

            return RedirectToAction("ViewOne", "CRM", new { id = Bid });
        }

        public IActionResult Delete(string id, string Bid)
        {
            var R = RethinkDb.Driver.RethinkDB.R;
            var Conn = R.Connection().Hostname("localhost").Port(28015).Timeout(60).Connect();
            bool Check = R.Db("MyCRM").Table("Purchases").Get(id).IsEmpty().Run(Conn);

            if (Check == true)
            {
                return RedirectToAction("ViewOne", "CRM", new { id = Bid });
            }

            var Query = R.Db("MyCRM").Table("Purchases").Get(id).Delete().Run(Conn);

            return RedirectToAction("ViewOne", "CRM", new { id = Bid });
        }
    }
}
