using MyCRMNoSQL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCRMNoSQL.Core.Interfaces
{
    public interface IAddressRepository : ICRUDRepository<Address>, IBRepository<Address>
    { 
        List<Address> GetAllByCity(string city);

        List<Address> GetAllByZipCode(int zipCode);
    }
}
