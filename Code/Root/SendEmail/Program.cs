using EmailSender;

namespace SendEmail
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            // TODOS
            // make a file for email body
            // make a config file for to/from address
            // make a command line arg for number of times to send and seconds to wait in between
            var toAddress = args[0];
            var fromAddress = args[1];
            var password = args[2];
            var emailer = new Emailer();
            emailer.SendEmail(toAddress, fromAddress, password);
        }
    }
}
