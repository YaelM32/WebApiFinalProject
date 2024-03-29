﻿using DataAccess.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepository
{
    public interface IUserRepository
    {

        public Task<User> checkUserExist(string email);
        public Task SignIn(User user);
        public Task ChangePassword(User user, string Password);
        public Task<User> getUserById(int id);
        public Task<User> getUserByEmail(string email);
    }
}
