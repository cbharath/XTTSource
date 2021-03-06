﻿using System;
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
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            Login login = new Login();
            login.Member = true;
            return View(login);
        }

        [HttpPost]
        public ActionResult Index(Login model)
        {
            Staff staff = new Staff();
            Client client = null;
            List<Staff> staffs = new List<Staff>();

            try
            {
                model.Member = true;
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
                return base.RedirectToAction("ErrorPage", "Comman");
            }

        }
    }
}