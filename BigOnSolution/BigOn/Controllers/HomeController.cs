using BigOn.Domain.AppCode.Extentions;
using BigOn.Domain.Models.DataContexts;
using BigOn.Domain.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System;
using BigOn.Domain.AppCode.Services;
using BigOn.WebUI.AppCode.Services;
using MediatR;
using BigOn.Domain.Business.FaqModule;

namespace BigOn.Controllers
{
    public class HomeController : Controller
    {
        private readonly BigOnDbContext _db;
        private readonly CryiptoService cryipto;
        private readonly EmailService emailService;
        private readonly IMediator mediator;

        public HomeController(BigOnDbContext db,CryiptoService cryipto,EmailService emailService,IMediator mediator)
        {
            this._db = db;
            this.cryipto = cryipto;
            this.emailService = emailService;
            this.mediator = mediator;
        }
        public IActionResult Index()
        {
            return View();
        }
        [Route("/about")]
        public IActionResult About()
        {
            return View();
        }
        [Route("/faqs")]

        public async Task<IActionResult> Faq(FaqsAllQuery query)
        {
            var data = await mediator.Send(query);
            return View(data);
        }

        public IActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Contact(ContactPost model)
        {
            _db.ContactPosts.Add(model);
            _db.SaveChanges();
            TempData["Message"] = "Sorgunuz qebul edildi";
            return RedirectToAction(nameof(Contact));
        }


        [HttpPost]

        public async Task<IActionResult> Subscribe(string Email)
        {
            var subscribe=await _db.Subscribers.FirstOrDefaultAsync(m=>m.Email.Equals(Email));
            if (subscribe != null && subscribe.AprovedDate !=null)
            {
                return Json(new
                {
                    error = false,
                    message = "Siz qeydiyatdan kecmisiz"
                });
            }
            else if(subscribe!= null)
            {
                goto end;
            }
            else
            {
                subscribe=new Subscribe();
                subscribe.Email= Email;
                await _db.Subscribers.AddAsync(subscribe);
                await _db.SaveChangesAsync();
                //string token = $"{subscribe.Id}-{subscribe.Email}-{DateTime.Now:yyyyMMddHHss}".Encrypt(Extension.saltKey,true);
                string token = cryipto.Encrypt($"{subscribe.Id}-{subscribe.Email}",true);

                string aproveLink = $"https://{Request.Host}/subscribe-approve?token={token}";
                //7string fromEmail = "rafetmemmedov5551999@mail.ru";


                //SmtpClient smtpClient = new SmtpClient("smtp.mail.ru",25);
                //smtpClient.Credentials = new NetworkCredential(fromEmail, "Bys0G2hxtgBgyyRmjmMV");
                //smtpClient.EnableSsl = true;
                //MailAddress from=new MailAddress(fromEmail,"Code Academy");
                //MailAddress to =new MailAddress(Email);
                //MailMessage mailMessage = new MailMessage(from,to);
                //mailMessage.Subject = "Salammmm";
                //mailMessage.Body = $"Zehmet olmasa linke basaraq tesdiqleyin <br/> <a href='{aproveLink}'>link</a>";
                string message= $"Zehmet olmasa linke basaraq tesdiqleyin <br/> <a href='{aproveLink}'>link</a>";
                //mailMessage.IsBodyHtml= true;
                //smtpClient.Send(mailMessage);

                await emailService.SendEmailAsync(Email, "Salam", message);
            }


            end:
            return Json(new
            {
                error = false,
                message = "Tesdiq linki epoct unvaniniza goderildi"
            });


        }


        [Route("/subscribe-approve")]
        public async Task<IActionResult> SubscribeAprove(string token)
        {
            token=cryipto.Decrypt(token);
            var match = Regex.Match(token, @"^(?<id>\d+)-(?<email>.+)$");
            if (!match.Success)
            {
                return Content("token zedelidir");
            }

            int id = Convert.ToInt32(match.Groups["id"].Value);
            string email= match.Groups["string"].Value;
            string expireDate = match.Groups["expireDate"].Value;

            var subscribe = await _db.Subscribers.FirstOrDefaultAsync(m => m.Id==id);

            if (subscribe == null)
            {
                return Content("tapilmadi");
            }
            else if(subscribe.Email == email)
            {
                return Content("token size aid deyil");
            }
            else if (subscribe.AprovedDate != null)
            {
                return Content("emailiniz artiq tesdiqlenib");
            }

            subscribe.AprovedDate= DateTime.Now;
            _db.SaveChanges();
            return Content("ugurludur");
        }
    }
}
