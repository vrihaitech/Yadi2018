using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Configuration;
using System.Net.Mail;
using System.Text;
using OM;
namespace Yadi.DBClasses
{
    public class SendMail
    {
        CommonFunctions ObjFunction = new CommonFunctions();
        public static void SendEmail(string to, string cc, string subject, string body, List<string> attachment)
        {
            SmtpClient smtpClient = null;
            MailMessage mailMessage = null;
            //  SmtpSection section = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
            try
            {
                //Instantiate a new instance of MailMessage
                mailMessage = new MailMessage();
                // mailMessage.From = new MailAddress("vrihaitech@gmail.com");
                mailMessage.From = new MailAddress(DBGetVal.Email);
                //Set the recepient address of the mail message
                if (to != null && to != string.Empty)
                {
                    foreach (string addr in to.Split(';'))
                    {
                        mailMessage.To.Add(new MailAddress(addr));
                    }
                }
                //Check if the cc value is null or an empty value
                if ((cc != null) && (cc != string.Empty))
                {
                    // Set the CC address of the mail message
                    foreach (string addr in cc.Split(';'))
                    {
                        mailMessage.CC.Add(new MailAddress(addr));
                    }
                }

                //create Alrternative HTML view
                AlternateView htmlView = AlternateView.CreateAlternateViewFromString(body, null, "text/html");

                //LinkedResource theEmailImage = new LinkedResource("G:\\Secure Biz It\\IDEA MGT\\IMS WEB\\EmailBody\\IMSWeb\\IMS\\images\\logo.png");
                //theEmailImage.ContentId = "myImageID";

                //Add the Image to the Alternate view

                // htmlView.LinkedResources.Add(theEmailImage);


                mailMessage.AlternateViews.Add(htmlView);

                foreach (var path in attachment)
                {
                    mailMessage.Attachments.Add(new Attachment(path));
                }


                //Set the subject of the mail message
                mailMessage.Subject = subject;

                // Set the body of the mail message
                mailMessage.Body = body;

                //Set the format of the mail message body as HTML
                mailMessage.IsBodyHtml = true;

                //Set the priority of the mail message to normal
                mailMessage.Priority = MailPriority.Normal;

                //Instantiate a new instance of SmtpClient


                smtpClient = new SmtpClient();

                if (DBGetVal.Email.Trim().Contains("@gmail.com"))
                {


                    smtpClient.Host = "smtp.gmail.com";   // We use gmail as our smtp client
                    smtpClient.Port = 587;
                    smtpClient.EnableSsl = true;
                    smtpClient.UseDefaultCredentials = true;
                    smtpClient.Credentials = new System.Net.NetworkCredential(DBGetVal.Email.Trim(), DBGetVal.EmailPass.Trim());

                    smtpClient.Send(mailMessage);
                }
                else if (DBGetVal.Email.Trim().Contains("@rediffmail.com"))
                {
                    smtpClient.Host = "smtp.rediffmail.com";
                    smtpClient.Port = 25;





                    // smtpClient.Credentials = new System.Net.NetworkCredential("vrihaitech@gmail.com", "Vriha@123#");

                    smtpClient.Credentials = new System.Net.NetworkCredential(DBGetVal.Email.Trim(), DBGetVal.EmailPass.Trim());


                    smtpClient.EnableSsl = true;

                    //Send the mail message
                    smtpClient.Send(mailMessage);
                }
            }
            catch (Exception ex) 
            {
                throw ex;
            }
            

        }

    }
}
