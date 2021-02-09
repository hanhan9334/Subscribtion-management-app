using System;
using System.Collections.Generic;
using System.Text;

namespace Lab2
{
    class SendViaMobile
    {
        public static List<SendViaMobile> subscribedMobiles = new List<SendViaMobile>();
        public String Number
        {
            set;get;
        }

        public SendViaMobile() {; }
        public SendViaMobile(string number)
        {
            Number = number;
            
        }
        public void sendText(string msg)
        {
            System.Diagnostics.Debug.WriteLine("The message" + "\"" + msg + "\" has already sent to " + Number);
        }

        public void Subscribe(Publisher pub)
        {
            pub.publishmsg += sendText;
            subscribedMobiles.Add(this);
        }

        public void UnSubscribe(Publisher pub)
        {
            pub.publishmsg -= sendText;
            subscribedMobiles.Remove(this);
        }

        public static Boolean ifEmailSubscribed(string number)
        {
            Boolean result = false;
            foreach(var mobile in subscribedMobiles)
            {
                if (mobile.Number == number)
                {
                    result = true;
                }
            }

            return result;
        }
    }
}
