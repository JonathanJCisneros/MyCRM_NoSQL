using MyCRMNoSQL.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCRMNoSQL.Service.Interfaces
{
    public interface IBusinessService : ICRUDService<Business>
    {
        bool CheckByName(string name);

        List<Business> GetAllWithLatestActivity();

        List<Business> GetAllByIndustry(string industry);

        string CreateClient(Business business, Address address, Staff staff);
    }
}
