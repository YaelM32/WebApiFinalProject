using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApiFinalProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadImageController : ControllerBase
    {
        [HttpPost]
        //שמירת התמונה של ספר ולוגו
        public static async Task<string> SaveBookImg(IFormFile userfile)
        {
            string filename = userfile.FileName;
            filename = Path.GetFileName(filename);
            string uploadFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", filename);
            await using var stream = new FileStream(uploadFilePath, FileMode.Create);
            await userfile.CopyToAsync(stream);
            return filename;
        }
        [HttpGet]
        //קבלת השם של התמונה שנשמרה
        public async Task<string> ReturnBookImgName(IFormFile userfile)
        {
            return userfile.FileName;
        }
    }
}
