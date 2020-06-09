using Natroral.Core.Models;
using Natroral.DataAccess.SQL;
using reCAPTCHA.MVC;
using System;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Natroral.WebUI.Controllers
{
    public class ContactController : Controller
    {
        DataContext context;

        public ContactController()
        {
            context = new DataContext();
        }

        [Authorize(Roles = "Admin")]
        // GET: Contact
        public ActionResult Index()
        {
            return View(context.Contacts.ToList());
        }

        public ActionResult Create()
        {
            Contact contact = new Contact();
            return View(contact);
        }

        [HttpPost]
        [CaptchaValidator]
        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> Create(Contact model, bool captchaValid)
        {
            if (captchaValid)
            {
                if (ModelState.IsValid)
                {
                    context.Contacts.Add(model);
                    await context.SaveChangesAsync();

                    //send email                                    
                    MailMessage msg = new MailMessage();
                    msg.From = new MailAddress(model.Email);
                    msg.To.Add(new MailAddress(ConfigurationManager.AppSettings["smtpEmail"].ToString()));
                    msg.Subject = model.Subject;

                    SmtpClient smtpClient = new SmtpClient(ConfigurationManager.AppSettings["smtpHost"].ToString(), Convert.ToInt32(ConfigurationManager.AppSettings["smtpPort"]));
                    System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["smtpEmail"].ToString(), ConfigurationManager.AppSettings["smtpPass"].ToString());
                    smtpClient.Credentials = credentials;
                    smtpClient.EnableSsl = true;

                    try
                    {
                        smtpClient.Send(msg);

                        ViewBag.Success = "Your message has been sent to Natroroal admin. We will contact you as soon as we can. Thank you!";
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Failed = "There is an error on Email Server connection. The contact message cannot be sent. " + ex.ToString();
                        ViewBag.Failed += "Please come back later.";
                    }

                    return View();
                }
                else
                {
                    return View(model);
                }
            }
            else
            {
                ModelState.AddModelError("_FORM", "Please try again.");
                return View(model);
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Delete(string Id)
        {
            var contactToDelete = context.Contacts.Single(a => a.Id == Id);

            if (contactToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(contactToDelete);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            var contactToDelete = context.Contacts.Single(a => a.Id == Id);

            if (contactToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                context.Contacts.Remove(contactToDelete);
                context.SaveChanges();

                return RedirectToAction("Index");
            }

        }


    }
}