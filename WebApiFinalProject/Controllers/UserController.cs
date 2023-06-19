﻿using AutoMapper;
using BusinessLogic.DTO;
using BusinessLogic.IService;
using DataAccess.DBModels;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiFinalProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        IUserService userService;
        IMapper mapper;
        public UserController(IUserService _userService, IMapper _mapper)
        {
            userService = _userService;
            mapper = _mapper;
        }



        [EnableCors("AllowOrigin")]
        [HttpGet,Route("login")]
        //ביצוע בדיקה האם המשתמש שנכנס כרגע רשום במערכת
        public async Task<User> checkUserExist(string email, string password)
        {
           return await userService.checkUserExist(email, password);
        }
       
        [HttpPost("SignIn")]
        //הוספת משתמשים חדשים לבית כנסת
        public Task SignIn([FromBody] UserDTO userDTO)
        {           
            User user = mapper.Map<User>(userDTO);
            return userService.SignIn(user);
        }

        [HttpPut("ChangePassword")]
        //שינוי סיסמא לבית כנסת
        public Task ChangePassword(string email, string Password)
        {
            return userService.ChangePassword(email, Password);
        }
        [HttpGet("getEmail")]
        public Task getEmail([FromQuery]string email)
        {
            return userService.getEmail(email);
        }
        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
