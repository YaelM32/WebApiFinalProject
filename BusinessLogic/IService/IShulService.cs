using DataAccess.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.IService
{
    public interface IShulService
    {
        public Task<int> SignIn(Shul shul);//, string FileName);

    }
}
