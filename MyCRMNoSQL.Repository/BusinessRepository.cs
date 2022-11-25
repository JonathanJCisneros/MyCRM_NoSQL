using MyCRMNoSQL.Core;
using MyCRMNoSQL.Repository.Interfaces;
using RethinkDb.Driver.Model;
using System;
using System.Linq;


namespace MyCRMNoSQL.Repository
{
    public class BusinessRepository : IBusinessRepository
    {
        public bool CheckById(string id)
        {
            var R = RethinkDb.Driver.RethinkDB.R;
            var Conn = R.Connection().Hostname("localhost").Port(28015).Timeout(60).Connect();
            bool Check = R.Db("MyCRM").Table("Businesses").Get(id).IsEmpty().Run(Conn);

            return Check;
        }

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

            Business Business = new()
            {
                Id = Query.id.ToString(),
                Name = Query.Name.ToString(),
                Website = Query.Website.ToString(),
                Industry = Query.Industry.ToString(),
                StartDate = Convert.ToDateTime(Query.StartDate),
                PocId = Query.PocId.ToString(),
                CreatedDate = Convert.ToDateTime(Query.CreatedDate),
                UpdatedDate = Convert.ToDateTime(Query.UpdatedDate),
            };

            return Business;
        }

        public List<Business> GetAll()
        {
            var R = RethinkDb.Driver.RethinkDB.R;
            var Conn = R.Connection().Hostname("localhost").Port(28015).Timeout(60).Connect();
            var Query = R.Db("MyCRM").Table("Businesses")
                .Merge(b => new
                {
                    PointOfContact = R.Db("MyCRM").Table("Staff").Get(b["PocId"]).Pluck("FirstName", "LastName"),
                    Author = R.Db("MyCRM").Table("Users").Get(b["UserId"]).Pluck("FirstName", "LastName")
                })
            .Run(Conn);

            if (Query.BufferedSize == 0)
            {
                return null;
            }

            List<Business> BusinessList = new();

            foreach (var i in Query)
            {
                Business Business = new()
                {
                    Id = i.id.ToString(),
                    Name = i.Name.ToString(),
                    Website = i.Website.ToString(),
                    Industry = i.Industry.ToString(),
                    UserId = i.UserId.ToString(),
                    CreatedDate = Convert.ToDateTime(i.CreatedDate),
                    UpdatedDate = Convert.ToDateTime(i.UpdatedDate),
                    PocId = i.id.ToString(),
                    PointOfContact = new Staff()
                    {
                        FirstName = i.PointOfContact.FirstName.ToString(),
                        LastName = i.PointOfContact.LastName.ToString()
                    },
                    Author = new User()
                    {
                        FirstName = i.Author.FirstName.ToString(),
                        LastName = i.Author.LastName.ToString()
                    }
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

            if(Query.BufferedSize == 0)
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
                    CreatedDate = Convert.ToDateTime(i.CreatedDate),
                    UpdatedDate = Convert.ToDateTime(i.UpdatedDate),
                    PointOfContact = new()
                    {
                        FirstName = i.PointOfContact.FirstName.ToString(),
                        LastName = i.PointOfContact.LastName.ToString(),
                    },
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

            if (Query.BufferedSize == 0)
            {
                return null;
            }

            List<Business> BusinessList = new();

            foreach (var i in Query)
            {
                Business Business = new()
                {
                    Id = i.id.ToString(),
                    Name = i.Name.ToString(),
                    PocId = i.id.ToString(),
                    CreatedDate = Convert.ToDateTime(i.createdDate),
                    UpdatedDate= Convert.ToDateTime(i.updatedDate),
                    PointOfContact = new()
                    {
                        FirstName = i.PointOfContact.FirstName.ToString(),
                        LastName = i.PointOfContact.LastName.ToString(),
                    }
                };

                BusinessList.Add(Business);
            }

            return BusinessList;
        }

        public string Create(Business business)
        {
            var R = RethinkDb.Driver.RethinkDB.R;
            var Conn = R.Connection().Hostname("localhost").Port(28015).Timeout(60).Connect();

            var Result = R.Db("MyCRM").Table("Businesses")
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

            string Id = Result.generated_keys[0].ToString();

            return Id;
        }

        public string Update(Business business)
        { 
            var R = RethinkDb.Driver.RethinkDB.R;
            var Conn = R.Connection().Hostname("localhost").Port(28015).Timeout(60).Connect();

            if (!string.IsNullOrEmpty(business.Name))
            {
                var Query = R.Db("MyCRM").Table("Businesses").Get(business.Id)
                    .Update(new
                    {
                        Name = business.Name,
                        UpdatedDate = business.UpdatedDate
                    })
                .Run(Conn);
            }

            if (!string.IsNullOrEmpty(business.Website))
            {
                var Query = R.Db("MyCRM").Table("Businesses").Get(business.Id)
                    .Update(new
                    {
                        Website = business.Website,
                        UpdatedDate = business.UpdatedDate
                    })
                .Run(Conn);
            }

            if (!string.IsNullOrEmpty(business.Industry))
            {
                var Query = R.Db("MyCRM").Table("Businesses").Get(business.Id)
                    .Update(new
                    {
                        Industry = business.Industry,
                        UpdatedDate = business.UpdatedDate
                    })
                .Run(Conn);
            }

            if (!string.IsNullOrEmpty(business.PocId))
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
