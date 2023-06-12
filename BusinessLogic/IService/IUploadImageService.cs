using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.IService
{
    public interface IUploadImageService
    {
        Task<string> SaveBookImg(IFormFile userfile);
    }
}
