using BusinessLogic.Dto;
using DataAccess.DBModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.IService
{
    public interface IShulService
    {
        public Task<int> SignIn(Shul shul);
        public Task UploadFile(int shulId, IFormFile userfile);
        public Task SetMap(int shulId, string fileName);
        public Task SetLogo(int shulId, string fileName);

        public Task<Shul> GetShulById(int shulId);

    }
}
