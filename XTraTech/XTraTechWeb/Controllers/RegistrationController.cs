using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using XTraTech.Entities.COM;
using XTraTech.Helper;
using XTraTechWeb.Models;

namespace XTraTechWeb.Controllers
{
    public class RegistrationController : Controller
    {
        //
        // GET: /Registration/

        public ActionResult Index(string ClientId)
        {
            if (Session["LoggedInUser"] != null)
            {
                Client client = new Client();
                client.Load(Convert.ToInt16(ClientId), true);
                Registration reg = new Registration();
                
                return View(reg);
            }
            else
            {
                return base.RedirectToAction("Index", "Login");
            }
        }

        //
        // POST: /Registration/

        [HttpPost]
        public ActionResult Index(Registration model)
        {
            Client client = new Client();
            List<Staff> staffs = new List<Staff>();
            Staff staff = new Staff();

            try
            {
                if (Session["LoggedInUser"] != null)
                {
                    client.Address1 = model.Address1;
                    client.Address2 = model.Address2;
                    client.City = model.City;
                    client.CompanyName = model.CompanyName;
                    client.Country = model.Country;
                    client.Email = model.Email;
                    client.FirstName = model.FirstName;
                    client.LastName = model.LastName;
                    client.MemberOf = model.MemberOf;
                    client.PhoneNumber = model.PhoneNumber;
                    client.State = model.State;
                    client.ZIP = model.ZIP;

                    client.UseDefaultSMTP = model.usedefautSMTP;
                    client.SMTPAddress = model.SMTPAddress;
                    client.PortNumber = model.PortNumber;
                    client.EnableSSL = model.enableSSL;
                    client.FromEmail = model.FromEmail;
                    client.EmailPassword = model.EmailPwd;

                    staff.Email = model.Email;
                    staff.FirstName = model.FirstName;
                    staff.LastName = model.LastName;
                    staff.Password = model.Password;
                    staff.PhoneNumber = model.PhoneNumber;
                    staff.UserName = model.UserName;
                    staffs.Add(staff);
                    client.Staffs = staffs;

                    client.Save();

                    //Email Here.
                    Email email = new Email();
                    email.emailTo = model.Email;
                    email.RunInBackground = true;
                    email.subject = "Account Registration Success";

                    StringBuilder sb = new StringBuilder();
                    sb.Append("<h1> Hi, " + model.LastName + " " + model.FirstName + "</h1>");
                    sb.Append("<p>Your Registration is success and now you can use the worlds most powerfull reservation engine</p>");
                    sb.Append("<br/><br/>");
                    sb.Append("<h3>Your login details are here:</h3>");
                    sb.Append("<p>UserName: " + model.UserName + "</p>");
                    sb.Append("<p>Password: " + model.Password + "</p>");

                    email.body = sb.ToString();
                    email.Send();
                }
                else
                {
                    return base.RedirectToAction("Index", "Login");
                }
            }
            catch (Exception)
            {
                return RedirectToAction("RegistrationSuccess", new { Success = false });
            }
            return RedirectToAction("RegistrationSuccess", new { Success = true });
        }

        public ActionResult RegistrationSuccess(bool Success)
        {
            ViewBag.Success = Success;
            return View();
        }
    }
}