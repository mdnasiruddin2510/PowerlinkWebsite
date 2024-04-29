using PosWebsite.Models;
using System.Net.Mail;
namespace PosWebsite.Helper
{
    public class EmailService
    {
        public async Task<int> SendMailAsync(string phoneNumber, string otp, AppDbContext db,string companyId)
        {
            string senderMail = "preciousmart851@gmail.com";
            try
            {
                MailMessage message = new();
                SmtpClient smtp = new();
                message.From = new MailAddress(address: senderMail);
                message.To.Add(new MailAddress(address: phoneNumber));
                message.Subject = "OTP";
                message.IsBodyHtml = true;
                message.Body = otp;
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential(senderMail, "ouhc jllf hvpm rrcq");
                smtp.Send(message);
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
    }
}
