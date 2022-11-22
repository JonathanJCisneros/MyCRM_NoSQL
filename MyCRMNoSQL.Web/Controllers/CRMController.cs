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

        //public IActionResult ViewOne(string id)
        //{
        //    if (!LoggedIn)
        //    {
        //        return RedirectToAction("Login", "User");
        //    }

        //    var R = RethinkDb.Driver.RethinkDB.R;
        //    var Conn = R.Connection().Hostname("localhost").Port(28015).Timeout(60).Connect();
        //    bool Check = R.Db("MyCRM").Table("Businesses").Get(id).IsEmpty().Run(Conn);

        //    if (Check == true)
        //    {
        //        return RedirectToAction("Dashboard");
        //    }

        //    var Query = R.Db("MyCRM").Table("Businesses").Get(id).Pluck("id", "Name", "Industry", "StartDate", "Website", "PocId", "CreatedDate")
        //        .Merge(B => new
        //        {
        //            PointOfContact = R.Db("MyCRM").Table("Staff").Get(B["PocId"]).Pluck("FirstName", "LastName", "Position", "Email", "PhoneNumber"),
        //            NoteList = R.Db("MyCRM").Table("Notes").GetAll(B["id"])[new { index = "BusinessId" }].Pluck("id", "Details", "UserId", "CreatedDate")
        //                .Merge(N => new
        //                {
        //                    User = R.Db("MyCRM").Table("Users").Get(N["UserId"]).Pluck("FirstName", "LastName")
        //                }).CoerceTo("array"),
        //            TaskList = R.Db("MyCRM").Table("Tasks").GetAll(B["id"])[new { index = "BusinessId" }].Pluck("id", "UserId", "StaffId", "Type", "Details", "DueDate", "Status", "CreatedDate", "UpdatedDate")
        //                .Merge(T => new
        //                {
        //                    User = R.Db("MyCRM").Table("Users").Get(T["UserId"]).Pluck("FirstName", "LastName"),
        //                    Staff = R.Db("MyCRM").Table("Staff").Get(T["StaffId"]).Pluck("FirstName", "LastName", "Position")
        //                }).CoerceTo("array"),
        //            StaffList = R.Db("MyCRM").Table("Staff").GetAll(B["id"])[new { index = "BusinessId" }].Pluck("id", "FirstName", "LastName", "Position", "Email", "PhoneNumber").CoerceTo("array"),
        //            AddressList = R.Db("MyCRM").Table("Addresses").GetAll(B["id"])[new { index = "BusinessId" }].Pluck("id", "Street", "AptSuite", "City", "State", "ZipCode").CoerceTo("array"),
        //            PurchaseList = R.Db("MyCRM").Table("Purchases").GetAll(B["id"])[new { index = "BusinessId" }].Pluck("id", "ProductId", "AddressId", "UserId", "CreatedDate")
        //                .Merge(P => new 
        //                {
        //                    Product = R.Db("MyCRM").Table("Products").Get(P["ProductId"]).Pluck("Name", "Description", "Price"),
        //                    Address = R.Db("MyCRM").Table("Addresses").Get(P["AddressId"]).Pluck("Street", "AptSuite", "City", "State", "ZipCode")
        //                }).CoerceTo("array"),
        //            ActivityList = R.Db("MyCRM").Table("Activities").GetAll(B["id"])[new { index = "BusinessId"}].Pluck("id", "Type", "Description", "StaffId", "UserId", "CreatedDate")
        //                .Merge(A => new 
        //                {
        //                    User = R.Db("MyCRM").Table("Users").Get(A["UserId"]).Pluck("FirstName", "LastName"),
        //                    Staff = R.Db("MyCRM").Table("Staff").Get(A["StaffId"]).Pluck("FirstName", "LastName", "Position")
        //                }).OrderBy(R.Desc("CreatedDate")).CoerceTo("array")
        //        })
        //    .Run(Conn);

        //    List<BusinessActivity> ActivityList = new List<BusinessActivity>();

        //    foreach (var A in Query.ActivityList)
        //    {
        //        User AUser = new User()
        //        {
        //            FirstName = A.User.FirstName.ToString(),
        //            LastName = A.User.LastName.ToString()
        //        };

        //        StaffFormModel AStaff = new StaffFormModel()
        //        {
        //            FirstName = A.Staff.FirstName.ToString(),
        //            LastName = A.Staff.LastName.ToString(),
        //            Position = A.Staff.Position.ToString()
        //        };

        //        BusinessActivity Activity = new BusinessActivity()
        //        {
        //            ActivityId = A.id.ToString(),
        //            Type = A.Type.ToString(),
        //            Note = A.Description.ToString(),
        //            StaffId = A.StaffId.ToString(),
        //            UserId = A.UserId.ToString(),
        //            CreatedDate = A.CreatedDate.ToDateTime(),
        //            User = AUser,
        //            Staff = AStaff
        //        };

        //        ActivityList.Add(Activity);
        //    }

        //    List<PurchaseFormModel> PurchaseList = new List<PurchaseFormModel>();

        //    foreach (var p in Query.PurchaseList)
        //    {
        //        ProductFormModel Product = new ProductFormModel()
        //        {
        //            Name = p.Product.Name.ToString(),
        //            Description = p.Product.Description.ToString(),
        //            Price = p.Product.Price.ToInt32(),
        //        };

        //        Address Address = new Address()
        //        {
        //            Street = p.Address.Street.ToString(),
        //            AptSuite = p.Address.AptSuite.ToString(),
        //            City = p.Address.City.ToString(),
        //            State = p.Address.State.ToString(),
        //            ZipCode = p.Address.ZipCode.ToInt32(),
        //        };

        //        PurchaseFormModel Purchase = new PurchaseFormModel()
        //        {
        //            PurchaseId = p.id.ToString(),
        //            ProductId = p.productId.ToString(),
        //            AddressId = p.addressId.ToString(),
        //            UserId = p.userId.ToString(),
        //            CreatedDate = p.createdDate.ToDateTime(),
        //            Product = Product,
        //            Address = Address
        //        };

        //        PurchaseList.Add(Purchase);
        //    }

        //    List<UpcomingTaskFormModel> TaskList = new List<UpcomingTaskFormModel>();

        //    foreach (var t in Query.TaskList)
        //    {
        //        User User = new User()
        //        {
        //            FirstName = t.User.FirstName.ToString(),
        //            LastName = t.User.LastName.ToString()
        //        };

        //        StaffFormModel Staff = new StaffFormModel()
        //        {
        //            FirstName = t.Staff.FirstName.ToString(),
        //            LastName = t.Staff.LastName.ToString(),
        //            Position = t.Staff.Position.ToString(),
        //        };

        //        UpcomingTaskFormModel Task = new UpcomingTaskFormModel()
        //        {
        //            TaskId = t.TaskId.ToString(),
        //            UserId = t.UserId.ToString(),
        //            StaffId = t.StaffId.ToString(),
        //            Type = t.Type.ToString(),
        //            Details = t.Details.ToString(),
        //            DueDate = t.DueDate.ToDateTime(),
        //            Status = t.Status.ToString(),
        //            CreatedDate = t.CreatedDate.ToDateTime(),
        //            UpdatedDate = t.UpdatedDate.ToDateTime(),
        //            User = User,
        //            Staff = Staff
        //        };

        //        TaskList.Add(Task);
        //    }

        //    List<NoteFormModel> NoteList = new List<NoteFormModel>();

        //    foreach (var t in Query.NoteList)
        //    {
        //        User User = new User()
        //        {
        //            FirstName = t.User.FirstName.ToString(),
        //            LastName = t.User.LastName.ToString(),
        //        };

        //        NoteFormModel Note = new NoteFormModel()
        //        {
        //            NoteId = t.id.ToString(),
        //            Details = t.Details.ToString(),
        //            CreatedDate = t.CreatedDate.ToDateTime(),
        //            UserId = t.UserId.ToString(),
        //            User = User
        //        };

        //        NoteList.Add(Note);
        //    }

        //    List<StaffFormModel> StaffList = new List<StaffFormModel>();

        //    foreach (var e in Query.StaffList)
        //    {
        //        StaffFormModel Employee = new StaffFormModel()
        //        {
        //            StaffId = e.id.ToString(),
        //            FirstName = e.FirstName.ToString(),
        //            LastName = e.LastName.ToString(),
        //            Position = e.Position.ToString(),
        //            Email = e.Email.ToString(),
        //            PhoneNumber = e.PhoneNumber.ToInt32(),
        //        };

        //        StaffList.Add(Employee);
        //    }

        //    List<Address> AddressList = new List<Address>();

        //    foreach (var e in Query.AddressList)
        //    {
        //        Address Address = new Address()
        //        {
        //            AddressId = e.id.ToString(),
        //            Street = e.Street.ToString(),
        //            AptSuite = e.AptSuite.ToString(),
        //            City = e.City.ToString(),
        //            State = e.State.ToString(),
        //            ZipCode = e.ZipCode.ToString(),
        //        };

        //        AddressList.Add(Address);
        //    }

        //    StaffFormModel PointOfContact = new StaffFormModel()
        //    {
        //        FirstName = Query.PointOfContact.FirstName.ToString(),
        //        LastName = Query.PointOfContact.LastName.ToString(),
        //        Position = Query.PointOfContact.Position.ToString(),
        //        Email = Query.PointOfContact.Email.ToString(),
        //        PhoneNumber = Query.PointOfContact.PhoneNumber.ToInt32()
        //    };

        //    Business Business = new Business() 
        //    { 
        //        BusinessId = Query.id.ToString(),
        //        Name = Query.Name.ToString(),
        //        Website = Query.Website.ToString(),
        //        Industry = Query.Industry.ToString(),
        //        StartDate = Query.StartDate.ToDateTime(),
        //        PocId = Query.PocId.ToString(),
        //        PointOfContact = PointOfContact,
        //        CreatedDate = Query.CreatedDate.ToDateTime(),
        //        ActivityList = ActivityList,
        //        PurchaseList = PurchaseList,
        //        TaskList = TaskList,
        //        NoteList = NoteList,
        //        StaffList = StaffList,
        //        AddressList = AddressList
        //    };

        //    return View(Business);
        //}

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
