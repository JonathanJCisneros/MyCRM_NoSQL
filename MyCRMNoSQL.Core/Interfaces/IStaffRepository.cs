using MyCRMNoSQL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCRMNoSQL.Core.Interfaces
{
    public interface IStaffRepository : ICRUDRepository<Staff>, IBRepository<Staff>
    {
        Task<List<Staff>> GetAllByPosition(string position);
    }
}
