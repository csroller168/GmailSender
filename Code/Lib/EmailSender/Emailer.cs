using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Mail;

namespace EmailSender
{
    public class Emailer
    {
        private readonly string _toAddress = ConfigurationManager.AppSettings["toAddress"];
        private readonly string _fromAddress = ConfigurationManager.AppSettings["fromAddress"];
        private readonly string _bodyFilePath = ConfigurationManager.AppSettings["bodyFilePath"];
        private readonly string _subject = ConfigurationManager.AppSettings["subject"];

        public void SendEmail(string fromPassword)
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
                smtp.Send(message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
