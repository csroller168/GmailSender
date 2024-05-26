using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading;

namespace EmailSender
{
    public class Emailer
    {
        private readonly string _toAddress = "fake@fake.com"; // ConfigurationManager.AppSettings["toAddress"];
        private readonly string _fromAddress = "fake@fake.com"; //ConfigurationManager.AppSettings["fromAddress"];
        private readonly string _bodyFilePath = "fake@fake.com"; //ConfigurationManager.AppSettings["bodyFilePath"];
        private readonly string _subject = "fake@fake.com"; //ConfigurationManager.AppSettings["subject"];

        public void SendEmail(
            int numIterations,
            int numSecondsBetweenIterations,
            string fromPassword)
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
                smtp.Credentials = new NetworkCredential(_fromAddress, fromPassword);
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

                foreach (var i in Enumerable.Range(0, numIterations))
                {
                    smtp.Send(message);
                    Thread.Sleep(TimeSpan.FromSeconds(numSecondsBetweenIterations));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
