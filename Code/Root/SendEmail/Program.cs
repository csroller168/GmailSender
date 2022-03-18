using EmailSender;

namespace SendEmail
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            // TODOS
            // make a command line arg for number of times to send and seconds to wait in between
            var password = args[0];
            var emailer = new Emailer();
            emailer.SendEmail(password);
        }
    }
}
