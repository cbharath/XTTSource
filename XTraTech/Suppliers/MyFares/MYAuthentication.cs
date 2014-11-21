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
            this.createSession.AccountNumber = "MCN003953";
            this.createSession.UserName = "CarsolizeXML";
            this.createSession.Password = "CS2013_xml";
            this.createSession.Target = Target.Test;
            this.createSession.TargetSpecified = true;
            OnePoint onePoint = new OnePoint();
            this.mySession = onePoint.CreateSession(this.createSession);
            return this.mySession;
        }
    }
}
