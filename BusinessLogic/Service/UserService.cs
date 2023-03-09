using BusinessLogic.IService;
using DataAccess.DBModels;
using DataAccess.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Service
{
    public class UserService:IUserService
    {
        IUserRepository userRepository;

        public UserService(IUserRepository _userRepository)
        {
            userRepository = _userRepository;
        }

        public Task<User> checkUserExist(string name, string password)
        {
            return userRepository.checkUserExist(name, password);
        }

        public Task SignIn(User user)
        {
            return userRepository.SignIn(user);
        }

        public Task ChangePassword(string name, string Password)
        {
            return userRepository.ChangePassword(name, Password);
        }

        public Task<string> getEmail(string name)
        {
            return userRepository.getEmail(name);

        }
    }
}
