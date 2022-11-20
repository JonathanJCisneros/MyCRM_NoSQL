using MyCRMNoSQL.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCRMNoSQL.Repository.Interfaces
{
    public interface IBRepository<T> where T : new()
    {
        List<T> GetAllByBusiness(string id);

        bool DeleteAllByBusiness(string id);
    }
}
