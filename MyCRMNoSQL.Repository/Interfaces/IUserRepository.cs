using MyCRMNoSQL.Repository;
using MyCRMNoSQL.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCRMNoSQL.Repository.Interfaces
{
    public interface IUserRepository : ICRUDRepository<User>
    {
        bool CheckByEmail(string email);

        User Login(string email);
    }
}
