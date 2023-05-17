using AutoMapper;
using BusinessLogic.Dto;
using BusinessLogic.IService;
using DataAccess.DBModels;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiFinalProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShulController : ControllerBase
    {
        IShulService shulService;
        IMapper mapper;
        public ShulController(IShulService _shulService, IMapper _mapper)
        {
            shulService = _shulService;
            mapper = _mapper;
        }
        [HttpPost("SignIn")]
        public Task<int> SignIn([FromBody] ShulDto shulDto)
        {
            Shul shul = mapper.Map<Shul>(shulDto);
            return shulService.SignIn(shul);
        }


        [HttpPost, Route("UploadImage")]
        public async Task UploadFile(int shulId, IFormFile userfile)
        {

            try
            {
                shulService.UploadFile(shulId, userfile);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            try
            {
                await SaveFileName(userfile);
                SetMap(shulId, userfile.FileName);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        [HttpGet, Route("SaveFileName")]

        public async Task SaveFileName(IFormFile userfile)
        {
            string filename = userfile.FileName;
            filename = Path.GetFileName(filename);
            string uploadFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", filename);
            await using var stream = new FileStream(uploadFilePath, FileMode.Create);
            await userfile.CopyToAsync(stream);
        }
        [HttpPost, Route("UploadLogo")]
        public async Task UploadLogo(int shulId, IFormFile logo)
        {
            try
            {
                shulService.UploadFile(shulId, logo);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            try
            {
                await SaveFileName(logo);
                SetLogo(shulId, logo.FileName);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        [HttpPut("SetMap")]

        public Task SetMap(int shulId, string Filename)
        {
            return shulService.SetMap(shulId, Filename);
        }
        [HttpPut("SetLogo")]

        public Task SetLogo(int shulId, string Filename)
        {
            return shulService.SetLogo(shulId, Filename);
        }

        [HttpGet("GetShulById")]
        public async Task<ShulDto> GetShulById([FromQuery] int shulId)
        {
            Shul s = await shulService.GetShulById(shulId);
            return mapper.Map<ShulDto>(s);
        }

    }
}




