using BusinessLogic.DTO;
using DataAccess.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.IService
{
    public interface IUserService
    {
        public Task<User> checkUserExist(string email, string password);
        public Task SignIn(User user);
        public Task ChangePassword(string email, string Password);
        public Task<User> getUserById(int id);

    }
}
