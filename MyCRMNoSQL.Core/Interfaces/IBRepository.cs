using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCRMNoSQL.Core.Interfaces
{
    public interface IBRepository<T> where T : new()
    {
        Task<List<T>> GetAllByBusiness(string id);

        Task<bool> DeleteAllByBusiness(string id);
    }
}
