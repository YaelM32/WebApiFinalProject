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


using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.Services;
using System.IO;
using System.Threading;


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
        public async Task getEmail(string email)
        {
            string body = "";
            try
            {
                User u = await dbContext.Users.Where(u => u.Email == email).FirstOrDefaultAsync();
                if (u != null)
                    body = "Sending random password";
                else
                    body = "Sending link for signIn";

                //// Configure the SMTP client
                //SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
                //    smtpClient.Credentials = new NetworkCredential("ProductLocation@gmail.com", "C&&Y7429");
                //    smtpClient.EnableSsl = true;
                //    // Create the email message
                //    MailMessage message = new MailMessage();

                //    message.From = new MailAddress("36325565166@mby.co.il");
                //    message.Subject = "Your subject here";
                //    message.Body = body;
                //    message.To.Add(new MailAddress("36325565166@mby.co.il"));

                //    // Send the email
                //    smtpClient.Send(message);


                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                mail.From = new MailAddress("ProductLocation1@gmail.com");
                mail.To.Add(email);
                mail.Subject = "קבצי חודש אחרון ";
                mail.Body = "קובץ מידע על הקבצים שהורדו : information" + " \n" +
                    "קבצי אישורי מחלה שנשלחו בחודש האחרון";

                System.Net.Mail.Attachment attachment;

                //foreach (var file in Directory.GetFiles(path))
                //{
                //    attachment = new System.Net.Mail.Attachment(file);
                //    mail.Attachments.Add(attachment);
                //}

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("ProductLocation1@gmail.com", "C&&Y7429");
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);


            }
            catch (Exception ex)
            {
                throw new Exception("Error in getEmail function " + ex.Message);
            }
        }


        //public static void getEmail(string email)
        //    {
        //        UserCredential credential;

        //        // Load the OAuth 2.0 credentials from a file or other secure source
        //        using (var stream = new FileStream("path/to/credentials.json", FileMode.Open, FileAccess.Read))
        //        {
        //            credential = GoogleCredential.FromStream(stream)
        //                .CreateScoped(GmailService.Scope.GmailSend)
        //                .UnderlyingCredential as UserCredential;
        //        }

        //        // Create the Gmail API service using the OAuth 2.0 credentials
        //        var service = new GmailService(new BaseClientService.Initializer()
        //        {
        //            HttpClientInitializer = credential,
        //            ApplicationName = "Your Application Name",
        //        });

        //        // Construct the message
        //        var message = new Message();
        //        var body = new StringBuilder();
        //        body.AppendLine("Hello,");
        //        body.AppendLine("");
        //        body.AppendLine("This is a test message sent from my .NET Core application.");
        //        message.Raw = Base64UrlEncode(Encoding.UTF8.GetBytes(body.ToString()));
        //        message.ThreadId = "";

        //        // Send the message
        //        service.Users.Messages.Send(message, "me").Execute();
        //    }

        //    private static string Base64UrlEncode(byte[] input)
        //    {
        //        return Convert.ToBase64String(input)
        //            .Replace("+", "-")
        //            .Replace("/", "_")
        //            .Replace("=", "");
        //    }
        //}
    }
}
