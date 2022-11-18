using MyCRMNoSQL.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCRMNoSQL.Service.Interfaces
{
    public interface IBusinessService
    {
        bool CheckByName(string name);

        List<Business> GetAllWithLatestActivity();

        List<Business> GetAllByIndustry(string industry);

        Business Get(string id);

        List<Business> GetAll();

        string Create(Business entity);

        string Update(Business entity);

        bool Delete(string id);
    }
}
