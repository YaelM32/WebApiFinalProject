using AutoMapper;
using BusinessLogic.Dto;
using BusinessLogic.IService;
using DataAccess.DBModels;
using Microsoft.AspNetCore.Mvc;

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

        // GET: api/<ShulController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ShulController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

      

        //[HttpPost]
        //public IActionResult PostFile(IFormFile file,)
        //{

        //    var saveFilePath = Path.Combine("c:\\savefilepath\\", model.UploaderAddress!);
        //    using (var stream = new FileStream(saveFilePath, FileMode.Create))
        //    {
        //        file.CopyToAsync(stream);
        //    }
        //    return Ok();
        //}


        // POST api/<ShulController>
        [HttpPost("SignIn")]
        public Task<int> SignIn([FromBody] ShulDto shulDto)
        {
           // string saveFilePath = Path.Combine("M:\\Final Project\\WebApiFinalProject\\WebApiFinalProject\\wwwroot\\images", shulDto.Name);
           
            Shul shul = mapper.Map<Shul>(shulDto);
            return shulService.SignIn(shul);//, saveFilePath);
        }
        // PUT api/<ShulController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {

        }

        // DELETE api/<ShulController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }




}