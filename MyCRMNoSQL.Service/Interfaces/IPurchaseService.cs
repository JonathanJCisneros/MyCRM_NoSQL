using MyCRMNoSQL.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCRMNoSQL.Service.Interfaces
{
    public interface IPurchaseService
    {
        List<Purchase> GetAllByBusiness(string id);

        List<Purchase> GetAllByIndustry(string industry);

        List<Purchase> GetAllByCity(string city);

        List<Purchase> GetAllByZipCode(int zipCode);

        List<Purchase> GetAllByUser(string id);

        Purchase Get(string id);

        List<Purchase> GetAll();

        string Create(Purchase entity);

        string Update(Purchase entity);

        bool Delete(string id);

        bool DeleteAllByBusiness(string id);
    }
}
