using DataAccess.DBModels;
using DataAccess.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using Image = DataAccess.DBModels.Image;

namespace DataAccess.Repository
{
    public class ShulRepository : IShulRepository
    {
        BookDBContext dbContext;
        public ShulRepository(BookDBContext _dbContext)
        {
            dbContext = _dbContext;
        }

        public async Task<Shul> GetShulById(int shulId)
        {
           return await dbContext.Shuls.FindAsync(shulId);
        }

        public async Task SetMap(int shulId, string fileName)
        {
            try
            {
                Shul s =  dbContext.Shuls.Find(shulId);
                if (s != null)
                {
                s.Map = fileName;
                dbContext.Update(s);
                await dbContext.SaveChangesAsync();
                }

            }
            
            catch (Exception ex)
            {
                throw new Exception("Error in SetMap function " + ex.Message);
            }
        }
        public async Task SetLogo(int shulId, string fileName)
        {
            try
            {
                Shul s = dbContext.Shuls.Find(shulId);
                if (s != null)
                {
                    s.Logo = fileName;
                    dbContext.Shuls.Update(s);
                    await dbContext.SaveChangesAsync();
                }

            }

            catch (Exception ex)
            {
                throw new Exception("Error in SetMap function " + ex.Message);
            }
        }

        public async Task<int> SignIn(Shul shul)//,string FileName)
        {
            try
            {
                dbContext.Shuls.AddAsync(shul);
                await dbContext.SaveChangesAsync();
                return shul.Id;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in SignIn function " + ex.Message);
            }
        }

        public async Task UploadFile(int shulId, IFormFile userfile)
        {
            try
            {
                Image image = new Image() { Filepath = "images/" + userfile.FileName, Filename = userfile.FileName };
                await dbContext.AddAsync(image);
                await dbContext.SaveChangesAsync();
            }

            catch (Exception ex)
            {
                throw new Exception("Error in SetMap function " + ex.Message);
            }
        }
    }

}

