using AutoMapper;
using BusinessLogic.Dto;
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



        // GET: api/<UserController>
        [EnableCors("AllowOrigin")]
        [HttpGet,Route("login")]
        public async Task<User> checkUserExist(string name, string password)
        {
           return await userService.checkUserExist(name, password);
        }
       

        //POST api/<UserController>
        [HttpPost("SignIn")]
        public Task SignIn([FromBody] UserDto userDto)
        {           
            User user = mapper.Map<User>(userDto);
            return userService.SignIn(user);
        }

        // PUT api/<UserController>
        [HttpPut("{name}")]
        public Task ChangePassword(string name,[FromBody] string Password)
        {
            return userService.ChangePassword(name, Password);
        }
        [HttpGet("getEmailByName")]
        public Task<string> getEmail(string name)
        {
            return userService.getEmail(name);
        }
        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
