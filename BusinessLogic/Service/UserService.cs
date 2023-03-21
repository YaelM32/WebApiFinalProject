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

        public Task<User> checkUserExist(string email, string password)
        {
            return userRepository.checkUserExist(email, password);
        }

        public Task SignIn(User user)
        {
            return userRepository.SignIn(user);
        }

        public Task ChangePassword(string email, string Password)
        {
            return userRepository.ChangePassword(email, Password);
        }

        public Task getEmail(string email)
        {
            return userRepository.getEmail(email);

        }
    }
}
