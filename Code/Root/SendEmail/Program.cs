using EmailSender;

namespace SendEmail
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            var numIterations = int.Parse(args[0]);
            var numSecondsBetweenIterations = int.Parse(args[1]);
            var password = args[2];
            var emailer = new Emailer();
            emailer.SendEmail(
                numIterations,
                numSecondsBetweenIterations,
                password);
        }
    }
}
