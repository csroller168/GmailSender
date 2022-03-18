using EmailSender;

namespace SendEmail
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            var toAddress = args[0];
            var fromAddress = args[1];
            var password = args[2];
            var emailer = new Emailer();
            emailer.SendEmail(toAddress, fromAddress, password);
        }
    }
}
