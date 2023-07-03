using AutoMapper;
using BusinessLogic.DTO;
using BusinessLogic.IService;
using DataAccess.DBModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace WebApiFinalProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
        [AllowAnonymous]
        public async Task<User> checkUserExist(string email, string password)
        {
           return await userService.checkUserExist(email, password);
        }
       
        [HttpPost("SignIn")]
        //הוספת משתמשים חדשים לבית כנסת
        [AllowAnonymous]
        public Task SignIn([FromBody] UserDTO userDTO)
        {           
            User user = mapper.Map<User>(userDTO);
            return userService.SignIn(user);
        }

        [HttpPut("ChangePassword")]
        //שינוי סיסמא לבית כנסת
        [AllowAnonymous]
        public Task ChangePassword(string email, string Password)
        {
            return userService.ChangePassword(email, Password);
        }
        [HttpGet("getEmail")]
        [AllowAnonymous]
        public Task getEmail([FromQuery]string email)
        {
            return EmailController.sendEmailForChangePwd(email);
        }
        [HttpPost("sendReceipt")]
        public Task sendReceipt([FromQuery] int idUser,[FromBody]List<BookDTO> books)
        {
            User user = userService.getUserById(idUser).Result;
            return EmailController.sendReceiptEmail(user,books);
        }
    }
}
