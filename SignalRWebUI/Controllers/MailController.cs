using Microsoft.AspNetCore.Mvc;
using MimeKit;
using SignalRWebUI.Dtos.MailDtos;
using System.Net.Mail;
using MailKit.Net.Smtp;
using MailKit.Security;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace SignalRWebUI.Controllers
{
    public class MailController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(CreateMailDto createMailDto)
        {
            MimeMessage mimeMessage = new MimeMessage();
            MailboxAddress mailboxAddressFrom = new MailboxAddress("SignalR Rezervasyon", "randomemory12@gmail.com");
            mimeMessage.From.Add(mailboxAddressFrom);
            MailboxAddress mailboxAddressTo = new MailboxAddress("User", createMailDto.ReceiverMail);
            mimeMessage.To.Add(mailboxAddressTo);
            var bodyBuilder = new BodyBuilder();
            bodyBuilder.TextBody = createMailDto.Body;
            mimeMessage.Body = bodyBuilder.ToMessageBody();
            mimeMessage.Subject = createMailDto.Subject;
            SmtpClient client = new SmtpClient();
            
                client.Connect("smtp.gmail.com", 587, false);
                client.Authenticate("randomemory12@gmail.com", "tvcn hjee qdwc craa");
                client.Send(mimeMessage);
                client.Disconnect(true);
            

            return RedirectToAction("Index", "Category");
        }
    }
}
