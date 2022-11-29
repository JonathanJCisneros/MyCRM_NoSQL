using MyCRMNoSQL.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCRMNoSQL.Service.Interfaces
{
    public interface IUserService : ICRUDService<User>
    { 

        bool CheckByEmail(string id);

        User Login(string email);

        void UpdateTimeStamp(string id);
    }
}
