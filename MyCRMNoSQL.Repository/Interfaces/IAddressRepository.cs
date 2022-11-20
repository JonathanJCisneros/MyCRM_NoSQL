using MyCRMNoSQL.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCRMNoSQL.Repository.Interfaces
{
    public interface IAddressRepository : ICRUDRepository<Address>, IBRepository<Address>
    { 
        List<Address> GetAllByCity(string city);

        List<Address> GetAllByState(string state);

        List<Address> GetAllByZipCode(int zipCode);
    }
}
