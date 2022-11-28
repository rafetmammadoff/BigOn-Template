using BigOn.Models.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.Net.Mail;
using System.Net;

namespace BigOn
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //string Email = "rafatam@code.edu.az";
            //string aproveLink = $"https://localhost:44372/subscribe-approve?token=1";
            //string fromEmail = "rafetmemmedov5551999@mail.ru";
            //SmtpClient smtpClient = new SmtpClient("smtp.mail.ru", 25);
            //smtpClient.Credentials = new NetworkCredential(fromEmail, "Bys0G2hxtgBgyyRmjmMV");
            //smtpClient.EnableSsl = true;
            //MailAddress from = new MailAddress(fromEmail, "Code Academy");
            //MailAddress to = new MailAddress(Email);
            //MailMessage mailMessage = new MailMessage(from, to);
            //mailMessage.Subject = "Salammmm";
            //mailMessage.Body = $"Zehmet olmasa linke basaraq tesdiqleyin <br/> <a href='{aproveLink}'>link</a>";
            //mailMessage.IsBodyHtml = true;
            //smtpClient.Send(mailMessage);
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
