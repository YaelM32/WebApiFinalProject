using DataAccess.DBModels;
using DataAccess.IRepository;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class UserRepository: IUserRepository
    { 
        BookDBContext dbContext;
        public UserRepository(BookDBContext _dbContext)
        {
            dbContext = _dbContext;
        }

        public async Task<User> checkUserExist(string name, string password)
        {
            try
            {
                User u1 = await dbContext.Users.Where(u => u.Name == name && u.Password == password).FirstOrDefaultAsync();
                return u1 == null ? null : u1;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in checkUserExist function " + ex.Message);
            }

        }

        public async Task SignIn(User user)
        {
            try
            {
                User u = await dbContext.Users.Where(u=>u.Name == user.Name).FirstOrDefaultAsync();
                if (u!=null && u.Name==user.Name)
                {
                    Console.WriteLine("The name is exist");
                }
                else
                {
                dbContext.Users.AddAsync(user);
                await dbContext.SaveChangesAsync();
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error in SignIn function " + ex.Message);
            }
        }

        public async Task ChangePassword(string name, string Password)
        {
            try
            {
                User u = await dbContext.Users.Where(u => u.Name == name).FirstOrDefaultAsync();
                if (u != null)
                {
                    u.Password = Password;
                    dbContext.Users.Update(u);
                    await dbContext.SaveChangesAsync();
                }
                else
                {
                    Console.WriteLine("The user isn't exist");
                }
                
            }
            catch (Exception ex)
            {
                throw new Exception("Error in SignIn function " + ex.Message);
            }
        }
        public async Task<string> getEmail(string name)
        {
            try
            {
                User u = await dbContext.Users.Where(u => u.Name == name).FirstOrDefaultAsync();
                if (u != null)
                {
                    // Configure the SMTP client
                    SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
                    smtpClient.Credentials = new NetworkCredential("36325565166@mby.co.il", "Student@264");
                    smtpClient.EnableSsl = true;
                    // Create the email message
                    MailMessage message = new MailMessage();

                    message.From = new MailAddress("36325565166@mby.co.il");
                    message.Subject = "Your subject here";
                    message.Body = "Your message here";
                    message.To.Add(new MailAddress("36325565166@mby.co.il"));

                    // Send the email
                    smtpClient.Send(message);

                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in getEmail function " + ex.Message);
            }
        }
    }
}
