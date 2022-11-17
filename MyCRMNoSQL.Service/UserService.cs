using MyCRMNoSQL.Core;
using MyCRMNoSQL.Core.Interfaces;
using MyCRMNoSQL.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCRMNoSQL.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public bool CheckByEmail(string email)
        {
            return _userRepository.CheckByEmail(email);
        }

        public string Register(User user)
        {
            return _userRepository.Register(user);
        }

        public User Login(string email)
        {
            return _userRepository.Login(email);
        }

        public User Get(string id)
        {
            return _userRepository.Get(id);
        }

        public User GetByEmail(string email)
        {
            return _userRepository.GetByEmail(email);
        }

        public List<User> GetAll()
        {
            return _userRepository.GetAll();
        }

        public bool Update(User user)
        {
            return _userRepository.Update(user);
        }

        public bool Delete(string id)
        {
            return _userRepository.Delete(id);
        }
    }
}
