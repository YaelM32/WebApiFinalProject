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
        public async Task<User> checkUserExist(string email, string password)
        {
           return await userService.checkUserExist(email, password);
        }
       

        //POST api/<UserController>
        [HttpPost("SignIn")]
        public Task SignIn([FromBody] UserDto userDto)
        {           
            User user = mapper.Map<User>(userDto);
            return userService.SignIn(user);
        }

        // PUT api/<UserController>
        [HttpPut("ChangePassword")]
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
