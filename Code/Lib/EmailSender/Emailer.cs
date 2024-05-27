using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;

namespace EmailSender
{
    public interface IEmailer
    {
        void SendEmail();
    }
    public class Emailer : IEmailer
    {
        private readonly string _toAddress = "fake@fake.com";
        private readonly string _fromAddress = "fake@fake.com";
        private readonly string _bodyFilePath = "fake@fake.com";
        private readonly string _subject = "fake@fake.com";
        private readonly int _numIterations = 0;
        private readonly int _numSecondsBetweenIterations = 0;
        private string _fromPassword = "fake";

        public Emailer(IConfiguration config)
        {
            _toAddress = config["EmailerOptions:ToAddress"] ?? string.Empty;
            _fromAddress = config["EmailerOptions:FromAddress"] ?? string.Empty;
            _bodyFilePath = config["EmailerOptions:BodyFilePath"] ?? string.Empty;
            _subject = config["EmailerOptions:Subject"] ?? string.Empty;
            _numIterations = int.Parse(config["EmailerOptions:NumIterations"] ?? string.Empty);
            _numSecondsBetweenIterations = int.Parse(config["EmailerOptions:NumSecondsBetweenIterations"] ?? string.Empty);
            _fromPassword = config["EmailerOptions:FromPassword"] ?? string.Empty;
        }

        public void SendEmail()
        {
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress(_fromAddress);
                message.To.Add(new MailAddress(_toAddress));
                message.Subject = _subject;
                message.IsBodyHtml = true; //to make message body as html  
                message.Body = File.ReadAllText(_bodyFilePath);
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com"; //for gmail host  
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(_fromAddress, _fromPassword);
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

                foreach (var i in Enumerable.Range(0, _numIterations))
                {
                    smtp.Send(message);
                    Thread.Sleep(TimeSpan.FromSeconds(_numSecondsBetweenIterations));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
