using System;
using EmailSender;

namespace SendEmail
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            var emailer = new Emailer();
            emailer.SendEmail();
        }
    }
}
