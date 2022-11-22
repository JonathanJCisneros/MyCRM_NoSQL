using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyCRMNoSQL.Models;
using MyCRMNoSQL.Core;
using RethinkDb.Driver;
using System.Diagnostics.Contracts;

namespace MyCRMNoSQL.Web.Controllers
{
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;

        public AdminController(ILogger<AdminController> logger)
        {
            _logger = logger;
        }

        public ActionResult Dashboard() 
        {
            return View();
        }
    }
}
