using BusinessLogic.IService;
using DataAccess.DBModels;
using DataAccess.IRepository;
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

        public Task<int> SignIn(Shul shul)//, string FileName)
        {
            return shulRepository.SignIn(shul);//,  FileName);
        }
    }
}
