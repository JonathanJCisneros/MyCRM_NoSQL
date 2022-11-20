using MyCRMNoSQL.Repository;
using MyCRMNoSQL.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCRMNoSQL.Repository.Interfaces
{
    public interface IBusinessRepository : ICRUDRepository<Business>
    {
        bool CheckByName(string name);

        List<Business> GetAllWithLatestActivity();

        List<Business> GetAllByIndustry(string industry);
    }
}
