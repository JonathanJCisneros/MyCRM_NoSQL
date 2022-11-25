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
    public class StaffRepository : IStaffRepository
    {
        public bool CheckById(string id)
        {
            var R = RethinkDb.Driver.RethinkDB.R;
            var Conn = R.Connection().Hostname("localhost").Port(28015).Timeout(60).Connect();
            bool Check = R.Db("MyCRM").Table("Staff").Get(id).IsEmpty().Run(Conn);

            return Check;
        }

        public Staff Get(string id)
        {
            var R = RethinkDb.Driver.RethinkDB.R;
            var Conn = R.Connection().Hostname("localhost").Port(28015).Timeout(60).Connect();

            var Query = R.Db("MyCRM").Table("Staff").Get(id).Run(Conn);

            if(Query == null)
            {
                return null;
            }

            Staff employee = new()
            {
                Id = Query.id.ToString(),
                Position = Query.Position.ToString(),
                FirstName = Query.FirstName.ToString(),
                LastName = Query.LastName.ToString(),
                PhoneNumber = Query.PhoneNumber.ToInt32(),
                Email = Query.Email.ToString(),
                BusinessId = Query.BusinessId.ToString(),
                CreatedDate = Query.CreatedDate.ToString(),
                UpdatedDate = Query.UpdatedDate.ToString()
            };

            return employee;
        }

        public List<Staff> GetAll()
        {
            var R = RethinkDb.Driver.RethinkDB.R;
            var Conn = R.Connection().Hostname("localhost").Port(28015).Timeout(60).Connect();

            var Query = R.Db("MyCRM").Table("Staff").Run(Conn);

            if (Query.BufferedSize == null)
            {
                return null;
            }

            List<Staff> StaffList = new();

            foreach (var item in Query)
            {
                Staff employee = new()
                {
                    Id = item.id.ToString(),
                    Position = item.Position.ToString(),
                    FirstName = item.FirstName.ToString(),
                    LastName = item.LastName.ToString(),
                    PhoneNumber = item.PhoneNumber.ToInt32(),
                    Email = item.Email.ToString(),
                    BusinessId = item.BusinessId.ToString(),
                    CreatedDate = item.CreatedDate.ToString(),
                    UpdatedDate = item.UpdatedDate.ToString()
                };

                StaffList.Add(employee);
            }

            return StaffList;
        }

        public List<Staff> GetAllByBusiness(string id)
        {
            var R = RethinkDb.Driver.RethinkDB.R;
            var Conn = R.Connection().Hostname("localhost").Port(28015).Timeout(60).Connect();

            var Query = R.Db("MyCRM").Table("Staff").GetAll(id)[new { index = "BusinessId" }].Run(Conn);

            if (Query.BufferedSize == null)
            {
                return null;
            }

            List<Staff> StaffList = new();

            foreach (var item in Query)
            {
                Staff employee = new()
                {
                    Id = item.id.ToString(),
                    Position = item.Position.ToString(),
                    FirstName = item.FirstName.ToString(),
                    LastName = item.LastName.ToString(),
                    PhoneNumber = item.PhoneNumber.ToInt32(),
                    Email = item.Email.ToString(),
                    BusinessId = item.BusinessId.ToString(),
                    CreatedDate = item.CreatedDate.ToString(),
                    UpdatedDate = item.UpdatedDate.ToString()
                };

                StaffList.Add(employee);
            }

            return StaffList;
        }

        public List<Staff> GetAllByPosition(string position)
        {
            var R = RethinkDb.Driver.RethinkDB.R;
            var Conn = R.Connection().Hostname("localhost").Port(28015).Timeout(60).Connect();

            var Query = R.Db("MyCRM").Table("Staff").GetAll(position)[new { index = "Position" }].Run(Conn);

            if (Query.BufferedSize == null)
            {
                return null;
            }

            List<Staff> StaffList = new();

            foreach (var item in Query)
            {
                Staff employee = new()
                {
                    Id = item.id.ToString(),
                    Position = item.Position.ToString(),
                    FirstName = item.FirstName.ToString(),
                    LastName = item.LastName.ToString(),
                    PhoneNumber = item.PhoneNumber.ToInt32(),
                    Email = item.Email.ToString(),
                    BusinessId = item.BusinessId.ToString(),
                    CreatedDate = item.CreatedDate.ToString(),
                    UpdatedDate = item.UpdatedDate.ToString()
                };

                StaffList.Add(employee);
            }

            return StaffList;
        }

        public string Create(Staff staff)
        {
            var R = RethinkDb.Driver.RethinkDB.R;
            var Conn = R.Connection().Hostname("localhost").Port(28015).Timeout(60).Connect();

            var Query = R.Db("MyCRM").Table("Staff")
                .Insert(new 
                { 
                    Position = staff.Position,
                    FirstName = staff.FirstName,
                    LastName = staff.LastName,
                    PhoneNumber = staff.PhoneNumber,
                    Email = staff.Email,
                    BusinessId = staff.BusinessId,
                    CreatedDate = staff.CreatedDate,
                    UpdatedDate = staff.UpdatedDate
                })
            .Run(Conn);

            string Id = Query.generated_keys[0].ToString();

            return Id;
        }

        public string Update(Staff staff)
        {
            var R = RethinkDb.Driver.RethinkDB.R;
            var Conn = R.Connection().Hostname("localhost").Port(28015).Timeout(60).Connect();

            var Query = R.Db("MyCRM").Table("Staff").Get(staff.Id)
                .Update(new
                {
                    Position = staff.Position,
                    FirstName = staff.FirstName,
                    LastName = staff.LastName,
                    PhoneNumber = staff.PhoneNumber,
                    Email = staff.Email,
                    BusinessId = staff.BusinessId,
                    UpdatedDate = staff.UpdatedDate
                })
            .Run(Conn);

            return staff.Id;
        }

        public bool Delete(string id)
        {
            var R = RethinkDb.Driver.RethinkDB.R;
            var Conn = R.Connection().Hostname("localhost").Port(28015).Timeout(60).Connect();

            var Result = R.Db("MyCRM").Table("Staff").Get(id).Delete().Run(Conn);

            if (Result.deleted == 0)
            {
                return false;
            }

            return true;
        }

        public bool DeleteAllByBusiness(string id)
        {
            var R = RethinkDb.Driver.RethinkDB.R;
            var Conn = R.Connection().Hostname("localhost").Port(28015).Timeout(60).Connect();

            var Result = R.Db("MyCRM").Table("Staff").GetAll(id)[new { index = "BusinessId" }].Delete().Run(Conn);

            if (Result.deleted == 0)
            {
                return false;
            }

            return true;
        }
    }
}
