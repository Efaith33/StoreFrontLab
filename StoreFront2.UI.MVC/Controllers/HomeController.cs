using StoreFront2.Models;
using System;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;

namespace StoreFront2.UI.MVC.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

       
        public ActionResult About()
        {
            ViewBag.Message = "About Space Labs Inc";

            return View();
        }


        public ActionResult Shop()
        {
            return View();
        }


        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Contact(ContactViewModel cvm)
        {
            if (!ModelState.IsValid)
            {
                return View(cvm);
            }
            string message = $"You have received an email from {cvm.Name} with a subject of {cvm.Subject}. Please respond to {cvm.Email} with your response to the following message: <br/>{cvm.Message}";

            MailMessage mm = new MailMessage("admin@eric-faith.net", "weibleyeric@yahoo.com", cvm.Subject, message);

            mm.IsBodyHtml = true;
            mm.Priority = MailPriority.High;
            mm.ReplyToList.Add(cvm.Email);


            SmtpClient client = new SmtpClient("mail.eric-faith.net");
            client.Port = 25; //alt port number is 8889
            client.EnableSsl = false;

            client.Credentials = new NetworkCredential("admin@eric-faith.net", "P@ssw0rd");

            try
            {
                client.Send(mm);
            }
            catch (Exception ex)
            {
                ViewBag.CustomerMessage = $"We're sorry your request could not be completed at this time. Please try again later. Error Message: {ex.Message}<br/>{ex.StackTrace}";
                return View(cvm);
            }

            return View("EmailConfirmation", cvm);

        }
    }
}
