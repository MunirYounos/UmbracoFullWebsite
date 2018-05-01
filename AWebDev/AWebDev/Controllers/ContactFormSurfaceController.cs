using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AWebDev.ViewModels;
using Umbraco.Web.Mvc;
using System.Net.Mail;
using Umbraco.Core.Models;


namespace AWebDev.Controllers
{
    public class ContactFormSurfaceController : SurfaceController
    {
        // GET: ContactFormSurface
        public ActionResult Index()
        {
            return PartialView("ContactFormPartial", new ContactForm());
        }

        [HttpPost]
        public ActionResult HandleFormSubmit(ContactForm model)
        {

            if (!ModelState.IsValid)
            {
                return CurrentUmbracoPage();
            }
            else
            {
                //send message
                MailMessage message = new MailMessage();
                message.To.Add("neeewbi@gmail.com");
                message.Subject = model.Subject;
                message.From = new MailAddress(model.Email, model.Name);
                message.Body = model.Message;


                //Messages 
                IContent comment = Services.ContentService.CreateContent(model.Subject, CurrentPage.Id, "message");
                // assign values
                comment.SetValue("messagename", model.Name);
                comment.SetValue("email", model.Email);
                comment.SetValue("subject", model.Subject);
                comment.SetValue("messagecontent", model.Message);
                // save to Umbraco

                Services.ContentService.Save(comment);
                // Services.ContentService.SaveAndPublish(comment);



                using (SmtpClient smtp = new SmtpClient())
                {
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.UseDefaultCredentials = false;
                    smtp.EnableSsl = true;
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.Credentials = new System.Net.NetworkCredential("neeewbi@gmail.com", "2NeeebiNeeebi");
                    smtp.EnableSsl = true;
                    // send mail
                    smtp.Send(message);

                    TempData["success"] = true;
                    return RedirectToCurrentUmbracoPage();

                }

            }
        }

    }
}