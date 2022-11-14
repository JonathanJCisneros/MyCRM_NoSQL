using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyCRMNoSQL.Models;
using RethinkDb.Driver;
using System.Diagnostics.Contracts;

namespace MyCRMNoSQL.Controllers
{
    public class CRMController : Controller
    {
        private readonly ILogger<CRMController> _logger;

        public CRMController(ILogger<CRMController> logger)
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

        private bool LoggedIn
        {
            get
            {
                return Uid != null;
            }
        }

        public string StringToUpper(string s)
        {
            s = s.Trim().ToLower();
            return char.ToUpper(s[0]) + s.Substring(1);
        }

        public IActionResult Dashboard()
        {
            if(!LoggedIn)
            {
                return RedirectToAction("Login", "User");
            }

            var R = RethinkDb.Driver.RethinkDB.R;
            var Conn = R.Connection().Hostname("localhost").Port(28015).Timeout(60).Connect();
            var Query = R.Db("MyCRM").Table("Businesses").Pluck("id", "Name", "PocId")
                .Merge(b => new 
                {
                    PointOfContact = R.Db("MyCRM").Table("Staff").Get(b["PocId"]).Pluck("FirstName", "LastName"),
                    LatestActivity = R.Db("MyCRM").Table("Activities").GetAll(b["id"])[new { index = "BusinessId" }].Pluck("Type").OrderBy(R.Desc("CreatedDate")).Limit(1).CoerceTo("array")
                })
            .Run(Conn);


            List<Business> BusinessList = new List<Business>();

            foreach (var i in Query)
            {
                BusinessActivity Activity = new BusinessActivity()
                {
                    Type = i.LatestActivity[0].Type.ToString()             
                };

                Staff POC = new Staff()
                {
                    FirstName = i.PointOfContact.FirstName.ToString(),
                    LastName = i.PointOfContact.LastName.ToString(),
                };

                Business Business = new Business()
                {
                    BusinessId= i.id.ToString(),
                    Name = i.Name.ToString(),
                    PocId = i.id.ToString(),
                    PointOfContact = POC,
                    LatestActivity = Activity
                };

                BusinessList.Add(Business);
            }

            return View("Dashboard", BusinessList);
        }

        public IActionResult New() 
        {
            if (!LoggedIn)
            {
                return RedirectToAction("Login", "User");
            }
            return View("NewBusiness");
        }

        public IActionResult NewClient(NewBusinessForm NewBiz)
        {
            if (!ModelState.IsValid)
            {
                return New();
            }

            NewBiz.Name = StringToUpper(NewBiz.Name);
            NewBiz.Industry = StringToUpper(NewBiz.Industry);
            NewBiz.Website = NewBiz.Website.Trim();
            NewBiz.Street = NewBiz.Street.Trim();
            NewBiz.State = NewBiz.State.Trim();
            NewBiz.FirstName = StringToUpper(NewBiz.FirstName);
            NewBiz.LastName = StringToUpper(NewBiz.LastName);
            NewBiz.Position = StringToUpper(NewBiz.Position);
            NewBiz.Email = NewBiz.Email.Trim().ToLower();

            var R = RethinkDb.Driver.RethinkDB.R;
            var Conn = R.Connection().Hostname("localhost").Port(28015).Timeout(60).Connect();
            long Check = R.Db("MyCRM").Table("Users").GetAll(NewBiz.Name)[new { index = "Name" }].IsEmpty().Run(Conn);

            if (Check > 0)
            {
                ModelState.AddModelError("Name", "already exists");
                return New();
            }

            var BResult = R.Db("MyCRM").Table("Businesses")
                .Insert(new
                {
                    Name = NewBiz.Name,
                    Industry = NewBiz.Industry,
                    Website = NewBiz.Website,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                    UserId = Uid
                })
            .Run(Conn);

            string BId = BResult.generated_keys[0].ToString();

            var AResult = R.Db("MyCRM").Table("Addresses")
                .Insert(new
                {
                    Street = NewBiz.Street,
                    AptSuite = NewBiz.AptSuite,
                    City = NewBiz.City,
                    State = NewBiz.State,
                    ZipCode = NewBiz.ZipCode,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                    BusinessId = BId
                })
            .Run(Conn);

            var SResult = R.Db("MyCRM").Table("Staff")
                .Insert(new
                {
                    FirstName = NewBiz.FirstName,
                    LastName = NewBiz.LastName,
                    Position = NewBiz.Position,
                    Email = NewBiz.Email,
                    PhoneNumber = NewBiz.PhoneNumber,
                    CreatedDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                    BusinessId = BId
                })
            .Run(Conn);

            string SId = SResult.generated_keys[0].ToString();

            var UBiz = R.Db("MyCRM").Table("Businesses").Get(BId)
                .Update(new
                {
                    PocId = SId
                })
            .Run(Conn);

            return Dashboard();
        }

