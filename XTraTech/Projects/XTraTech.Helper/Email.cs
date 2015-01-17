using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Mail;
using System.Threading;

namespace XTraTech.Helper
{
    public class Email
    {
        private string smtpAddress = string.Empty;
        private int portNumber;
        private bool enableSSL;

        private string emailFrom = string.Empty;
        private string password = string.Empty;

        public string emailTo = string.Empty;
        public string subject = string.Empty;
        public string body = string.Empty;
        public bool RunInBackground { get; set; }
        public Email()
        {
            smtpAddress = "mail.booketickets.com";
            portNumber = 587;
            enableSSL = false;
            emailFrom = "support@booketickets.com";
            password = "Sup@8Tix";
            //smtpAddress = "smtp.gmail.com";
            //portNumber = 587;
            //enableSSL = true;
            //emailFrom = "chumbalekar.bharath@gmail.com";
            //password = "mssuma06";
        }

        public Email(string SmtpAddress, int PortNumber, bool EnableSSL, string FromAddress, string FromPassword)
        {
            smtpAddress = SmtpAddress;
            portNumber = PortNumber;
            enableSSL = EnableSSL;
            emailFrom = FromAddress;
            password = FromPassword;
        }

        public void Send()
        {
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(emailFrom);
                mail.To.Add(emailTo);
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true;

                // Can set to false, if you are sending pure text.

                //mail.Attachments.Add(new Attachment("C:\\SomeFile.txt"));
                //mail.Attachments.Add(new Attachment("C:\\SomeZip.zip"));

                using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                {
                    smtp.Credentials = new NetworkCredential(emailFrom, password);
                    smtp.EnableSsl = enableSSL;
                    smtp.Send(mail);

                    //Thread thread = new Thread(() => smtp.Send(mail));
                    //if (RunInBackground)
                    //    thread.IsBackground = true;
                    //else
                    //    thread.IsBackground = false;
                    //thread.Start();
                }
            }
        }
    }
}
