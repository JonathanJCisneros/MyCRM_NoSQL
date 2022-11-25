using MyCRMNoSQL.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCRMNoSQL.Service.Interfaces
{
    public interface IStaffService : ICRUDService<Staff>, IBService<Staff>
    {
        List<Staff> GetAllByPosition(string position);
    }
}
