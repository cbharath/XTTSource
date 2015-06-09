using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading;
using XTraTech.Models;
using XTraTech.Entities.COM;
using XTraTech.Helper;
using System.Text;

namespace XTraTech.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/

        public ActionResult Index()
        {
            //Testing Emails.
            //Email email = new Email();
            //email.emailTo = "chumbalekar.bharath@gmail.com";
            //email.body = "<h1>Testing bookEtickets.com mails</h1>";
            //email.subject = "Testing";
            //email.Send();

            Login login = new Login();
            return View(login);
        }

        [HttpPost]
        public ActionResult Index(Login model)
        {
            Staff staff = new Staff();
            Client client =null;
            List<Staff> staffs = new List<Staff>();

            try
            {
                if (model.Member)
                {
                    staff = staff.AuthnticateUser(model.UserName, model.Password);
                    if (staff != null)
                    {
                        client = new Client(); 
                        client.Load(staff.ClientID, false);
                        client.Staffs = new List<Staff>();
                        client.Staffs.Add(staff);
                        Session["LoggedInUser"] = client;
                        return base.RedirectToAction("Index", "Search");
                    }
                    else
                    {

                        return View();
                    }
                }
                else
                {
                    client = new Client();
                    //Create User and then login.
                    client.CompanyName = model.CompanyName;
                    client.Country = model.Country;
                    client.Email = model.UserName;
                    client.FirstName = model.FirstName;
                    client.LastName = model.LastName;

                    client.UseDefaultSMTP = true;
                    client.EnableSSL = false;

                    staff.Email = model.UserName;
                    staff.FirstName = model.FirstName;
                    staff.LastName = model.LastName;
                    staff.Password = model.Password;
                    staff.UserName = model.UserName;
                    staffs.Add(staff);
                    client.Staffs = staffs;

                    client.Save();

                    //Email Here.
                    Email email = new Email();
                    email.emailTo = model.UserName;
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
                    Session["LoggedInUser"] = client;
                    return base.RedirectToAction("Index", "Search");
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return base.RedirectToAction("Index", "Login");
        }
    }
}
