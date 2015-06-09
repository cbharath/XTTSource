using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XTraTechWeb.Models
{
    public class Login
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }
        public string Country { get; set; }
        public string ConfirmPassward { get; set; }
        public bool Member { get; set; }
    }
}