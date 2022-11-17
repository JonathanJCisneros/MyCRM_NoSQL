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

        User GetByEmail(string email);

        List<User> GetAll();

        bool Update(User user);

        bool Delete(string id);
    }
}
