using Microsoft.AspNetCore.Mvc;
using MyCRMNoSQL.Models;
using MyCRMNoSQL.Core;
using MyCRMNoSQL.Service.Interfaces;
#pragma warning disable CS8603

namespace MyCRMNoSQL.Web.Controllers
{
    public class CRMController : Controller
    {
        private readonly ILogger<CRMController> _logger;
        private readonly IExtension _extension;
        private readonly IBusinessService _businessService;
        private readonly IAddressService _addressService;
        private readonly IStaffService _staffService;
        private readonly INoteService _noteService;
        private readonly IUpcomingTaskService _upcomingTask;
        private readonly IPurchaseService _purchaseService;
        private readonly IClientActivityService _clientActivityService;


        public CRMController(
            ILogger<CRMController> logger,
            IExtension extension,
            IBusinessService businessService,
            IAddressService addressService,
            IStaffService staffService,
            INoteService noteService,
            IUpcomingTaskService upcomingTaskService,
            IPurchaseService purchaseService,
            IClientActivityService clientActivityService)
        {
            _logger = logger;
            _extension = extension;
            _businessService = businessService;
            _addressService = addressService;
            _staffService = staffService;
            _noteService = noteService;
            _upcomingTask = upcomingTaskService;
            _purchaseService = purchaseService;
            _clientActivityService = clientActivityService;
        }

        public IActionResult Dashboard()
        {
            if(!_extension.LoggedIn())
            {
                return RedirectToAction("Login", "User");
            }

            List<Business> businessList = _businessService.GetAllWithLatestActivity();

            return View("Dashboard", businessList);
        }

        public IActionResult New()
        {
            if (!_extension.LoggedIn())
            {
                return RedirectToAction("Login", "User");
            }

            return View("NewBusiness");
        }

        public IActionResult NewClient(NewBusinessFormModel newBiz)
        {
            if (!ModelState.IsValid)
            {
                return New();
            }

            newBiz = NewBusinessFormModel.DbPrep(newBiz);

            bool Check = _businessService.CheckByName(newBiz.Name);

            if (Check == false)
            {
                ModelState.AddModelError("Name", "already exists");
                return New();
            }

            Business business = new()
            {
                Name = newBiz.Name,
                Industry = newBiz.Industry,
                Website = newBiz.Website,
                StartDate = newBiz.StartDate,
                UserId = _extension.UserId(),
                CreatedDate = newBiz.CreatedDate,
                UpdatedDate = newBiz.UpdatedDate
            };

            Address address = new()
            {
                Street = newBiz.Street,
                AptSuite = newBiz.AptSuite,
                City = newBiz.City,
                State = newBiz.State,
                CreatedDate = newBiz.CreatedDate,
                UpdatedDate = newBiz.UpdatedDate
            };

            Staff staff = new()
            {
                Position = newBiz.Position,
                FirstName = newBiz.FirstName,
                LastName = newBiz.LastName,
                PhoneNumber = newBiz.PhoneNumber,
                Email = newBiz.Email,
                CreatedDate = newBiz.CreatedDate,
                UpdatedDate = newBiz.UpdatedDate
            };

            string Id = _businessService.CreateClient(business, address, staff);

            return Dashboard();
        }

        public IActionResult ViewOne(string id)
        {
            if (!_extension.LoggedIn())
            {
                return RedirectToAction("Login", "User");
            }

            Business business = _businessService.Get(id);

            return View(business);
        }

        [HttpPost]
        public IActionResult Update(string id, BusinessFormModel businessForm)
        {
            businessForm = BusinessFormModel.DbPrep(businessForm);

            Business business = new()
            {
                Id = id,
                Name = businessForm.Name,
                Industry = businessForm.Industry,
                Website = businessForm.Website,
                PocId = businessForm.PocId,
                UpdatedDate = businessForm.UpdatedDate
            }; 

            string Id = _businessService.Update(business);

            return RedirectToAction("ViewOne", new { id = Id });
        }

        public IActionResult Delete(string id)
        {
            bool Success = _businessService.Delete(id);

            if (!Success)
            {
                return Content("Something went wrong...");
            }

            return Dashboard();
        }
    }
}
