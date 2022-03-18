using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;

namespace EmailSender
{
    public class Emailer
    {
       private readonly string _toAddress = ConfigurationManager.AppSettings["toAddress"];
        private readonly string _fromAddress = ConfigurationManager.AppSettings["fromAddress"];

        public void SendEmail(string fromPassword)
       {
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress(_fromAddress);
                message.To.Add(new MailAddress(_toAddress));
                message.Subject = "Test";
                message.IsBodyHtml = true; //to make message body as html  
                message.Body = "test body";
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
