using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;
namespace XTraTech.Entities.API
{
    [DataContract]
    public class Message
    {
        [DataMember]
        public string Details
        {
            get;
            set;
        }
        [DataMember, XmlAttribute("Type")]
        public string Type
        {
            get;
            set;
        }
        [DataMember, XmlAttribute("Code")]
        public string Code
        {
            get;
            set;
        }
    }
}
