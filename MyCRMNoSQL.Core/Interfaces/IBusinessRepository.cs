using MyCRMNoSQL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCRMNoSQL.Core.Interfaces
{
    public interface IBusinessRepository : ICRUDRepository<Business>
    {
        bool CheckByName(string name);

        List<Business> GetAllByIndustry(string industry);
    }
}