        public IActionResult ViewOne(string id)
        {
            if (!LoggedIn)
            {
                return RedirectToAction("Login", "User");
            }

            var R = RethinkDb.Driver.RethinkDB.R;
            var Conn = R.Connection().Hostname("localhost").Port(28015).Timeout(60).Connect();
            bool Check = R.Db("MyCRM").Table("Businesses").Get(id).IsEmpty().Run(Conn);

            if (Check == true)
            {
                return RedirectToAction("Dashboard");
            }

            var Query = R.Db("MyCRM").Table("Businesses").Get(id).Pluck("id", "Name", "Industry", "StartDate", "Website", "PocId", "CreatedDate")
                .Merge(B => new
                {
                    PointOfContact = R.Db("MyCRM").Table("Staff").Get(B["PocId"]).Pluck("FirstName", "LastName", "Position", "Email", "PhoneNumber"),
                    NoteList = R.Db("MyCRM").Table("Notes").GetAll(B["id"])[new { index = "BusinessId" }].Pluck("id", "Details", "UserId", "CreatedDate")
                        .Merge(N => new
                        {
                            User = R.Db("MyCRM").Table("Users").Get(N["UserId"]).Pluck("FirstName", "LastName")
                        }).CoerceTo("array"),
                    TaskList = R.Db("MyCRM").Table("Tasks").GetAll(B["id"])[new { index = "BusinessId" }].Pluck("id", "UserId", "StaffId", "Type", "Details", "DueDate", "Status", "CreatedDate", "UpdatedDate")
                        .Merge(T => new
                        {
                            User = R.Db("MyCRM").Table("Users").Get(T["UserId"]).Pluck("FirstName", "LastName"),
                            Staff = R.Db("MyCRM").Table("Staff").Get(T["StaffId"]).Pluck("FirstName", "LastName", "Position")
                        }).CoerceTo("array"),
                    StaffList = R.Db("MyCRM").Table("Staff").GetAll(B["id"])[new { index = "BusinessId" }].Pluck("id", "FirstName", "LastName", "Position", "Email", "PhoneNumber").CoerceTo("array"),
                    AddressList = R.Db("MyCRM").Table("Addresses").GetAll(B["id"])[new { index = "BusinessId" }].Pluck("id", "Street", "AptSuite", "City", "State", "ZipCode").CoerceTo("array"),
                    PurchaseList = R.Db("MyCRM").Table("Purchases").GetAll(B["id"])[new { index = "BusinessId" }].Pluck("id", "ProductId", "AddressId", "UserId", "CreatedDate")
                        .Merge(P => new 
                        {
                            Product = R.Db("MyCRM").Table("Products").Get(P["ProductId"]).Pluck("Name", "Description", "Price"),
                            Address = R.Db("MyCRM").Table("Addresses").Get(P["AddressId"]).Pluck("Street", "AptSuite", "City", "State", "ZipCode")
                        }).CoerceTo("array"),
                    ActivityList = R.Db("MyCRM").Table("Activities").GetAll(B["id"])[new { index = "BusinessId"}].Pluck("id", "Type", "Description", "StaffId", "UserId", "CreatedDate")
                        .Merge(A => new 
                        {
                            User = R.Db("MyCRM").Table("Users").Get(A["UserId"]).Pluck("FirstName", "LastName"),
                            Staff = R.Db("MyCRM").Table("Staff").Get(A["StaffId"]).Pluck("FirstName", "LastName", "Position")
                        }).OrderBy(R.Desc("CreatedDate")).CoerceTo("array")
                })
            .Run(Conn);

            List<BusinessActivity> ActivityList = new List<BusinessActivity>();

            foreach (var A in Query.ActivityList)
            {
                User AUser = new User()
                {
                    FirstName = A.User.FirstName.ToString(),
                    LastName = A.User.LastName.ToString()
                };

                Staff AStaff = new Staff()
                {
                    FirstName = A.Staff.FirstName.ToString(),
                    LastName = A.Staff.LastName.ToString(),
                    Position = A.Staff.Position.ToString()
                };

                BusinessActivity Activity = new BusinessActivity()
                {
                    ActivityId = A.id.ToString(),
                    Type = A.Type.ToString(),
                    Note = A.Description.ToString(),
                    StaffId = A.StaffId.ToString(),
                    UserId = A.UserId.ToString(),
                    CreatedDate = A.CreatedDate.ToDateTime(),
                    User = AUser,
                    Staff = AStaff
                };

                ActivityList.Add(Activity);
            }

            List<Purchase> PurchaseList = new List<Purchase>();

            foreach (var p in Query.PurchaseList)
            {
                Product Product = new Product()
                {
                    Name = p.Product.Name.ToString(),
                    Description = p.Product.Description.ToString(),
                    Price = p.Product.Price.ToInt32(),
                };

                Address Address = new Address()
                {
                    Street = p.Address.Street.ToString(),
                    AptSuite = p.Address.AptSuite.ToString(),
                    City = p.Address.City.ToString(),
                    State = p.Address.State.ToString(),
                    ZipCode = p.Address.ZipCode.ToInt32(),
                };

                Purchase Purchase = new Purchase()
                {
                    PurchaseId = p.id.ToString(),
                    ProductId = p.productId.ToString(),
                    AddressId = p.addressId.ToString(),
                    UserId = p.userId.ToString(),
                    CreatedDate = p.createdDate.ToDateTime(),
                    Product = Product,
                    Address = Address
                };

                PurchaseList.Add(Purchase);
            }

            List<UpcomingTask> TaskList = new List<UpcomingTask>();

            foreach (var t in Query.TaskList)
            {
                User User = new User()
                {
                    FirstName = t.User.FirstName.ToString(),
                    LastName = t.User.LastName.ToString()
                };

                Staff Staff = new Staff()
                {
                    FirstName = t.Staff.FirstName.ToString(),
                    LastName = t.Staff.LastName.ToString(),
                    Position = t.Staff.Position.ToString(),
                };

                UpcomingTask Task = new UpcomingTask()
                {
                    TaskId = t.TaskId.ToString(),
                    UserId = t.UserId.ToString(),
                    StaffId = t.StaffId.ToString(),
                    Type = t.Type.ToString(),
                    Details = t.Details.ToString(),
                    DueDate = t.DueDate.ToDateTime(),
                    Status = t.Status.ToString(),
                    CreatedDate = t.CreatedDate.ToDateTime(),
                    UpdatedDate = t.UpdatedDate.ToDateTime(),
                    User = User,
                    Staff = Staff
                };

                TaskList.Add(Task);
            }

            List<Note> NoteList = new List<Note>();

            foreach (var t in Query.NoteList)
            {
                User User = new User()
                {
                    FirstName = t.User.FirstName.ToString(),
                    LastName = t.User.LastName.ToString(),
                };

                Note Note = new Note()
                {
                    NoteId = t.id.ToString(),
                    Details = t.Details.ToString(),
                    CreatedDate = t.CreatedDate.ToDateTime(),
                    UserId = t.UserId.ToString(),
                    User = User
                };

                NoteList.Add(Note);
            }

            List<Staff> StaffList = new List<Staff>();

            foreach (var e in Query.StaffList)
            {
                Staff Employee = new Staff()
                {
                    StaffId = e.id.ToString(),
                    FirstName = e.FirstName.ToString(),
                    LastName = e.LastName.ToString(),
                    Position = e.Position.ToString(),
                    Email = e.Email.ToString(),
                    PhoneNumber = e.PhoneNumber.ToInt32(),
                };

                StaffList.Add(Employee);
            }

            List<Address> AddressList = new List<Address>();

            foreach (var e in Query.AddressList)
            {
                Address Address = new Address()
                {
                    AddressId = e.id.ToString(),
                    Street = e.Street.ToString(),
                    AptSuite = e.AptSuite.ToString(),
                    City = e.City.ToString(),
                    State = e.State.ToString(),
                    ZipCode = e.ZipCode.ToString(),
                };

                AddressList.Add(Address);
            }

            Staff PointOfContact = new Staff()
            {
                FirstName = Query.PointOfContact.FirstName.ToString(),
                LastName = Query.PointOfContact.LastName.ToString(),
                Position = Query.PointOfContact.Position.ToString(),
                Email = Query.PointOfContact.Email.ToString(),
                PhoneNumber = Query.PointOfContact.PhoneNumber.ToInt32()
            };

            Business Business = new Business() 
            { 
                BusinessId = Query.id.ToString(),
                Name = Query.Name.ToString(),
                Website = Query.Website.ToString(),
                Industry = Query.Industry.ToString(),
                StartDate = Query.StartDate.ToDateTime(),
                PocId = Query.PocId.ToString(),
                PointOfContact = PointOfContact,
                CreatedDate = Query.CreatedDate.ToDateTime(),
                ActivityList = ActivityList,
                PurchaseList = PurchaseList,
                TaskList = TaskList,
                NoteList = NoteList,
                StaffList = StaffList,
                AddressList = AddressList
            };

            return View(Business);
        }

