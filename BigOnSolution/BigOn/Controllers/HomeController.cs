using BigOn.AppCode.Extentions;
using BigOn.Models.DataContexts;
using BigOn.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace BigOn.Controllers
{
    public class HomeController : Controller
    {
        private readonly BigOnDbContext _db;

        public HomeController(BigOnDbContext db)
        {
            this._db = db;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
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
                string token = $"{subscribe.Id}-{subscribe.Email}".Encrypt(Extension.saltKey);

                string aproveLink = $"https://localhost:44372/subscribe-approve?token={token}";
                string fromEmail = "rafetmemmedov5551999@mail.ru";

                 
                SmtpClient smtpClient = new SmtpClient("smtp.mail.ru",25);
                smtpClient.Credentials = new NetworkCredential(fromEmail, "Bys0G2hxtgBgyyRmjmMV");
                smtpClient.EnableSsl = true;
                MailAddress from=new MailAddress(fromEmail,"Code Academy");
                MailAddress to =new MailAddress(Email);
                MailMessage mailMessage = new MailMessage(from,to);
                mailMessage.Subject = "Salammmm";
                mailMessage.Body = $"Zehmet olmasa linke basaraq tesdiqleyin <br/> <a href='{aproveLink}'>link</a>";
                mailMessage.IsBodyHtml= true;
                smtpClient.Send(mailMessage);
            }


            end:
            return Json(new
            {
                error = false,
                message = "Tesdiq linki epoct unvaniniza goderildi"
            });


        }

        public IActionResult SubscribeAprove(string token)
        {
            return View();
        }
    }
}
