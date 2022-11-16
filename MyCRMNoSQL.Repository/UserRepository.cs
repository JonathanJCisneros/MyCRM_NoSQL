using MyCRMNoSQL.Core;
using MyCRMNoSQL.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCRMNoSQL.Repository
{
    public class UserRepository : IUserRepository
    {
        public async Task<bool> CheckByEmail(string email)
        {
            return false;
        }

        public async Task<string> Login(User user)
        {
            return null;
        }

        public async Task<string> Register(User user)
        {
            return null;
        }

        public async Task<User> Get(string id) 
        {
            return null;
        }

        public async Task<List<User>> GetAll()
        {
            return null;
        }

        public async Task<bool> Create(User user)
        {
            return false;
        }

        public async Task<bool> Update(User user)
        {
            return false;
        }

        public async Task<bool> Delete(string id)
        {
            return false;
        }
    }
}
