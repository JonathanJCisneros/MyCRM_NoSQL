using MyCRMNoSQL.Core;
using MyCRMNoSQL.Core.Interfaces;
using RethinkDb.Driver.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCRMNoSQL.Repository
{
    public class PurchaseRepository : IPurchaseRepository
    {
        public Purchase Get(string id)
        {
            var R = RethinkDb.Driver.RethinkDB.R;
            var Conn = R.Connection().Hostname("localhost").Port(28015).Timeout(60).Connect();
            var Query = R.Db("MyCRM").Table("Purchases").Get(id)
                .Merge(P => new 
                { 
                    BusinessAssociated = R.Db("MyCRM").Table("Businesses").Get(P["BusinessId"]).Pluck("Name", "Industry"),
                    BusinessLocation = R.Db("MyCRM").Table("Addresses").Get(P["AddressId"]).Pluck("City", "State", "ZipCode"),
                    ProductAssociated = R.Db("MyCRM").Table("Products").Get(P["ProductId"]).Pluck("Name", "Price"),
                    SalesRep = R.Db("MyCRM").Table("Users").Get(P["UserId"]).Pluck("FirstName", "LastName")
                })
            .Run(Conn);

            Business business = new()
            {
                Name = Query.BusinessAssociated.Name.ToString(),
                Industry = Query.BusinessAssociated.Industry.ToString()
            };

            Address address = new()
            {
                City = Query.BusinessLocation.City.ToString(),
                State = Query.BusinessLocation.State.ToString(),
                ZipCode = Query.BusinessLocation.ZipCode.ToInt32()
            };

            Product product = new()
            {
                Name = Query.ProductAssociated.Name.ToString(),
                Price = Query.ProductAssociated.Price.ToInt32()
            };

            User user = new()
            {
                FirstName = Query.SalesRep.FirstName.ToString(),
                LastName = Query.SalesRep.LastName.ToString()
            };

            Purchase purchase = new()
            {
                Id = Query.id.ToString(),
                BusinessId = Query.BusinessId.ToString(),
                AddressId = Query.AddressId.ToString(),
                UserId = Query.UserId.ToString(),
                CreatedDate = Query.CreatedDate.ToDateTime(),
                UpdatedDate = Query.UpdatedDate.ToDateTime(),
                BusinessAssociated = business,
                ProductAssociated = product,
                BusinessLocation = address,
                SalesRep = user
            };

            return purchase;
        }

        public List<Purchase> GetAll()
        {
            var R = RethinkDb.Driver.RethinkDB.R;
            var Conn = R.Connection().Hostname("localhost").Port(28015).Timeout(60).Connect();
            var Query = R.Db("MyCRM").Table("Purchases")
                .Merge(P => new
                {
                    BusinessAssociated = R.Db("MyCRM").Table("Businesses").Get(P["BusinessId"]).Pluck("Name", "Industry"),
                    BusinessLocation = R.Db("MyCRM").Table("Addresses").Get(P["AddressId"]).Pluck("City", "State", "ZipCode"),
                    ProductAssociated = R.Db("MyCRM").Table("Products").Get(P["ProductId"]).Pluck("Name", "Price"),
                    SalesRep = R.Db("MyCRM").Table("Users").Get(P["UserId"]).Pluck("FirstName", "LastName")
                })
            .Run(Conn);

            List<Purchase> purchaseList = new();

            foreach (var item in Query)
            {
                Business business = new()
                {
                    Name = item.BusinessAssociated.Name.ToString(),
                    Industry = item.BusinessAssociated.Industry.ToString()
                };

                Address address = new()
                {
                    City = item.BusinessLocation.City.ToString(),
                    State = item.BusinessLocation.State.ToString(),
                    ZipCode = item.BusinessLocation.ZipCode.ToInt32()
                };

                Product product = new()
                {
                    Name = item.ProductAssociated.Name.ToString(),
                    Price = item.ProductAssociated.Price.ToInt32()
                };

                User user = new()
                {
                    FirstName = item.SalesRep.FirstName.ToString(),
                    LastName = item.SalesRep.LastName.ToString()
                };

                Purchase purchase = new()
                {
                    Id = item.id.ToString(),
                    BusinessId = item.BusinessId.ToString(),
                    AddressId = item.AddressId.ToString(),
                    UserId = item.UserId.ToString(),
                    CreatedDate = item.CreatedDate.ToDateTime(),
                    UpdatedDate = item.UpdatedDate.ToDateTime(),
                    BusinessAssociated = business,
                    ProductAssociated = product,
                    BusinessLocation = address,
                    SalesRep = user
                };

                purchaseList.Add(purchase);
            }


            return purchaseList;
        }

        public List<Purchase> GetAllByBusiness(string id)
        {
            var R = RethinkDb.Driver.RethinkDB.R;
            var Conn = R.Connection().Hostname("localhost").Port(28015).Timeout(60).Connect();
            var Query = R.Db("MyCRM").Table("Purchases").GetAll(id)[new { index = "BusinessId"}]
                .Merge(P => new
                {
                    BusinessAssociated = R.Db("MyCRM").Table("Businesses").Get(P["BusinessId"]).Pluck("Name", "Industry"),
                    BusinessLocation = R.Db("MyCRM").Table("Addresses").Get(P["AddressId"]).Pluck("City", "State", "ZipCode"),
                    ProductAssociated = R.Db("MyCRM").Table("Products").Get(P["ProductId"]).Pluck("Name", "Price"),
                    SalesRep = R.Db("MyCRM").Table("Users").Get(P["UserId"]).Pluck("FirstName", "LastName")
                })
            .Run(Conn);

            List<Purchase> purchaseList = new();

            foreach (var item in Query)
            {
                Business business = new()
                {
                    Name = item.BusinessAssociated.Name.ToString(),
                    Industry = item.BusinessAssociated.Industry.ToString()
                };

                Address address = new()
                {
                    City = item.BusinessLocation.City.ToString(),
                    State = item.BusinessLocation.State.ToString(),
                    ZipCode = item.BusinessLocation.ZipCode.ToInt32()
                };

                Product product = new()
                {
                    Name = item.ProductAssociated.Name.ToString(),
                    Price = item.ProductAssociated.Price.ToInt32()
                };

                User user = new()
                {
                    FirstName = item.SalesRep.FirstName.ToString(),
                    LastName = item.SalesRep.LastName.ToString()
                };

                Purchase purchase = new()
                {
                    Id = item.id.ToString(),
                    BusinessId = item.BusinessId.ToString(),
                    AddressId = item.AddressId.ToString(),
                    UserId = item.UserId.ToString(),
                    CreatedDate = item.CreatedDate.ToDateTime(),
                    UpdatedDate = item.UpdatedDate.ToDateTime(),
                    BusinessAssociated = business,
                    ProductAssociated = product,
                    BusinessLocation = address,
                    SalesRep = user
                };

                purchaseList.Add(purchase);
            }


            return purchaseList;
        }

        public List<Purchase> GetAllByIndustry(string industry)
        {
            return null;
        }

        public List<Purchase> GetAllByCity(string city)
        {
            return null;
        }

        public List<Purchase> GetAllByZipCode(int zipCode)
        {
            return null;
        }

        public List<Purchase> GetAllByUser(string id)
        {
            var R = RethinkDb.Driver.RethinkDB.R;
            var Conn = R.Connection().Hostname("localhost").Port(28015).Timeout(60).Connect();
            var Query = R.Db("MyCRM").Table("Purchases").GetAll(id)[new { index = "UserId"}]
                .Merge(P => new
                {
                    BusinessAssociated = R.Db("MyCRM").Table("Businesses").Get(P["BusinessId"]).Pluck("Name", "Industry"),
                    BusinessLocation = R.Db("MyCRM").Table("Addresses").Get(P["AddressId"]).Pluck("City", "State", "ZipCode"),
                    ProductAssociated = R.Db("MyCRM").Table("Products").Get(P["ProductId"]).Pluck("Name", "Price"),
                    SalesRep = R.Db("MyCRM").Table("Users").Get(P["UserId"]).Pluck("FirstName", "LastName")
                })
            .Run(Conn);

            List<Purchase> purchaseList = new();

            foreach (var item in Query)
            {
                Business business = new()
                {
                    Name = item.BusinessAssociated.Name.ToString(),
                    Industry = item.BusinessAssociated.Industry.ToString()
                };

                Address address = new()
                {
                    City = item.BusinessLocation.City.ToString(),
                    State = item.BusinessLocation.State.ToString(),
                    ZipCode = item.BusinessLocation.ZipCode.ToInt32()
                };

                Product product = new()
                {
                    Name = item.ProductAssociated.Name.ToString(),
                    Price = item.ProductAssociated.Price.ToInt32()
                };

                User user = new()
                {
                    FirstName = item.SalesRep.FirstName.ToString(),
                    LastName = item.SalesRep.LastName.ToString()
                };

                Purchase purchase = new()
                {
                    Id = item.id.ToString(),
                    BusinessId = item.BusinessId.ToString(),
                    AddressId = item.AddressId.ToString(),
                    UserId = item.UserId.ToString(),
                    CreatedDate = item.CreatedDate.ToDateTime(),
                    UpdatedDate = item.UpdatedDate.ToDateTime(),
                    BusinessAssociated = business,
                    ProductAssociated = product,
                    BusinessLocation = address,
                    SalesRep = user
                };

                purchaseList.Add(purchase);
            }


            return purchaseList;
        }

        public string Create(Purchase purchase)
        {
            var R = RethinkDb.Driver.RethinkDB.R;
            var Conn = R.Connection().Hostname("localhost").Port(28015).Timeout(60).Connect();

            var Result = R.Db("MyCRM").Table("Purchases")
                .Insert(new
                {
                    BusinessId = purchase.BusinessId,
                    ProductId = purchase.ProductId,
                    AddressId = purchase.AddressId,
                    UserId = purchase.UserId,
                    CreatedDate = purchase.CreatedDate,
                    UpdatedDate = purchase.UpdatedDate
                })
            .Run(Conn);

            string Id = Result.generated_keys[0].ToString();

            return Id;
        }

        public string Update(Purchase purchase)
        {
            var R = RethinkDb.Driver.RethinkDB.R;
            var Conn = R.Connection().Hostname("localhost").Port(28015).Timeout(60).Connect();

            var Result = R.Db("MyCRM").Table("Purchases")
                .Update(new
                {
                    BusinessId = purchase.BusinessId,
                    ProductId = purchase.ProductId,
                    AddressId = purchase.AddressId,
                    UserId = purchase.UserId,
                    UpdatedDate = purchase.UpdatedDate
                })
            .Run(Conn);

            string Id = Result.generated_keys[0].ToString();

            return Id;
        }

        public bool Delete(string id)
        {
            var R = RethinkDb.Driver.RethinkDB.R;
            var Conn = R.Connection().Hostname("localhost").Port(28015).Timeout(60).Connect();

            var Query = R.Db("MyCRM").Table("Purchases").Get(id).Delete().Run(Conn);
            
            if(Query == null)
            {
                return false;
            }

            return true;
        }

        public bool DeleteAllByBusiness(string id)
        {
            var R = RethinkDb.Driver.RethinkDB.R;
            var Conn = R.Connection().Hostname("localhost").Port(28015).Timeout(60).Connect();

            var Query = R.Db("MyCRM").Table("Purchases").GetAll(id)[new { index = "BusinessId" }].Delete().Run(Conn);

            if (Query.Count == 0)
            {
                return false;
            }

            return true;
        }
    }
}
