using MyCRMNoSQL.Core;
using MyCRMNoSQL.Repository.Interfaces;
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
            var Query = R.Db("MyCRM").Table("Businesses").Get(id).Run(Conn);

            if(Query == null)
            {
                return null;
            }

            Business Business = new Business()
            {
                Id = Query.id.ToString(),
                Name = Query.Name.ToString(),
                Website = Query.Website.ToString(),
                Industry = Query.Industry.ToString(),
                StartDate = Query.StartDate.ToDateTime(),
                PocId = Query.PocId.ToString(),
                CreatedDate = Query.CreatedDate.ToDateTime(),
                UpdatedDate = Query.UpdatedDate.ToDateTime(),
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

        public List<Business> GetAllWithLatestActivity()
        {
            var R = RethinkDb.Driver.RethinkDB.R;
            var Conn = R.Connection().Hostname("localhost").Port(28015).Timeout(60).Connect();
            var Query = R.Db("MyCRM").Table("Businesses")
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
            var Query = R.Db("MyCRM").Table("Businesses").GetAll(industry)[new { index = "Industry"}]
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
                    CreatedDate = i.createdDate.ToDateTime(),
                    UpdatedDate= i.updatedDate.ToDateTime(),
                    PointOfContact = POC
                };

                BusinessList.Add(Business);
            }

            return BusinessList;
        }

        public string Create(Business business)
        {
            var R = RethinkDb.Driver.RethinkDB.R;
            var Conn = R.Connection().Hostname("localhost").Port(28015).Timeout(60).Connect();

            var Query = R.Db("MyCRM").Table("Businesses")
                .Insert(new 
                {
                    Name = business.Name,
                    Website = business.Website,
                    StartDate = business.StartDate,
                    Industry = business.Industry,
                    UserId = business.UserId,
                    CreatedDate = business.CreatedDate,
                    UpdatedDate = business.UpdatedDate
                })
            .Run(Conn);

            string Id = 

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
