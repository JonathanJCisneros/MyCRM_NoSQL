using MyCRMNoSQL.Core;
using MyCRMNoSQL.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace MyCRMNoSQL.Repository
{
    public class AddressRepository : IAddressRepository
    {
        public bool CheckById(string id)
        {
            var R = RethinkDb.Driver.RethinkDB.R;
            var Conn = R.Connection().Hostname("localhost").Port(28015).Timeout(60).Connect();
            bool Check = R.Db("MyCRM").Table("Addresses").Get(id).IsEmpty().Run(Conn);

            return Check;
        }

        public Address Get(string id)
        {
            var R = RethinkDb.Driver.RethinkDB.R;
            var Conn = R.Connection().Hostname("localhost").Port(28015).Timeout(60).Connect();

            var Query = R.Db("MyCRM").Table("Addresses").Get(id).Run(Conn);

            if(Query == null)
            {
                return null;
            }

            Address address = new()
            {
                Id = Query.id.ToString(),
                Street = Query.Street.ToString(),
                AptSuite = Query.AptSuite.ToString(),
                City = Query.City.ToString(),
                State = Query.State.ToString(),
                ZipCode = Query.ZipCode.ToInt32(),
                CreatedDate = Query.CreatedDate.ToDateTime(),
                UpdatedDate = Query.UpdatedDate.ToDateTime(),
                BuisinessId = Query.BuisinessId.ToString()
            };

            return address;
        }

        public List<Address> GetAll()
        {
            var R = RethinkDb.Driver.RethinkDB.R;
            var Conn = R.Connection().Hostname("localhost").Port(28015).Timeout(60).Connect();

            var Query = R.Db("MyCRM").Table("Addresses").Run(Conn);

            if (Query.BufferedSize == 0)
            {
                return null;
            }

            List<Address> AddressList = new();

            foreach (var item in Query)
            {
                Address address = new()
                {
                    Id = item.id.ToString(),
                    Street = item.Street.ToString(),
                    AptSuite = item.AptSuite.ToString(),
                    City = item.City.ToString(),
                    State = item.State.ToString(),
                    ZipCode = item.ZipCode.ToInt32(),
                    CreatedDate = item.CreatedDate.ToDateTime(),
                    UpdatedDate = item.UpdatedDate.ToDateTime(),
                    BuisinessId = item.BuisinessId.ToString()
                };

                AddressList.Add(address);
            }

            return AddressList;
        }

        public List<Address> GetAllByBusiness(string id)
        {
            var R = RethinkDb.Driver.RethinkDB.R;
            var Conn = R.Connection().Hostname("localhost").Port(28015).Timeout(60).Connect();

            var Query = R.Db("MyCRM").Table("Addresses").GetAll(id)[new { index = "BusinessId" }].Run(Conn);

            if (Query.BufferedSize == 0)
            {
                return null;
            }

            List<Address> AddressList = new();

            foreach (var item in Query)
            {
                Address address = new()
                {
                    Id = item.id.ToString(),
                    Street = item.Street.ToString(),
                    AptSuite = item.AptSuite.ToString(),
                    City = item.City.ToString(),
                    State = item.State.ToString(),
                    ZipCode = item.ZipCode.ToInt32(),
                    CreatedDate = item.CreatedDate.ToDateTime(),
                    UpdatedDate = item.UpdatedDate.ToDateTime(),
                    BuisinessId = item.BuisinessId.ToString()
                };

                AddressList.Add(address);
            }

            return AddressList;
        }

        public List<Address> GetAllByCity(string city)
        {
            var R = RethinkDb.Driver.RethinkDB.R;
            var Conn = R.Connection().Hostname("localhost").Port(28015).Timeout(60).Connect();

            var Query = R.Db("MyCRM").Table("Addresses").GetAll(city)[new { index = "City" }].Run(Conn);

            if (Query.BufferedSize == 0)
            {
                return null;
            }

            List<Address> AddressList = new();

            foreach (var item in Query)
            {
                Address address = new()
                {
                    Id = item.id.ToString(),
                    Street = item.Street.ToString(),
                    AptSuite = item.AptSuite.ToString(),
                    City = item.City.ToString(),
                    State = item.State.ToString(),
                    ZipCode = item.ZipCode.ToInt32(),
                    CreatedDate = item.CreatedDate.ToDateTime(),
                    UpdatedDate = item.UpdatedDate.ToDateTime(),
                    BuisinessId = item.BuisinessId.ToString()
                };

                AddressList.Add(address);
            }

            return AddressList;
        }

        public List<Address> GetAllByState(string state)
        {
            var R = RethinkDb.Driver.RethinkDB.R;
            var Conn = R.Connection().Hostname("localhost").Port(28015).Timeout(60).Connect();

            var Query = R.Db("MyCRM").Table("Addresses").GetAll(state)[new { index = "State" }].Run(Conn);

            if (Query.BufferedSize == 0)
            {
                return null;
            }

            List<Address> AddressList = new();

            foreach (var item in Query)
            {
                Address address = new()
                {
                    Id = item.id.ToString(),
                    Street = item.Street.ToString(),
                    AptSuite = item.AptSuite.ToString(),
                    City = item.City.ToString(),
                    State = item.State.ToString(),
                    ZipCode = item.ZipCode.ToInt32(),
                    CreatedDate = item.CreatedDate.ToDateTime(),
                    UpdatedDate = item.UpdatedDate.ToDateTime(),
                    BuisinessId = item.BuisinessId.ToString()
                };

                AddressList.Add(address);
            }

            return AddressList;
        }

        public List<Address> GetAllByZipCode(int zipCode)
        {
            var R = RethinkDb.Driver.RethinkDB.R;
            var Conn = R.Connection().Hostname("localhost").Port(28015).Timeout(60).Connect();

            var Query = R.Db("MyCRM").Table("Addresses").GetAll(zipCode)[new { index = "ZipCode" }].Run(Conn);

            if (Query.BufferedSize == 0)
            {
                return null;
            }

            List<Address> AddressList = new();

            foreach (var item in Query)
            {
                Address address = new()
                {
                    Id = item.id.ToString(),
                    Street = item.Street.ToString(),
                    AptSuite = item.AptSuite.ToString(),
                    City = item.City.ToString(),
                    State = item.State.ToString(),
                    ZipCode = item.ZipCode.ToInt32(),
                    CreatedDate = item.CreatedDate.ToDateTime(),
                    UpdatedDate = item.UpdatedDate.ToDateTime(),
                    BuisinessId = item.BuisinessId.ToString()
                };

                AddressList.Add(address);
            }

            return AddressList;
        }

        public string Create(Address address)
        {
            var R = RethinkDb.Driver.RethinkDB.R;
            var Conn = R.Connection().Hostname("localhost").Port(28015).Timeout(60).Connect();

            var Query = R.Db("MyCRM").Table("Addresses")
                .Insert(new 
                {
                    Street = address.Street,
                    AptSuite = address.AptSuite,
                    City = address.City,
                    State = address.State,
                    ZipCode = address.ZipCode,
                    CreatedDate = address.CreatedDate,
                    UpdatedDate = address.UpdatedDate,
                    BusinessId = address.BuisinessId
                })
            .Run(Conn);

            return address.Id;
        }

        public string Update(Address address)
        {
            var R = RethinkDb.Driver.RethinkDB.R;
            var Conn = R.Connection().Hostname("localhost").Port(28015).Timeout(60).Connect();

            var Query = R.Db("MyCRM").Table("Addresses").Get(address.Id)
                .Update(new
                {
                    Street = address.Street,
                    AptSuite = address.AptSuite,
                    City = address.City,
                    State = address.State,
                    ZipCode = address.ZipCode,
                    UpdatedDate = address.UpdatedDate
                })
            .Run(Conn);

            return address.Id;
        }

        public bool Delete(string id)
        {
            var R = RethinkDb.Driver.RethinkDB.R;
            var Conn = R.Connection().Hostname("localhost").Port(28015).Timeout(60).Connect();

            var Query = R.Db("MyCRM").Table("Addresses").Get(id).Delete().Run(Conn);

            if (Query.deleted == 0)
            {
                return false;
            }

            return true;
        }

        public bool DeleteAllByBusiness(string id)
        {
            var R = RethinkDb.Driver.RethinkDB.R;
            var Conn = R.Connection().Hostname("localhost").Port(28015).Timeout(60).Connect();

            var Query = R.Db("MyCRM").Table("Addresses").GetAll(id)[new { index = "BusinessId" }].Delete().Run(Conn);

            if(Query.deleted == 0)
            {
                return false;
            }

            return true;
        }
    }
}