        [HttpPost]
        public IActionResult Update(string id, Business Business)
        {

            var R = RethinkDb.Driver.RethinkDB.R;
            var Conn = R.Connection().Hostname("localhost").Port(28015).Timeout(60).Connect();
            bool Check = R.Db("MyCRM").Table("Businesses").Get(id).IsEmpty().Run(Conn);

            if (Check == true)
            {
                return RedirectToAction("Dashboard");
            }

            if(Business.Name != null)
            {
                Business.Name = Business.Name.Trim();

                var Query = R.Db("MyCRM").Table("Businesses").Get(id)
                    .Update(new
                    {
                        Name = Business.Name,
                        UpdatedDate = DateTime.Now
                    }).Run(Conn);
            }

            if(Business.Website != null)
            {
                Business.Website = Business.Website.Trim().ToLower();

                var Query = R.Db("MyCRM").Table("Businesses").Get(id)
                    .Update(new
                    {
                        Website = Business.Website,
                        UpdatedDate = DateTime.Now
                    }).Run(Conn);
            }

            if(Business.Industry != null)
            {
                Business.Industry = StringToUpper(Business.Industry);

                var Query = R.Db("MyCRM").Table("Businesses").Get(id)
                    .Update(new
                    {
                        Industry = Business.Industry,
                        UpdatedDate = DateTime.Now
                    }).Run(Conn);
            }

            if(Business.PocId != null)
            {
                Business.PocId = Business.PocId.Trim();

                var Query = R.Db("MyCRM").Table("Businesses").Get(id)
                    .Update(new
                    {
                        PocId = Business.PocId,
                        UpdatedDate = DateTime.Now
                    }).Run(Conn);
            }

            return RedirectToAction("ViewOne", new { id = id});
        }

