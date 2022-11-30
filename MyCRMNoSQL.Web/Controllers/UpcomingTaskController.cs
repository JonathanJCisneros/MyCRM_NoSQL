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
    public class UpcomingTaskController : Controller
    {
        private readonly ILogger<UpcomingTaskController> _logger;
        private readonly IExtension _extension;
        private readonly IUpcomingTaskService _upcomingTaskService;

        public UpcomingTaskController(ILogger<UpcomingTaskController> logger, IExtension extension, IUpcomingTaskService upcomingTaskService)
        {
            _logger = logger;
            _extension = extension;
            _upcomingTaskService = upcomingTaskService;
        }

        public IActionResult Get(string id)
        {
            if (!_extension.LoggedIn())
            {
                return RedirectToAction("Login", "User");
            }

            UpcomingTask task = _upcomingTaskService.Get(id);

            return View(task);
        }

        public IActionResult GetAll()
        {
            if (!_extension.LoggedIn())
            {
                return RedirectToAction("Login", "User");
            }

            List<UpcomingTask> taskList = _upcomingTaskService.GetAll();

            return View(taskList);
        }

        [HttpPost]
        public IActionResult Add(string id, UpcomingTaskFormModel Task)
        {
            Task.UserId = _extension.UserId();
            Task.BusinessId = id;

            if (!ModelState.IsValid)
            {
                return RedirectToAction("ViewOne", "CRM", new { id = id });
            }

            Task = UpcomingTaskFormModel.DbPrep(Task);

            UpcomingTask task = new()
            {
                UserId = Task.UserId,
                BusinessId = Task.BusinessId,
                StaffId = Task.StaffId,
                Type = Task.Type,
                Details = Task.Details,
                DueDate = Task.DueDate,
                Status = Task.Status,
                CreatedDate = Task.CreatedDate,
                UpdatedDate = Task.UpdatedDate,
            };

            string Id = _upcomingTaskService.Create(task);

            return RedirectToAction("ViewOne", "CRM", new { id = id });
        }

        [HttpPost]
        public IActionResult Update(string id, string Bid, UpcomingTaskFormModel Task)
        {
            Task.UserId = _extension.UserId();
            Task.BusinessId = Bid;

            if (!ModelState.IsValid)
            {
                return RedirectToAction("ViewOne", "CRM", new { id = Bid });
            }

            Task = UpcomingTaskFormModel.DbPrep(Task);

            UpcomingTask task = new()
            {
                Id = id,
                UserId = Task.UserId,
                BusinessId = Task.BusinessId,
                StaffId = Task.StaffId,
                Type = Task.Type,
                Details = Task.Details,
                DueDate = Task.DueDate,
                Status = Task.Status,
                UpdatedDate = Task.UpdatedDate
            };

            string Id = _upcomingTaskService.Update(task);

            return RedirectToAction("ViewOne", "CRM", new { id = id });
        }

        public IActionResult Delete(string id, string Bid)
        {
            if(!_extension.LoggedIn())
            {
                return RedirectToAction("Login", "User");
            }

            bool Success = _upcomingTaskService.Delete(id);

            if(!Success)
            {
                return Content("Something went wrong");
            }

            return RedirectToAction("ViewOne", "CRM", new { id = Bid });
        }
    }
}
