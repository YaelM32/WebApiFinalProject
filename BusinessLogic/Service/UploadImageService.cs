using BusinessLogic.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Service
{
    public class UploadImageService:IUploadImageService
    {
        public async Task<string> SaveBookImg(IFormFile userfile)
        {
            string filename = userfile.FileName;
            filename = Path.GetFileName(filename);
            string uploadFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", filename);
            await using var stream = new FileStream(uploadFilePath, FileMode.Create);
            await userfile.CopyToAsync(stream);
            return filename;
        }
        public async Task<string> ReturnBookImgName(IFormFile userfile)
        {
            return userfile.FileName;         
        }
    }
}
