using BusinessLogic.DTO;
using BusinessLogic.IService;
using DataAccess.DBModels;
using DataAccess.IRepository;
using DataAccess.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Service;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Service
{
    public class UserService : IUserService
    {
        IUserRepository userRepository;
        IConfiguration configuration;
        IPasswordHashHelper passwordHashHelper;
        public UserService(IUserRepository _userRepository, IConfiguration _configuration, IPasswordHashHelper _passwordHashHelper)
        {
            userRepository = _userRepository;
            configuration = _configuration;
            passwordHashHelper = _passwordHashHelper;
        }
        //בדיקה האם המשתמש קיים במערכת
        public async Task<User> checkUserExist(string email, string password)
        {
            User user = await userRepository.checkUserExist(email);
            if (user != null)
            {
                string Hashedpassword = passwordHashHelper.HashPassword(password, user.Salt, 1000, 8);
                if (!Hashedpassword.Equals(user.Password.TrimEnd()))
                    return null;


                //User user = userRepository.checkUserExist(email, password).Result;
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(configuration.GetSection("key").Value);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                user.Token = tokenHandler.WriteToken(token);
            }
            return user;

        }
        //רישום משתמש חדש
        public Task SignIn(User user)
        {
            user.Salt = passwordHashHelper.GenerateSalt(8);
            user.Password = passwordHashHelper.HashPassword(user.Password, user.Salt, 1000, 8);
            return userRepository.SignIn(user);
        }
        //שינוי סיסמא
        public Task ChangePassword(string email, string Password)
        {
            //user.Salt = passwordHashHelper.GenerateSalt(8);
            //user.Password = passwordHashHelper.HashPassword(user.Password, user.Salt, 1000, 8);
            return userRepository.ChangePassword(email, Password);

        }

        public Task<User> getUserById(int id)
        {
            return userRepository.getUserById(id);
        }


    }
}
