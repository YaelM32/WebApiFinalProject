using DataAccess.DBModels;
using DataAccess.IRepository;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
//using System.Net.WebUtility.UrlEncode;

using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.Services;
using System.IO;
using System.Threading;
using Org.BouncyCastle.Cms;
using static System.Net.WebRequestMethods;
using System.Text.Encodings.Web;

namespace DataAccess.Repository
{
    public class UserRepository : IUserRepository
    {
        BookDBContext dbContext;
        public UserRepository(BookDBContext _dbContext)
        {
            dbContext = _dbContext;
        }

        public async Task<User> checkUserExist(string email, string password)
        {
            try
            {
                User u1 = await dbContext.Users.Where(u => u.Email == email && u.Password == password).FirstOrDefaultAsync();
                return u1 == null ? null : u1;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in checkUserExist function " + ex.Message);
            }

        }

        public async Task SignIn(User user)
        {
            try
            {
                User u = await dbContext.Users.Where(u => u.Name == user.Name).FirstOrDefaultAsync();
                if (u != null && u.Name == user.Name)
                {
                    Console.WriteLine("The name is exist");
                }
                else
                {
                    dbContext.Users.AddAsync(user);
                    await dbContext.SaveChangesAsync();
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error in SignIn function " + ex.Message);
            }
        }

        public async Task ChangePassword(string email, string Password)
        {
            try
            {
                email = Base64Decode(email);
                User u = await dbContext.Users.Where(u => u.Email == email).FirstOrDefaultAsync();
                if (u != null)
                {
                    u.Password = Password;
                    dbContext.Users.Update(u);
                    await dbContext.SaveChangesAsync();
                }
                else
                {
                    Console.WriteLine("The user isn't exist");
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error in SignIn function " + ex.Message);
            }
        }

        public static string Base64Encode(string text)
        {
            var textBytes = System.Text.Encoding.UTF8.GetBytes(text);
            return System.Convert.ToBase64String(textBytes);
        }
        public static string Base64Decode(string base64)
        {
            var base64Bytes = System.Convert.FromBase64String(base64);
            return System.Text.Encoding.UTF8.GetString(base64Bytes);
        }

        public async Task<User> getUserById(int id)
        {
            User u = await dbContext.Users.Where(u => u.Id == id).FirstOrDefaultAsync();
            return u;
        }
     
    }
}
