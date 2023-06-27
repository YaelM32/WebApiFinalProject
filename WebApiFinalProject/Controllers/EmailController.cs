using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;
using BusinessLogic.DTO;
using DataAccess.DBModels;

namespace WebApiFinalProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {

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
        public static async Task sendEmailForChangePwd(string email)
        {
            var url = $"http://localhost:3000/newPassword/${Base64Encode(email)}";
            var subject = "איפוס סיסמא";
            var body = "<html><body dir=\"rtl\"><h1>איפוס סיסמא למערכת תרומות ספרים</h1><p>מייל זה נשלח לך עבור איפוס סיסמא, נא ללחוץ על הקישור המצורף</p>" +
                    $"<a href='{url}'>לחץ כאן לאיפוס סיסמא</a></body></html>";
            sendEmail(email, body, subject);
        }
        public static async Task sendReceiptEmail(User user, List<BookDTO> books)
        {
            string myMap = "";
            books.ForEach((b) => myMap += $"<li>{b.Name}</li>");
            var body = $"<html><body dir=\"rtl\"><h1>תודה על תרומתך!!</h1><p>לכבוד הרב הגאון {user.Name} שליטא</p><ul>{myMap}</ul></body></html>";
            sendEmail(user.Email, body, "תרומתך התקבלה למערכת");
        }

        public static async Task sendEmail(string email, string body, string subject)
        {
            using (SmtpClient client = new SmtpClient()
            {
                Host = "smtp.office365.com",
                Port = 587,
                UseDefaultCredentials = false, // This require to be before setting Credentials property
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential("DonateBooks@outlook.co.il", "Y&&C7429"), // you must give a full email address for authentication 
                TargetName = "STARTTLS/smtp.office365.com", // Set to avoid MustIssueStartTlsFirst exception
                EnableSsl = true // Set to avoid secure connection exception
            })
            {
                MailMessage message = new MailMessage()
                {
                    From = new MailAddress("DonateBooks@outlook.co.il"), // sender must be a full email address
                    Subject = subject,
                    IsBodyHtml = true,

                    Body = body,

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