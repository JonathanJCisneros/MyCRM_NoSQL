using MyCRMNoSQL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCRMNoSQL.Core.Interfaces
{
    public interface IUserRepository : ICRUDRepository<User>
    {
        Task<bool> CheckByEmail(string email);

        Task<string> Login(User user);

        Task<string> Register(User user);
    }
}
