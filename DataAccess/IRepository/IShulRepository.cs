using DataAccess.DBModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepository
{
    public interface IShulRepository
    {
        public Task<int> SignIn(Shul shul);//, string FileName);
        public Task UploadFile(int shulId, IFormFile userfile);

        public Task SetMap(int shulId, string fileName);
        public Task<Shul> GetShulById(int shulId);


    }
}
