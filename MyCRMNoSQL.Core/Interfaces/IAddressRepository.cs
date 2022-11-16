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
        Task<List<Address>> GetAllByCity(string city);

        Task<List<Address>> GetAllByZipCode(int zipCode);
    }
}
