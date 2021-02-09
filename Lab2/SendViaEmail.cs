using System;
using System.Collections.Generic;
using System.Text;

namespace Lab2
{
    class SendViaEmail
    {
        public static List<SendViaEmail> subscribedEmails = new List<SendViaEmail>();
        public String EmailAddress
        {
            set;get;
        }

        public SendViaEmail() {; }
        public SendViaEmail(string email)
        {
            EmailAddress = email;
            
        }
        public void sendEmail(string msg)
        {
            System.Diagnostics.Debug.WriteLine("The message" + "\"" + msg + "\" has already sent to " + EmailAddress);
        }

        public void Subscribe(Publisher pub)
        {
            pub.publishmsg += sendEmail;
            subscribedEmails.Add(this);
        }

        public void UnSubscribe(Publisher pub)
        {
            pub.publishmsg -= sendEmail;
            subscribedEmails.Remove(this);
        }

        public static Boolean ifEmailSubscribed(string emailAddress)
        {
            Boolean result = false;
            foreach (var email in subscribedEmails)
            {
                if (emailAddress == email.EmailAddress)
                {
                    result = true;
                }
            }

            return result;
        }
    }
}
