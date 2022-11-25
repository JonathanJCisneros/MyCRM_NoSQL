using MyCRMNoSQL.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCRMNoSQL.Service.Interfaces
{
    public interface IAddressService : ICRUDService<Address>, IBService<Address> 
    {
        List<Address> GetAllByCity(string city);

        List<Address> GetAllByState(string state);

        List<Address> GetAllByZipCode(int zipCode);
    }
}
