using MyCRMNoSQL.Core;
using MyCRMNoSQL.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCRMNoSQL.Repository
{
    public class AddressRepository : IAddressRepository
    {
        public Address Get(string id)
        {
            return null;
        }

        public List<Address> GetAll()
        {
            return null;
        }

        public List<Address> GetAllByBusiness(string id)
        {
            return null;
        }

        public List<Address> GetAllByCity(string city)
        {
            return null;
        }

        public List<Address> GetAllByZipCode(int zipCode)
        {
            return null;
        }

        public bool Create(Address address)
        {
            return false;
        }

        public bool Update(Address address)
        {
            return false;
        }

        public bool Delete(string id)
        {
            return false;
        }

        public bool DeleteAllByBusiness(string id)
        {
            return false;
        }
    }
}
