using System;
using System.Net;
using System.Net.Mail;

namespace EmailSender
{
    public class Emailer
    {
       public void SendEmail(string toAddress, string fromAddress, string fromPassword)
       {
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress(fromAddress);
                message.To.Add(new MailAddress(toAddress));
                message.Subject = "Test";
                message.IsBodyHtml = true; //to make message body as html  
                message.Body = "test body";
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com"; //for gmail host  
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(fromAddress, fromPassword);
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
