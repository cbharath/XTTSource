using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyFares
{
    public class MYAuthentication
    {
        SessionCreateRQ createSession = new SessionCreateRQ();
        SessionCreateRS mySession = new SessionCreateRS();

        public SessionCreateRS CreateSession()
        {
            //this.createSession.AccountNumber = "MCN003953";
            //this.createSession.UserName = "CarsolizeXML";
            //this.createSession.Password = "CS2013_xml";
            //this.createSession.Target = Target.Test;
            //this.createSession.TargetSpecified = true;

            //this.createSession.AccountNumber = "MCN006795";
            //this.createSession.UserName = "SkiddooXML";
            //this.createSession.Password = "SO2014_xml";
            //this.createSession.Target = Target.Test;
            //this.createSession.TargetSpecified = true;

            this.createSession.AccountNumber = "MCN005983";
            this.createSession.UserName = "BookingHubXML";
            this.createSession.Password = "BH2014_xml";
            this.createSession.Target = Target.Test;
            this.createSession.TargetSpecified = true;

            OnePoint onePoint = new OnePoint();
            this.mySession = onePoint.CreateSession(this.createSession);
            return this.mySession;
        }
    }
}
