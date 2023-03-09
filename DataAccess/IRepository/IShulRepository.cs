using DataAccess.DBModels;
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

    }
}
