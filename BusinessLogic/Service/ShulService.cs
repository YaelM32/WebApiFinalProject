using BusinessLogic.Dto;
using BusinessLogic.IService;
using DataAccess.DBModels;
using DataAccess.IRepository;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Service
{
    public class ShulService : IShulService
    {
        IShulRepository shulRepository;

        public ShulService(IShulRepository shulRepository)
        {
            this.shulRepository = shulRepository;
        }

        public Task<Shul> GetShulById(int shulId)
        {
            return shulRepository.GetShulById(shulId);
        }

        public Task SetMap(int shulId, string fileName)
        {
            return shulRepository.SetMap(shulId,fileName);
        }

        public Task<int> SignIn(Shul shul)//, string FileName)
        {
            return shulRepository.SignIn(shul);//,  FileName);
        }

        public Task UploadFile(int shulId, IFormFile userfile)
        {
            return shulRepository.UploadFile(shulId, userfile);
        }
    }
}
