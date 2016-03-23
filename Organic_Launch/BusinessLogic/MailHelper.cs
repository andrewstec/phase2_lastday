using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using WebApplication1.ViewModels;

namespace WebApplication3.BusinessLogic
{
    public class MailHelper
    {
        public const string SUCCESS
        = "Am email has been sent to you. Please click the link provided to proceed.";
        public const string ERROR = "There was an error with your submission";
        const string TO = "davedsmithjr@gmail.com";  // Specify where you want this email sent.
        // This value may/may not be constant.
        // To get started use one of your email 
        // addresses.
        public string EmailFromArvixe(Message message)
        {
            // Use credentials of the Mail account that you created with the steps above.
            const string FROM = "marrionluaka@being-marrion.com";
            const string SUBJECT = "No reply: Acme Security Confirmation.";
            const string FROM_PWD = "1234567";
            const bool USE_HTML = true;

            // Get the mail server obtained in the steps described above.
            const string SMTP_SERVER = "143.95.249.35";
            try
            {
                MailMessage mailMsg = new MailMessage(FROM, message.Sender);
                mailMsg.Subject = SUBJECT;
                mailMsg.Body = message.Body + "<br/>sent by: " + TO;
                mailMsg.IsBodyHtml = USE_HTML;

                SmtpClient smtp = new SmtpClient();
                smtp.Host = SMTP_SERVER;
                smtp.Credentials = new System.Net.NetworkCredential(FROM, FROM_PWD);
                smtp.Port = 587;
                smtp.Send(mailMsg);
            }
            catch 
            {
                return ERROR;
            }
            return SUCCESS;
        }
    }
}
