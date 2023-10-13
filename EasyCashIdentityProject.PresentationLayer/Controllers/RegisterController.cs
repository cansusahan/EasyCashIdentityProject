using EasyCashIdentityProject.DtoLayer.Dtos.AppUserDtos;
using EasyCashIdentityProject.EntityLayer.Concrete;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using System.Net.Mail;

namespace EasyCashIdentityProject.PresentationLayer.Controllers
{
    public class RegisterController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public RegisterController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        [HttpPost]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(AppUserRegisterDto appUserRegisterDto)
        {
            if(ModelState.IsValid)
            {
                Random random=new Random();
                int code;
                code = random.Next(100000, 1000000);

				AppUser appUser = new AppUser()
                {
                    UserName = appUserRegisterDto.Username,
                    Name = appUserRegisterDto.Name,
                    Surname = appUserRegisterDto.Surname,
                    Email = appUserRegisterDto.Email,
                    City="aaaa",
                    District="bbb",
                    ImageUrl="cccc",
                    ComfirmCode=code

                };
                var result = await _userManager.CreateAsync(appUser, appUserRegisterDto.Password);
                if(result.Succeeded)
                {
                    //mail adresine kod gpnderme alanı
                    MimeMessage mimeMessage = new MimeMessage();
                    MailboxAddress mailboxAddressFrom = new MailboxAddress("Easy Cash Admin", "cnsusahan07@gmail.com");
                    MailboxAddress mailboxAddressTo = new MailboxAddress("User", appUser.Email);
                   
                    mimeMessage.From.Add(mailboxAddressFrom);
                    mimeMessage.To.Add(mailboxAddressTo);

                    var bodyBuilder = new BodyBuilder();
                    bodyBuilder.TextBody = "Kayıt işlemini gercekleştiren için onay kodunuz:" + code;
                    mimeMessage.Body=bodyBuilder.ToMessageBody();

                    mimeMessage.Subject = "Easy Cash Onay Kodu";


                    MailKit.Net.Smtp.SmtpClient client=new MailKit.Net.Smtp.SmtpClient();
                    client.Connect("smpt.gmail.com", 587, false);
                    client.Authenticate("cnsusahan07@gmail.com", "jvkk ffqf zhrn osra");
                    client.Send(mimeMessage);
                    client.Disconnect(true);

                    TempData["Mail"]=appUserRegisterDto.Email;
                    //
                    

                    return RedirectToAction("Index", "ConfirmMail");

                }
                else
                {
                    foreach(var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }
            return View();
        }
    }
}