        public IActionResult Delete(string id)
        {
            var R = RethinkDb.Driver.RethinkDB.R;
            var Conn = R.Connection().Hostname("localhost").Port(28015).Timeout(60).Connect();
            bool Check = R.Db("MyCRM").Table("Businesses").Get(id).IsEmpty().Run(Conn);

            if (Check == true)
            {
                return RedirectToAction("Dashboard");
            }

            var BQuery = R.Db("MyCRM").Table("Businesses").Get(BizId).Delete();
            var BAQuery = R.Db("MyCRM").Table("Activities").GetAll(BizId)[new { index = "BusinessId"}].Delete();
            var PQuery = R.Db("MyCRM").Table("Purchases").GetAll(BizId)[new { index = "BusinessId" }].Delete();
            var TQuery = R.Db("MyCRM").Table("Tasks").GetAll(BizId)[new { index = "BusinessId" }].Delete();
            var NQuery = R.Db("MyCRM").Table("Notes").GetAll(BizId)[new { index = "BusinessId" }].Delete();
            var SQuery = R.Db("MyCRM").Table("Staff").GetAll(BizId)[new { index = "BusinessId" }].Delete();
            var AQuery = R.Db("MyCRM").Table("Addresses").GetAll(BizId)[new { index = "BusinessId" }].Delete();

            return View("Dashboard");
        }
    }
}
