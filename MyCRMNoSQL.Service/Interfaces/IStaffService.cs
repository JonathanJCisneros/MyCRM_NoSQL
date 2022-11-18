using MyCRMNoSQL.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCRMNoSQL.Service.Interfaces
{
    public interface IStaffService
    {
        List<Staff> GetAllByPosition(string position);
        
        List<Staff> GetAllByBusiness(string id);

        Staff Get(string id);

        List<Staff> GetAll();

        string Create(Staff entity);

        string Update(Staff entity);

        bool Delete(string id);

        bool DeleteAllByBusiness(string id);
    }
}
