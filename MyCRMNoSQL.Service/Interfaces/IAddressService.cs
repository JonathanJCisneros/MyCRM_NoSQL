using MyCRMNoSQL.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCRMNoSQL.Service.Interfaces
{
    public interface IAddressService
    {
        List<Address> GetAllByBusiness(string id);

        List<Address> GetAllByCity(string city);

        List<Address> GetAllByState(string state);

        List<Address> GetAllByZipCode(int zipCode);

        Address Get(string id);

        List<Address> GetAll();

        string Create(Address entity);

        string Update(Address entity);

        bool Delete(string id);

        bool DeleteAllByBusiness(string id);
    }
}
