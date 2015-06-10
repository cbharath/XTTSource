using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace XTraTechWeb.Models
{
    public class Registration
    {
        public string CompanyName { get; set; }
        public string MemberOf { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZIP { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool usedefautSMTP { get; set; }
        public string SMTPAddress { get; set; }
        public string PortNumber { get; set; }
        public bool enableSSL { get; set; }
        public string FromEmail { get; set; }
        public string  EmailPwd { get; set; }
        public string CaptchaCode { get; set; }
    }
}