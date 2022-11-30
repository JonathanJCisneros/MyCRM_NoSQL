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
    public class StaffController : Controller
    {
        private readonly ILogger<StaffController> _logger;
        private readonly IExtension _extension;
        private readonly IStaffService _staffService;

        public StaffController(ILogger<StaffController> logger, IExtension extension, IStaffService staffService)
        {
            _logger = logger;
            _extension = extension;
            _staffService = staffService;
        }

        public IActionResult Get(string id)
        {
            if (!_extension.LoggedIn())
            {
                return RedirectToAction("Login", "User");
            }

            Staff staff = _staffService.Get(id);

            return View(staff);
        }

        public IActionResult GetAll()
        {
            if (!_extension.LoggedIn())
            {
                return RedirectToAction("Login", "User");
            }

            List<Staff> staffList = _staffService.GetAll();

            return View(staffList);
        }

        [HttpPost]
        public IActionResult Add(string id, StaffFormModel Staff)
        {
            Staff.BusinessId = id;

            if (!ModelState.IsValid)
            {
                return RedirectToAction("ViewOne", "CRM", new { id = id });
            }

            Staff = StaffFormModel.DbPrep(Staff);

            Staff staff = new()
            {
                Position = Staff.Position,
                FirstName = Staff.FirstName,
                LastName = Staff.LastName,
                PhoneNumber = Staff.PhoneNumber,
                Email = Staff.Email,
                BusinessId = Staff.BusinessId,
                CreatedDate = Staff.CreatedDate,
                UpdatedDate = Staff.UpdatedDate
            };

            string Id = _staffService.Create(staff);

            return RedirectToAction("ViewOne", "CRM", new { id = id });
        }

        [HttpPost]
        public IActionResult Update(string id, string Bid, StaffFormModel Staff)
        {
            Staff.BusinessId = Bid;

            if (!ModelState.IsValid)
            {
                return RedirectToAction("ViewOne", "CRM", new { id = Bid });
            }

            Staff = StaffFormModel.DbPrep(Staff);

            Staff staff = new()
            {
                Id = id,
                Position = Staff.Position,
                FirstName = Staff.FirstName,
                LastName = Staff.LastName,
                PhoneNumber = Staff.PhoneNumber,
                Email = Staff.Email,
                BusinessId = Staff.BusinessId,
                UpdatedDate = Staff.UpdatedDate
            };

            string Id = _staffService.Update(staff);

            return RedirectToAction("ViewOne", "CRM", new { id = Bid });
        }

        public IActionResult Delete(string id, string Bid)
        {
            if (!_extension.LoggedIn())
            {
                return RedirectToAction("Login", "User");
            }

            bool Success = _staffService.Delete(id);

            if(!Success)
            {
                return Content("Something went wrong...");
            }

            return RedirectToAction("ViewOne", "CRM", new { id = Bid });
        }
    }
}
