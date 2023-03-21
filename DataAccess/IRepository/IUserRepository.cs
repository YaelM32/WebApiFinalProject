using DataAccess.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepository
{
    public interface IUserRepository
    {
        public Task<User> checkUserExist(string email, string password);
        public Task SignIn(User user);
        public Task ChangePassword(string email, string Password);
        public Task getEmail(string email);
    }
}
