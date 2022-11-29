using Microsoft.AspNetCore.Mvc;
using MyCRMNoSQL.Core;
using MyCRMNoSQL.Models;
using MyCRMNoSQL.Service.Interfaces;

namespace MyCRMNoSQL.Web.Controllers
{
    public class ClientActivityController : Controller
    {
        private readonly ILogger<ClientActivityController> _logger;
        private readonly IExtension _extension;
        private readonly IClientActivityService _clientActivityService;

        public ClientActivityController(ILogger<ClientActivityController> logger, IExtension extension, IClientActivityService clientActivityService)
        {
            _logger = logger;
            _extension = extension;
            _clientActivityService = clientActivityService;
        }

        public IActionResult Get(string id)
        {
            if (!_extension.LoggedIn())
            {
                return RedirectToAction("Login", "User");
            }

            ClientActivity activity = _clientActivityService.Get(id);

            return View(activity);
        }

        [HttpPost]
        public IActionResult Add(string id, ClientActivityFormModel Activity)
        {
            Activity.BusinessId = id;

            if (!ModelState.IsValid)
            {
                return RedirectToAction("ViewOne", "CRM", new { id = id });
            }

            Activity = ClientActivityFormModel.DbPrep(Activity);

            ClientActivity activity = new()
            {
                UserId = Activity.UserId,
                BusinessId = Activity.BusinessId,
                StaffId = Activity.StaffId,
                Type = Activity.Type,
                Note = Activity.Note,
                CreatedDate = Activity.CreatedDate,
                UpdatedDate = Activity.UpdatedDate,
            };

            string Id = _clientActivityService.Create(activity);

            return RedirectToAction("ViewOne", "CRM", new { id = id });
        }

        [HttpPost]
        public IActionResult Update(string id, string Bid, ClientActivityFormModel Activity)
        {
            Activity.BusinessId = Bid;

            if (!ModelState.IsValid)
            {
                return RedirectToAction("ViewOne", "CRM", new { id = Bid });
            }

            Activity = ClientActivityFormModel.DbPrep(Activity);

            ClientActivity activity = new()
            {
                Id = id,
                UserId = Activity.UserId,
                BusinessId = Activity.BusinessId,
                StaffId = Activity.StaffId,
                Type = Activity.Type,
                Note = Activity.Note,
                UpdatedDate = Activity.UpdatedDate,
            };

            string Id = _clientActivityService.Update(activity);

            return RedirectToAction("ViewOne", "CRM", new { id = Bid });
        }

        public IActionResult Delete(string id, string Bid)
        {
            if (!_extension.LoggedIn())
            {
                return RedirectToAction("Login", "User");
            }

            bool Success = _clientActivityService.Delete(id);

            if(!Success)
            {
                return Content("Delete didn't work out");
            }

            return RedirectToAction("ViewOne", "CRM", new { id = Bid });
        }
    }
}
