using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyCRMNoSQL.Models;
using MyCRMNoSQL.Core;
using MyCRMNoSQL.CustomExtensions;
using RethinkDb.Driver;
using System.Diagnostics.Contracts;

namespace MyCRMNoSQL.Controllers
{
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;

        public ProductController(ILogger<ProductController> logger)
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

        //[HttpPost]
        //public IActionResult Add(string id, ProductFormModel Product)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return RedirectToAction("ViewOne", "CRM", new { id = id });
        //    }

        //    Product = ProductFormModel.DbPrep(Product);

        //    var R = RethinkDb.Driver.RethinkDB.R;
        //    var Conn = R.Connection().Hostname("localhost").Port(28015).Timeout(60).Connect();
        //    var Query = R.Db("MyCRM").Table("Products")
        //        .Insert(new
        //        {
        //            BusinessId = id,
        //            UserId = Uid,
        //            Name = Product.Name,
        //            Description = Product.Description,
        //            Price = Product.Price,
        //            CreatedDate = Product.CreatedDate,
        //            UpdatedDate = Product.UpdatedDate
        //        })
        //    .Run(Conn);

        //    return RedirectToAction("ViewOne", "CRM", new { id = id });
        //}

        //[HttpPost]
        //public IActionResult Update(string id, string Bid, ProductFormModel Product)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return RedirectToAction("ViewOne", "CRM", new { id = Bid });
        //    }

        //    Product = ProductFormModel.DbPrep(Product);

        //    var R = RethinkDb.Driver.RethinkDB.R;
        //    var Conn = R.Connection().Hostname("localhost").Port(28015).Timeout(60).Connect();
        //    bool Check = R.Db("MyCRM").Table("Products").Get(id).IsEmpty().Run(Conn);

        //    if (Check == true)
        //    {
        //        return RedirectToAction("ViewOne", "CRM", new { id = Bid });
        //    }

        //    var Query = R.Db("MyCRM").Table("Products")
        //        .Update(new
        //        {
        //            BusinessId = Bid,
        //            UserId = Uid,
        //            Name = Product.Name,
        //            Description = Product.Description,
        //            Price = Product.Price,
        //            UpdatedDate = Product.UpdatedDate
        //        })
        //    .Run(Conn);

        //    return RedirectToAction("ViewOne", "CRM", new { id = Bid });
        //}

        //public IActionResult Delete(string id, string Bid)
        //{
        //    var R = RethinkDb.Driver.RethinkDB.R;
        //    var Conn = R.Connection().Hostname("localhost").Port(28015).Timeout(60).Connect();
        //    bool Check = R.Db("MyCRM").Table("Products").Get(id).IsEmpty().Run(Conn);

        //    if (Check == true)
        //    {
        //        return RedirectToAction("ViewOne", "CRM", new { id = Bid });
        //    }

        //    var Query = R.Db("MyCRM").Table("Products").Get(id).Delete().Run(Conn);

        //    return RedirectToAction("ViewOne", "CRM", new { id = Bid });
        //}
    }
}
