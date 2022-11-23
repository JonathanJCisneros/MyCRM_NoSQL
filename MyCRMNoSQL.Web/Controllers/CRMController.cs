using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyCRMNoSQL.Models;
using RethinkDb.Driver;
using MyCRMNoSQL.Core;
using MyCRMNoSQL.Service.Interfaces;

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

            string Id = _businessService.Create(business, address, staff);

            Console.WriteLine(Id);

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

        //[HttpPost]
        //public IActionResult Update(string id, Business Business)
        //{

        //    var R = RethinkDb.Driver.RethinkDB.R;
        //    var Conn = R.Connection().Hostname("localhost").Port(28015).Timeout(60).Connect();
        //    bool Check = R.Db("MyCRM").Table("Businesses").Get(id).IsEmpty().Run(Conn);

        //    if (Check == true)
        //    {
        //        return RedirectToAction("Dashboard");
        //    }

        //    if(Business.Name != null)
        //    {
        //        Business.Name = Business.Name.Trim();

        //        var Query = R.Db("MyCRM").Table("Businesses").Get(id)
        //            .Update(new
        //            {
        //                Name = Business.Name,
        //                UpdatedDate = DateTime.Now
        //            })
        //        .Run(Conn);
        //    }

        //    if(Business.Website != null)
        //    {
        //        Business.Website = Business.Website.Trim().ToLower();

        //        var Query = R.Db("MyCRM").Table("Businesses").Get(id)
        //            .Update(new
        //            {
        //                Website = Business.Website,
        //                UpdatedDate = DateTime.Now
        //            })
        //        .Run(Conn);
        //    }

        //    if(Business.Industry != null)
        //    {
        //        Business.Industry = MyExtensions.StringToUpper(Business.Industry);

        //        var Query = R.Db("MyCRM").Table("Businesses").Get(id)
        //            .Update(new
        //            {
        //                Industry = Business.Industry,
        //                UpdatedDate = DateTime.Now
        //            })
        //        .Run(Conn);
        //    }

        //    if(Business.PocId != null)
        //    {
        //        Business.PocId = Business.PocId.Trim();

        //        var Query = R.Db("MyCRM").Table("Businesses").Get(id)
        //            .Update(new
        //            {
        //                PocId = Business.PocId,
        //                UpdatedDate = DateTime.Now
        //            })
        //        .Run(Conn);
        //    }

        //    return RedirectToAction("ViewOne", new { id = id});
        //}

        //public IActionResult Delete(string id)
        //{
        //    var R = RethinkDb.Driver.RethinkDB.R;
        //    var Conn = R.Connection().Hostname("localhost").Port(28015).Timeout(60).Connect();
        //    bool Check = R.Db("MyCRM").Table("Businesses").Get(id).IsEmpty().Run(Conn);

        //    if (Check == true)
        //    {
        //        return RedirectToAction("Dashboard");
        //    }

        //    var BQuery = R.Db("MyCRM").Table("Businesses").Get(id).Delete().Run(Conn);
        //    var BAQuery = R.Db("MyCRM").Table("Activities").GetAll(id)[new { index = "BusinessId"}].Delete().Run(Conn);
        //    var PQuery = R.Db("MyCRM").Table("Purchases").GetAll(id)[new { index = "BusinessId" }].Delete().Run(Conn);
        //    var TQuery = R.Db("MyCRM").Table("Tasks").GetAll(id)[new { index = "BusinessId" }].Delete().Run(Conn);
        //    var NQuery = R.Db("MyCRM").Table("Notes").GetAll(id)[new { index = "BusinessId" }].Delete().Run(Conn);
        //    var SQuery = R.Db("MyCRM").Table("Staff").GetAll(id)[new { index = "BusinessId" }].Delete().Run(Conn);
        //    var AQuery = R.Db("MyCRM").Table("Addresses").GetAll(id)[new { index = "BusinessId" }].Delete().Run(Conn);

        //    return View("Dashboard");
        //}
    }
}
