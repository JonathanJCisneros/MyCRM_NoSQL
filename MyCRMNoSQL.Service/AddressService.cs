using MyCRMNoSQL.Core;
using MyCRMNoSQL.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCRMNoSQL.Service
{
    public class AddressService : IAddressService
    {
        public bool CheckById(string id)
        {
            return false;
        }

        public Address Get(string id)
        {
            return null;
        }

        public List<Address> GetAll()
        {
            return null;
        }

        public List<Address> GetAllByCity(string city)
        {
            return null;
        }

        public List<Address> GetAllByState(string state)
        {
            return null;
        }

        public List<Address> GetAllByZipCode(int zipCode)
        {
            return null;
        }

        public List<Address> GetAllByBusiness(string id)
        {
            return null;
        }

        public string Create(Address address)
        {
            return null;
        }

        public string Update(Address address)
        {
            return null;
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
