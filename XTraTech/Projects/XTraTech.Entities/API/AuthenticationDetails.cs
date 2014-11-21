using System;
using System.Runtime.Serialization;
namespace XTraTech.Entities.API
{
    [DataContract]
    public class AuthenticationDetails
    {
        [DataMember]
        public string UserName
        {
            get;
            set;
        }
        [DataMember]
        public string Password
        {
            get;
            set;
        }
        [DataMember]
        public string UniqueId
        {
            get;
            set;
        }
    }
}

