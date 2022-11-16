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
        Task<bool> CheckByName(string name);

        Task<List<Business>> GetAllByIndustry(string industry);
    }
}
