using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XTraTech.Entities.COM;

namespace XTraTechWeb.Models
{
    public class clientmodel
    {
        public List<Client> ListofClients { get; set; }
        public Client Client { get; set; }
    }
}