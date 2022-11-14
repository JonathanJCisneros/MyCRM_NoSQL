using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyCRMNoSQL.Models;
using MyCRMNoSQL.CustomExtensions;
using RethinkDb.Driver;
using System.Diagnostics.Contracts;
using System.Security.Cryptography;

namespace MyCRMNoSQL.Controllers
{
    public class AddressController : Controller
    {
        private readonly ILogger<AddressController> _logger;

        public AddressController(ILogger<AddressController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Add(string id, Address Address)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("ViewOne", "CRM", new { id = id });
            }

            Address = Address.DbPrep(Address);

            var R = RethinkDb.Driver.RethinkDB.R;
            var Conn = R.Connection().Hostname("localhost").Port(28015).Timeout(60).Connect();
            var Query = R.Db("MyCRM").Table("Addresses")
                .Insert(new
                {
                    Street = Address.Street,
                    AptSuite = Address.AptSuite,
                    City = Address.City,
                    ZipCode = Address.ZipCode,
                    CreatedDate = Address.CreatedDate,
                    UpdatedDate = Address.UpdatedDate,
                    BusinessId = id
                })
            .Run(Conn);

            return RedirectToAction("ViewOne", "CRM", new { id = id });
        }

        [HttpPost]
        public IActionResult Update(string id, string Bid, Address Address) 
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("ViewOne", "CRM", new { id = Bid });
            }

            Address = Address.DbPrep(Address);

            var R = RethinkDb.Driver.RethinkDB.R;
            var Conn = R.Connection().Hostname("localhost").Port(28015).Timeout(60).Connect();
            bool Check = R.Db("MyCRM").Table("Addresses").Get(id).IsEmpty().Run(Conn);

            if (Check == true)
            {
                return RedirectToAction("ViewOne", "CRM", new { id = Bid });
            }

            var Query = R.Db("MyCRM").Table("Addresses")
                .Update(new
                {
                    Street = Address.Street,
                    AptSuite = Address.AptSuite,
                    City = Address.City,
                    ZipCode = Address.ZipCode,
                    UpdatedDate = Address.UpdatedDate
                })
            .Run(Conn);

            return RedirectToAction("ViewOne", "CRM", new { id = Bid });
        }

        public IActionResult Delete(string id, string Bid)
        {
            var R = RethinkDb.Driver.RethinkDB.R;
            var Conn = R.Connection().Hostname("localhost").Port(28015).Timeout(60).Connect();
            bool Check = R.Db("MyCRM").Table("Addresses").Get(id).IsEmpty().Run(Conn);

            if (Check == true)
            {
                return RedirectToAction("ViewOne", "CRM", new { id = Bid });
            }

            var Query = R.Db("MyCRM").Table("Addresses").Get(id).Delete().Run(Conn);

            return RedirectToAction("ViewOne", "CRM", new { id = Bid });
        }
    }
}
