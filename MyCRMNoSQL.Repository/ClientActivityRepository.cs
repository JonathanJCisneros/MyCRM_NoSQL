using MyCRMNoSQL.Core;
using MyCRMNoSQL.Repository.Interfaces;
using RethinkDb.Driver.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCRMNoSQL.Repository
{
    public class ClientActivityRepository : IClientActivityRepository
    {
        public bool CheckById(string id)
        {
            var R = RethinkDb.Driver.RethinkDB.R;
            var Conn = R.Connection().Hostname("localhost").Port(28015).Timeout(60).Connect();
            bool Check = R.Db("MyCRM").Table("Activities").Get(id).IsEmpty().Run(Conn);

            return Check;
        }

        public ClientActivity Get(string id)
        {
            var R = RethinkDb.Driver.RethinkDB.R;
            var Conn = R.Connection().Hostname("localhost").Port(28015).Timeout(60).Connect();

            var Query = R.Db("MyCRM").Table("Activities").Get(id).Run(Conn);

            if(Query == null)
            {
                return null;
            }

            ClientActivity activity = new()
            {
                Id = Query.id.ToString(),
                UserId = Query.UserId.ToString(),
                BusinessId = Query.BusinessId.ToString(),
                StaffId = Query.StaffId.ToString(),
                Type = Query.Type.ToString(),
                Note = Query.Note.ToString(),
                CreatedDate = Query.CreatedDate.ToDateTime(),
                UpdatedDate = Query.UpdatedDate.ToDateTime()
            };

            return activity;
        }

        public List<ClientActivity> GetAll()
        {
            var R = RethinkDb.Driver.RethinkDB.R;
            var Conn = R.Connection().Hostname("localhost").Port(28015).Timeout(60).Connect();

            var Query = R.Db("MyCRM").Table("Activities")
                .Merge(a => new
                { 
                    UserAssociated = R.Db("MyCRM").Table("Users").Get(a["UserId"]).Pluck("FirstName", "LastName"),
                    BusinessAssociated = R.Db("MyCRM").Table("Businesses").Get(a["BusinessId"]).Pluck("Name", "Industry"),
                    EmployeeAssociated = R.Db("MyCRM").Table("Staff").Get(a["StaffId"]).Pluck("FirstName", "LastName", "Position")
                })
            .Run(Conn);

            if (Query.BufferedSize == 0)
            {
                return null;
            }

            List<ClientActivity> ActivityList = new();

            foreach (var t in Query)
            {
                User user = new()
                {
                    FirstName = t.UserAssociated.FirstName.ToString(),
                    LastName = t.UserAssociated.LastName.ToString(),
                };

                Business business = new()
                {
                    Name = t.BusinessAssociated.Name.ToString(),
                    Industry = t.BusinessAssociated.Industry.ToString()
                };

                Staff staff = new()
                {
                    FirstName = t.EmployeeAssociated.FirstName.ToString(),
                    LastName= t.EmployeeAssociated.LastName.ToString(),
                    Position = t.EmployeeAssociated.Position.ToString(),
                };

                ClientActivity activity = new()
                {
                    Id = t.Id.ToString(),
                    UserId = t.UserId.ToString(),
                    BusinessId = t.BusinessId.ToString(),
                    StaffId = t.StaffId.ToString(),
                    Type = t.Type.ToString(),
                    Note = t.Note.ToString(),
                    CreatedDate = t.CreatedDate.ToString(),
                    UpdatedDate = t.UpdatedDate.ToString(),
                    UserAssociated = user,
                    BusinessAssociated = business,
                    EmployeeAssociated = staff
                };

                ActivityList.Add(activity);
            }

            return ActivityList;
        }

        public List<ClientActivity> GetAllByBusiness(string id)
        {
            var R = RethinkDb.Driver.RethinkDB.R;
            var Conn = R.Connection().Hostname("localhost").Port(28015).Timeout(60).Connect();

            var Query = R.Db("MyCRM").Table("Activities").GetAll(id)[new { index = "BusinessId" }]
                .Merge(a => new
                {
                    UserAssociated = R.Db("MyCRM").Table("Users").Get(a["UserId"]).Pluck("FirstName", "LastName"),
                    BusinessAssociated = R.Db("MyCRM").Table("Businesses").Get(a["BusinessId"]).Pluck("Name", "Industry"),
                    EmployeeAssociated = R.Db("MyCRM").Table("Staff").Get(a["StaffId"]).Pluck("FirstName", "LastName", "Position")
                })
            .Run(Conn);

            if (Query.BufferedSize == 0)
            {
                return null;
            }

            List<ClientActivity> ActivityList = new();

            foreach (var t in Query)
            {
                User user = new()
                {
                    FirstName = t.UserAssociated.FirstName.ToString(),
                    LastName = t.UserAssociated.LastName.ToString(),
                };

                Business business = new()
                {
                    Name = t.BusinessAssociated.Name.ToString(),
                    Industry = t.BusinessAssociated.Industry.ToString()
                };

                Staff staff = new()
                {
                    FirstName = t.EmployeeAssociated.FirstName.ToString(),
                    LastName = t.EmployeeAssociated.LastName.ToString(),
                    Position = t.EmployeeAssociated.Position.ToString(),
                };

                ClientActivity activity = new()
                {
                    Id = t.Id.ToString(),
                    UserId = t.UserId.ToString(),
                    BusinessId = t.BusinessId.ToString(),
                    StaffId = t.StaffId.ToString(),
                    Type = t.Type.ToString(),
                    Note = t.Note.ToString(),
                    CreatedDate = t.CreatedDate.ToString(),
                    UpdatedDate = t.UpdatedDate.ToString(),
                    UserAssociated = user,
                    BusinessAssociated = business,
                    EmployeeAssociated = staff
                };

                ActivityList.Add(activity);
            }

            return ActivityList;
        }

        public List<ClientActivity> GetAllByUser(string id)
        {
            var R = RethinkDb.Driver.RethinkDB.R;
            var Conn = R.Connection().Hostname("localhost").Port(28015).Timeout(60).Connect();

            var Query = R.Db("MyCRM").Table("Activities").GetAll(id)[new { index = "UserId" }]
                .Merge(a => new
                {
                    UserAssociated = R.Db("MyCRM").Table("Users").Get(a["UserId"]).Pluck("FirstName", "LastName"),
                    BusinessAssociated = R.Db("MyCRM").Table("Businesses").Get(a["BusinessId"]).Pluck("Name", "Industry"),
                    EmployeeAssociated = R.Db("MyCRM").Table("Staff").Get(a["StaffId"]).Pluck("FirstName", "LastName", "Position")
                })
            .Run(Conn);

            if (Query.BufferedSize == 0)
            {
                return null;
            }

            List<ClientActivity> ActivityList = new();

            foreach (var t in Query)
            {
                User user = new()
                {
                    FirstName = t.UserAssociated.FirstName.ToString(),
                    LastName = t.UserAssociated.LastName.ToString(),
                };

                Business business = new()
                {
                    Name = t.BusinessAssociated.Name.ToString(),
                    Industry = t.BusinessAssociated.Industry.ToString()
                };

                Staff staff = new()
                {
                    FirstName = t.EmployeeAssociated.FirstName.ToString(),
                    LastName = t.EmployeeAssociated.LastName.ToString(),
                    Position = t.EmployeeAssociated.Position.ToString(),
                };

                ClientActivity activity = new()
                {
                    Id = t.Id.ToString(),
                    UserId = t.UserId.ToString(),
                    BusinessId = t.BusinessId.ToString(),
                    StaffId = t.StaffId.ToString(),
                    Type = t.Type.ToString(),
                    Note = t.Note.ToString(),
                    CreatedDate = t.CreatedDate.ToString(),
                    UpdatedDate = t.UpdatedDate.ToString(),
                    UserAssociated = user,
                    BusinessAssociated = business,
                    EmployeeAssociated = staff
                };

                ActivityList.Add(activity);
            }

            return ActivityList;
        }

        public List<ClientActivity> GetAllByType(string type)
        {
            var R = RethinkDb.Driver.RethinkDB.R;
            var Conn = R.Connection().Hostname("localhost").Port(28015).Timeout(60).Connect();

            var Query = R.Db("MyCRM").Table("Activities").GetAll(type)[new { index = "Type" }]
                .Merge(a => new
                {
                    UserAssociated = R.Db("MyCRM").Table("Users").Get(a["UserId"]).Pluck("FirstName", "LastName"),
                    BusinessAssociated = R.Db("MyCRM").Table("Businesses").Get(a["BusinessId"]).Pluck("Name", "Industry"),
                    EmployeeAssociated = R.Db("MyCRM").Table("Staff").Get(a["StaffId"]).Pluck("FirstName", "LastName", "Position")
                })
            .Run(Conn);

            if (Query.BufferedSize == 0)
            {
                return null;
            }

            List<ClientActivity> ActivityList = new();

            foreach (var t in Query)
            {
                User user = new()
                {
                    FirstName = t.UserAssociated.FirstName.ToString(),
                    LastName = t.UserAssociated.LastName.ToString(),
                };

                Business business = new()
                {
                    Name = t.BusinessAssociated.Name.ToString(),
                    Industry = t.BusinessAssociated.Industry.ToString()
                };

                Staff staff = new()
                {
                    FirstName = t.EmployeeAssociated.FirstName.ToString(),
                    LastName = t.EmployeeAssociated.LastName.ToString(),
                    Position = t.EmployeeAssociated.Position.ToString(),
                };

                ClientActivity activity = new()
                {
                    Id = t.Id.ToString(),
                    UserId = t.UserId.ToString(),
                    BusinessId = t.BusinessId.ToString(),
                    StaffId = t.StaffId.ToString(),
                    Type = t.Type.ToString(),
                    Note = t.Note.ToString(),
                    CreatedDate = t.CreatedDate.ToString(),
                    UpdatedDate = t.UpdatedDate.ToString(),
                    UserAssociated = user,
                    BusinessAssociated = business,
                    EmployeeAssociated = staff
                };

                ActivityList.Add(activity);
            }

            return ActivityList;
        }

        public string Create(ClientActivity activity)
        {
            var R = RethinkDb.Driver.RethinkDB.R;
            var Conn = R.Connection().Hostname("localhost").Port(28015).Timeout(60).Connect();

            var Result = R.Db("MyCRM").Table("Activities")
                .Insert(new
                {
                    UserId = activity.UserId,
                    BusinessId = activity.BusinessId,
                    StaffId = activity.StaffId,
                    Type = activity.Type,
                    Note = activity.Note,
                    CreatedDate = activity.CreatedDate,
                    UpdatedDate = activity.UpdatedDate,
                })
            .Run(Conn);

            string Id = Result.generated_keys[0].ToString();

            return Id;
        }

        public string Update(ClientActivity activity)
        {
            var R = RethinkDb.Driver.RethinkDB.R;
            var Conn = R.Connection().Hostname("localhost").Port(28015).Timeout(60).Connect();

            var Result = R.Db("MyCRM").Table("Activities").Get(activity.Id)
                .Update(new 
                {
                    UserId = activity.UserId,
                    BusinessId = activity.BusinessId,
                    StaffId = activity.StaffId,
                    Type = activity.Type,
                    Note = activity.Note,
                    UpdatedDate = activity.UpdatedDate
                })
            .Run(Conn);

            return activity.Id;
        }

        public bool Delete(string id)
        {
            var R = RethinkDb.Driver.RethinkDB.R;
            var Conn = R.Connection().Hostname("localhost").Port(28015).Timeout(60).Connect();

            var Result = R.Db("MyCRM").Table("Activities").Get(id).Delete().Run(Conn);

            if (Result == null)
            {
                return false;
            }

            return true;
        }

        public bool DeleteAllByBusiness(string id)
        {
            var R = RethinkDb.Driver.RethinkDB.R;
            var Conn = R.Connection().Hostname("localhost").Port(28015).Timeout(60).Connect();

            var Result = R.Db("MyCRM").Table("Activities").GetAll(id)[new { index = "BusinessId" }].Delete().Run(Conn);

            if (Result.deleted == 0)
            {
                return false;
            }

            return true;
        }
    }
}
