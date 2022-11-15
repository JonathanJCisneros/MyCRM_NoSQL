using MyCRMNoSQL.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCRMNoSQL.Repository
{
    public class UserRepository : IRepository<User>
    {
        public async Task<bool> Check(string Id)
        {
            return false;
        }

        public async Task<User> Login(string Id)
        {
            return null;
        }
        
        public async Task<User> Get(string Id) 
        {
            return null;
        }

        public async Task<User> Create(User User)
        {
            return null;
        }

        public async Task<User> Update(User User)
        {
            return null;
        }

        public async Task<string> Delete(string Id)
        {
            return null;
        }
    }
}
