using MyCRMNoSQL.Core;
using MyCRMNoSQL.Repository.Interfaces;
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

        public bool CheckById(string id)
        {
            return _userRepository.CheckById(id);
        }

        public bool CheckByEmail(string email)
        {
            return _userRepository.CheckByEmail(email);
        }

        public string Create(User user)
        {
            return _userRepository.Create(user);
        }

        public User Login(string email)
        {
            User user = _userRepository.Login(email);

            if(user == null)
            {
                return null;
            }

            UpdateTimeStamp(user.Id);

            return user;
        }

        public User Get(string id)
        {
            return _userRepository.Get(id);
        }

        public List<User> GetAll()
        {
            return _userRepository.GetAll();
        }

        public string Update(User user)
        {
            return _userRepository.Update(user);
        }

        public void UpdateTimeStamp(string id)
        {
            _userRepository.UpdateTimeStamp(id);
        }

        public bool Delete(string id)
        {
            return _userRepository.Delete(id);
        }
    }
}
