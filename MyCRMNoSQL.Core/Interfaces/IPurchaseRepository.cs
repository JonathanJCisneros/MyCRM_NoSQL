using MyCRMNoSQL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCRMNoSQL.Core.Interfaces
{
    public interface IPurchaseRepository : ICRUDRepository<Purchase>, IBRepository<Purchase>
    {
        Task<List<Purchase>> GetAllByIndustry(string industry);

        Task<List<Purchase>> GetAllByCity(string city);

        Task<List<Purchase>> GetAllByZipCode(int zipCode);

        Task<List<Purchase>> GetAllByUser(string id);
    }
}
