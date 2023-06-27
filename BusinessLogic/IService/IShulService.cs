using BusinessLogic.DTO;
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
        Task<List<Shul>> GetShuls();
        Task<int> SignIn(Shul shul);
        Task UploadFile(int shulId, IFormFile userfile);
        Task SetMap(int shulId, string fileName);
        Task SetLogo(int shulId, string fileName);
        Task<Shul> GetShulById(int shulId);
        public Task EditShulDetails(int shulId, Shul shulDTO);
    }
}
