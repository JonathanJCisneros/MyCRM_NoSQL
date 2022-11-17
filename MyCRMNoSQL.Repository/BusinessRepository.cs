using MyCRMNoSQL.Core;
using MyCRMNoSQL.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MyCRMNoSQL.Repository
{
    public class BusinessRepository : IBusinessRepository
    {
        public bool CheckByName(string name)
        {
            var R = RethinkDb.Driver.RethinkDB.R;
            var Conn = R.Connection().Hostname("localhost").Port(28015).Timeout(60).Connect();
            bool Check = R.Db("MyCRM").Table("Businesses").GetAll(name)[new { index = "Name" }].IsEmpty().Run(Conn);

            return Check;
        }

        public Business Get(string id)
        {
            var R = RethinkDb.Driver.RethinkDB.R;
            var Conn = R.Connection().Hostname("localhost").Port(28015).Timeout(60).Connect();
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
                            User = R.Db("MyCRM").Table("Users").Get(P["UserId"]).Pluck("id", "FirstName", "LastName"),
                            Product = R.Db("MyCRM").Table("Products").Get(P["ProductId"]).Pluck("Name", "Description", "Price"),
                            Address = R.Db("MyCRM").Table("Addresses").Get(P["AddressId"]).Pluck("Street", "AptSuite", "City", "State", "ZipCode")
                        }).CoerceTo("array"),
                    ActivityList = R.Db("MyCRM").Table("Activities").GetAll(B["id"])[new { index = "BusinessId" }].Pluck("id", "Type", "Description", "StaffId", "UserId", "CreatedDate")
                        .Merge(A => new
                        {
                            User = R.Db("MyCRM").Table("Users").Get(A["UserId"]).Pluck("FirstName", "LastName"),
                            Staff = R.Db("MyCRM").Table("Staff").Get(A["StaffId"]).Pluck("FirstName", "LastName", "Position")
                        }).OrderBy(R.Desc("CreatedDate")).CoerceTo("array")
                })
            .Run(Conn);

            List<ClientActivity> ActivityList = new();

            foreach (var A in Query.ActivityList)
            {
                User AUser = new()
                {
                    FirstName = A.User.FirstName.ToString(),
                    LastName = A.User.LastName.ToString()
                };

                Staff AStaff = new()
                {
                    FirstName = A.Staff.FirstName.ToString(),
                    LastName = A.Staff.LastName.ToString(),
                    Position = A.Staff.Position.ToString()
                };

                ClientActivity Activity = new()
                {
                    Id = A.id.ToString(),
                    Type = A.Type.ToString(),
                    Note = A.Description.ToString(),
                    StaffId = A.StaffId.ToString(),
                    UserId = A.UserId.ToString(),
                    CreatedDate = A.CreatedDate.ToDateTime(),
                    UserAssociated = AUser,
                    EmployeeAssociated = AStaff
                };

                ActivityList.Add(Activity);
            }

            List<Purchase> PurchaseList = new();

            foreach (var p in Query.PurchaseList)
            {
                Product Product = new()
                {
                    Name = p.Product.Name.ToString(),
                    Description = p.Product.Description.ToString(),
                    Price = p.Product.Price.ToInt32(),
                };

                Address Address = new()
                {
                    Street = p.Address.Street.ToString(),
                    AptSuite = p.Address.AptSuite.ToString(),
                    City = p.Address.City.ToString(),
                    State = p.Address.State.ToString(),
                    ZipCode = p.Address.ZipCode.ToInt32(),
                };

                User User = new()
                {
                    Id = p.User.id.ToString(),
                    FirstName = p.User.FirstName.ToString(),
                    LastName = p.User.LastName.ToString()
                };

                Purchase Purchase = new()
                {
                    Id = p.id.ToString(),
                    ProductId = p.productId.ToString(),
                    AddressId = p.addressId.ToString(),
                    UserId = p.userId.ToString(),
                    CreatedDate = p.createdDate.ToDateTime(),
                    ProductAssociated = Product,
                    BusinessLocation = Address,
                    SalesRep = User
                };

                PurchaseList.Add(Purchase);
            }

            List<UpcomingTask> TaskList = new();

            foreach (var t in Query.TaskList)
            {
                User User = new()
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

                UpcomingTask Task = new()
                {
                    Id = t.TaskId.ToString(),
                    UserId = t.UserId.ToString(),
                    StaffId = t.StaffId.ToString(),
                    Type = t.Type.ToString(),
                    Details = t.Details.ToString(),
                    DueDate = t.DueDate.ToDateTime(),
                    Status = t.Status.ToString(),
                    CreatedDate = t.CreatedDate.ToDateTime(),
                    UpdatedDate = t.UpdatedDate.ToDateTime(),
                    UserAssociated = User,
                    EmployeeAssociated = Staff
                };

                TaskList.Add(Task);
            }

            List<Note> NoteList = new();

            foreach (var t in Query.NoteList)
            {
                User User = new()
                {
                    FirstName = t.User.FirstName.ToString(),
                    LastName = t.User.LastName.ToString(),
                };

                Note Note = new()
                {
                    Id = t.id.ToString(),
                    Details = t.Details.ToString(),
                    CreatedDate = t.CreatedDate.ToDateTime(),
                    UserId = t.UserId.ToString(),
                    Author = User
                };

                NoteList.Add(Note);
            }

            List<Staff> StaffList = new();

            foreach (var e in Query.StaffList)
            {
                Staff Employee = new()
                {
                    Id = e.id.ToString(),
                    FirstName = e.FirstName.ToString(),
                    LastName = e.LastName.ToString(),
                    Position = e.Position.ToString(),
                    Email = e.Email.ToString(),
                    PhoneNumber = e.PhoneNumber.ToInt32(),
                };

                StaffList.Add(Employee);
            }

            List<Address> AddressList = new();

            foreach (var e in Query.AddressList)
            {
                Address Address = new()
                {
                    Id = e.id.ToString(),
                    Street = e.Street.ToString(),
                    AptSuite = e.AptSuite.ToString(),
                    City = e.City.ToString(),
                    State = e.State.ToString(),
                    ZipCode = e.ZipCode.ToString(),
                };

                AddressList.Add(Address);
            }

            Staff PointOfContact = new()
            {
                FirstName = Query.PointOfContact.FirstName.ToString(),
                LastName = Query.PointOfContact.LastName.ToString(),
                Position = Query.PointOfContact.Position.ToString(),
                Email = Query.PointOfContact.Email.ToString(),
                PhoneNumber = Query.PointOfContact.PhoneNumber.ToInt32()
            };

            Business Business = new Business()
            {
                Id = Query.id.ToString(),
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

            return Business;
        }

        public List<Business> GetAll()
        {
            var R = RethinkDb.Driver.RethinkDB.R;
            var Conn = R.Connection().Hostname("localhost").Port(28015).Timeout(60).Connect();
            var Query = R.Db("MyCRM").Table("Businesses").Pluck("id", "Name", "PocId")
                .Merge(b => new
                {
                    PointOfContact = R.Db("MyCRM").Table("Staff").Get(b["PocId"]).Pluck("FirstName", "LastName"),
                    LatestActivity = R.Db("MyCRM").Table("Activities").GetAll(b["id"])[new { index = "BusinessId" }].Pluck("Type", "CreatedDate").OrderBy(R.Desc("CreatedDate")).Limit(1).CoerceTo("array")
                })
            .Run(Conn);

            if(Query.Count == 0)
            {
                return null;
            }

            List<Business> BusinessList = new();

            foreach (var i in Query)
            {
                ClientActivity Activity = new();

                if (i.LatestActivity.Count > 0)
                {
                    Activity.Type = i.LatestActivity[0].Type.ToString();
                    Activity.CreatedDate = i.LatestActivity[0].CreatedDate;
                }


                Staff POC = new()
                {
                    FirstName = i.PointOfContact.FirstName.ToString(),
                    LastName = i.PointOfContact.LastName.ToString(),
                };

                Business Business = new()
                {
                    Id = i.id.ToString(),
                    Name = i.Name.ToString(),
                    PocId = i.id.ToString(),
                    PointOfContact = POC,
                    LatestActivity = Activity
                };

                BusinessList.Add(Business);
            }


            return BusinessList;
        }

        public List<Business> GetAllByIndustry(string industry)
        {
            var R = RethinkDb.Driver.RethinkDB.R;
            var Conn = R.Connection().Hostname("localhost").Port(28015).Timeout(60).Connect();
            var Query = R.Db("MyCRM").Table("Businesses").GetAll(industry)[new { index = "Industry"}].Pluck("id", "Name", "PocId")
                .Merge(b => new
                {
                    PointOfContact = R.Db("MyCRM").Table("Staff").Get(b["PocId"]).Pluck("FirstName", "LastName")
                })
            .Run(Conn);

            if (Query.Count == 0)
            {
                return null;
            }

            List<Business> BusinessList = new();

            foreach (var i in Query)
            {
                Staff POC = new()
                {
                    FirstName = i.PointOfContact.FirstName.ToString(),
                    LastName = i.PointOfContact.LastName.ToString(),
                };

                Business Business = new()
                {
                    Id = i.id.ToString(),
                    Name = i.Name.ToString(),
                    PocId = i.id.ToString(),
                    PointOfContact = POC
                };

                BusinessList.Add(Business);
            }

            return BusinessList;
        }

        public string CreateClient(Business business, Address address, Staff staff)
        {
            var R = RethinkDb.Driver.RethinkDB.R;
            var Conn = R.Connection().Hostname("localhost").Port(28015).Timeout(60).Connect();
            var BResult = R.Db("MyCRM").Table("Businesses")
                .Insert(new
                {
                    Name = business.Name,
                    Industry = business.Industry,
                    Website = business.Website,
                    CreatedDate = business.CreatedDate,
                    UpdatedDate = business.UpdatedDate,
                    UserId = business.UserId
                })
            .Run(Conn);

            string bId = BResult.generated_keys[0].tostring();

            var AResult = R.Db("MyCRM").Table("Addresses")
                .Insert(new
                {
                    Street = address.Street,
                    AptSuite = address.AptSuite,
                    City = address.City,
                    State = address.State,
                    ZipCode = address.ZipCode,
                    CreatedDate = address.CreatedDate,
                    UpdatedDate = address.UpdatedDate,
                    BusinessId = bId
                })
            .Run(Conn);

            var SResult = R.Db("MyCRM").Table("Staff")
                .Insert(new
                {
                    FirstName = staff.FirstName,
                    LastName = staff.LastName,
                    Position = staff.Position,
                    Email = staff.Email,
                    PhoneNumber = staff.PhoneNumber,
                    CreatedDate = staff.CreatedDate,
                    UpdatedDate = staff.UpdatedDate,
                    BusinessId = bId
                })
            .Run(Conn);

            string sId = SResult.generated_keys[0].ToString();

            var UBiz = R.Db("MyCRM").Table("Businesses").Get(bId)
                .Update(new
                {
                    PocId = sId
                })
            .Run(Conn);

            return bId;
        }

        public string Create(Business b)
        {
            return "Probably never gonna use this";
        }

        public string Update(Business business)
        { 
            var R = RethinkDb.Driver.RethinkDB.R;
            var Conn = R.Connection().Hostname("localhost").Port(28015).Timeout(60).Connect();
            
            if(business.Name != null)
            {
                var Query = R.Db("MyCRM").Table("Businesses").Get(business.Id)
                    .Update(new
                    {
                        Name = business.Name,
                        UpdatedDate = business.UpdatedDate
                    })
                .Run(Conn);
            }

            if (business.Website != null)
            {
                var Query = R.Db("MyCRM").Table("Businesses").Get(business.Id)
                    .Update(new
                    {
                        Website = business.Website,
                        UpdatedDate = business.UpdatedDate
                    })
                .Run(Conn);
            }

            if (business.Industry != null)
            {
                var Query = R.Db("MyCRM").Table("Businesses").Get(business.Id)
                    .Update(new
                    {
                        Industry = business.Industry,
                        UpdatedDate = business.UpdatedDate
                    })
                .Run(Conn);
            }

            if (business.PocId != null)
            {
                var Query = R.Db("MyCRM").Table("Businesses").Get(business.Id)
                    .Update(new
                    {
                        PocId = business.PocId,
                        UpdatedDate = business.UpdatedDate
                    })
                .Run(Conn);
            }

            return business.Id;
        }

        public bool Delete(string id)
        {
            var R = RethinkDb.Driver.RethinkDB.R;
            var Conn = R.Connection().Hostname("localhost").Port(28015).Timeout(60).Connect();
            var Query = R.Db("MyCRM").Table("Businesses").Get(id).Delete().Run(Conn);

            if(Query.deleted == 0)
            {
                return false;
            }

            return true;
        }
    }
}
