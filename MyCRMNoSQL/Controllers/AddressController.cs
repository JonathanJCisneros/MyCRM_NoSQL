using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyCRMNoSQL.Models;
using MyCRMNoSQL.CustomExtensions;
using RethinkDb.Driver;
using System.Diagnostics.Contracts;

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
            return RedirectToAction("ViewOne", "CRM", new { id = id });
        }

        [HttpPost]
        public IActionResult Update(string id, Address Address) 
        {
            return RedirectToAction("ViewOne", "CRM", new { id = id });
        }

        public IActionResult Delete(string id)
        {
            return RedirectToAction("ViewOne", "CRM", new { id = id });
        }
    }
}
