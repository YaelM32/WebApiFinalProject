using DataAccess.DBModels;
using DataAccess.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class ShulRepository : IShulRepository
    {
        BookDBContext dbContext;
        public ShulRepository(BookDBContext _dbContext)
        {
            dbContext = _dbContext;
        }

        public async Task<int> SignIn(Shul shul)//,string FileName)
        {
            try
            {
                //var saveFilePath = Path.Combine("c:\\savefilepath\\", uploadedFile.FileName);
                //Shul newShul = new()
                //{
                //    Id = shul.Id,
                //    Name = shul.Name,
                //    Address = shul.Address,
                //};

                //if (shul.Map != null)
                //{
                //    using (var memoryStream = new MemoryStream())
                //    {
                //        Stream stream = new MemoryStream(shul.Map);
                //        await stream.CopyToAsync(memoryStream);
                //        if (memoryStream.Length < 2097152)
                //        {
                //            newShul.Map = memoryStream.ToArray();
                //        }
                //        else
                //        {
                //            Console.WriteLine("File", "The file is too large.");
                //        }

                //    }
                //}
                //byte[] imageData = File.ReadAllBytes(FileName);
                //shul.Map = imageData;
                dbContext.Shuls.AddAsync(shul);
                await dbContext.SaveChangesAsync();
                return shul.Id;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in SignIn function " + ex.Message);
            }
        }
    }

}   

 