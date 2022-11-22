using MyCRMNoSQL.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCRMNoSQL.Service.Interfaces
{
    public interface IUserService
    {
        bool CheckByEmail(string id);

        string Register(User user);

        User Login(string email);

        User Get(string id);

        List<User> GetAll();

        string Update(User user);

        void UpdateTimeStamp(string id);

        bool Delete(string id);
    }
}
