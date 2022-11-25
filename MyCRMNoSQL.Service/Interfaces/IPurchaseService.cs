using MyCRMNoSQL.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCRMNoSQL.Service.Interfaces
{
    public interface IPurchaseService : ICRUDService<Purchase>, IBService<Purchase>
    {
        List<Purchase> GetAllByIndustry(string industry);

        List<Purchase> GetAllByCity(string city);

        List<Purchase> GetAllByZipCode(int zipCode);

        List<Purchase> GetAllByUser(string id);
    }
}
