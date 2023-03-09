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
        public Task<User> checkUserExist(string name, string password);
        public Task SignIn(User user);
        public Task ChangePassword(string name, string Password);

        public Task<string> getEmail(string name);

    }
}
