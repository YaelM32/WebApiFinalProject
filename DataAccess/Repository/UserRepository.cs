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
//using System.Net.WebUtility.UrlEncode;

using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.Services;
using System.IO;
using System.Threading;
using Org.BouncyCastle.Cms;
using static System.Net.WebRequestMethods;
using System.Text.Encodings.Web;

namespace DataAccess.Repository
{
    public class UserRepository : IUserRepository
    {
        BookDBContext dbContext;
        public UserRepository(BookDBContext _dbContext)
        {
            dbContext = _dbContext;
        }

        public async Task<User> checkUserExist(string email, string password)
        {
            try
            {
                User u1 = await dbContext.Users.Where(u => u.Email == email && u.Password == password).FirstOrDefaultAsync();
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
                User u = await dbContext.Users.Where(u => u.Name == user.Name).FirstOrDefaultAsync();
                if (u != null && u.Name == user.Name)
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

        public async Task ChangePassword(string email, string Password)
        {
            try
            {
                email = Base64Decode(email);
                User u = await dbContext.Users.Where(u => u.Email == email).FirstOrDefaultAsync();
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

        public static string Base64Encode(string text)
        {
            var textBytes = System.Text.Encoding.UTF8.GetBytes(text);
            return System.Convert.ToBase64String(textBytes);
        }
        public static string Base64Decode(string base64)
        {
            var base64Bytes = System.Convert.FromBase64String(base64);
            return System.Text.Encoding.UTF8.GetString(base64Bytes);
        }
        public async Task getEmail(string email)
        {
            using (SmtpClient client = new SmtpClient()
            {
                Host = "smtp.office365.com",
                Port = 587,
                UseDefaultCredentials = false, // This require to be before setting Credentials property
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential("36325565166@mby.co.il", "Student@264"), // you must give a full email address for authentication 
                TargetName = "STARTTLS/smtp.office365.com", // Set to avoid MustIssueStartTlsFirst exception
                EnableSsl = true // Set to avoid secure connection exception
            })
            {
                var url = $"http://localhost:3000/newPassword/${Base64Encode(email)}";
                var link = $"<a href='{url}'>Click here</a>";
                MailMessage message = new MailMessage()
                {
                    From = new MailAddress("36325565166@mby.co.il"), // sender must be a full email address
                    Subject = "איפוס סיסמא",
                    IsBodyHtml = true,

                    Body = "<html><body dir=\"rtl\"><h1>איפוס סיסמא למערכת תרומות ספרים</h1><p>מייל זה נשלח לך עבור איפוס סיסמא, נא ללחוץ על הקישור המצורף</p>" +
                    $"<a href='{url}'>לחץ כאן לאיפוס סיסמא</a></body></html>",

                    // 
                    BodyEncoding = System.Text.Encoding.UTF8,
                    SubjectEncoding = System.Text.Encoding.UTF8,

                };

                message.To.Add(email);

                try
                {
                    client.Send(message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
