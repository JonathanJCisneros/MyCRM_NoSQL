using MyCRMNoSQL.Core;
using MyCRMNoSQL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCRMNoSQL.Repository.Interfaces
{
    public interface IPurchaseRepository : ICRUDRepository<Purchase>, IBRepository<Purchase>
    {
        List<Purchase> GetAllByIndustry(string industry);

        List<Purchase> GetAllByCity(string city);

        List<Purchase> GetAllByZipCode(int zipCode);

        List<Purchase> GetAllByUser(string id);
    }
}
