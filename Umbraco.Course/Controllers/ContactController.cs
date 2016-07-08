
using System.Net.Mail;
using Umbraco.Course.Level2;

namespace Umbraco.Course.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using Umbraco.Web.Mvc;
    using Umbraco.Course.Models;


    public class ContactController : SurfaceController
    {

        [HttpPost]
        public ActionResult Submit(ContactModel model)
        {
            if (!ModelState.IsValid)
            {
                return CurrentUmbracoPage();
            }

            SendEmail(model);

            return RedirectToCurrentUmbracoPage();
        }

        private void SendEmail(ContactModel model)
        {
            var message = new MailMessage()
            {
                Subject = "New Contact Request",
                From = new MailAddress(model.Email, model.Name),
                Body = model.Message
            };
            message.To.Add("siteadmin@domain.com");
            var smtp = new SmtpClient();
            smtp.SetPickupDirectoryLocation();
            smtp.Send(message);
            TempData["success"] = true;
        }
    }
}