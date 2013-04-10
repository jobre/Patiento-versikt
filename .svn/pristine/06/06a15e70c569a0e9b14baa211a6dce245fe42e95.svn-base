using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Mail;
using GCS;

namespace Ortoped.HelpClasses
{
  class Mail
  {
    public void sendMail(string smtp, int port, string from, string to, string subject, string message, string[] file, string user, string password)
    {
      //create the mail message
      MailMessage mail = new MailMessage();
      SmtpClient scSmtp = null;


      try
      {
        Log4Net.Logger.loggInfo("Starting sendmail", Config.User, "Mail.sendMail");

        mail.From = new MailAddress(from);
        mail.To.Add(to);
        mail.Subject = subject;
        mail.Body = message;

        Log4Net.Logger.loggInfo("After creation of mail", Config.User, "Mail.sendMail");

        //send the message
        scSmtp = new SmtpClient(smtp);
        scSmtp.DeliveryMethod = SmtpDeliveryMethod.Network;
        scSmtp.Port = port;

        Log4Net.Logger.loggInfo("After config", Config.User, "Mail.sendMail");

        if (GCF.noNULL(user).Trim().Equals(""))
        {
          scSmtp.UseDefaultCredentials = true;
        }
        else
        {
          scSmtp.UseDefaultCredentials = false;
          scSmtp.Credentials = new NetworkCredential(user, password);
        }

        foreach (string s in file)
        {
          Log4Net.Logger.loggInfo("Adding attachment " + s, Config.User, "Mail.sendMail");
          Attachment att = new Attachment(s);
          mail.Attachments.Add(att);
        }

        scSmtp.Send(mail);
        Log4Net.Logger.loggInfo("Sent mail with config (Port, Host, UseDefCred: " + scSmtp.Port + " : " + scSmtp.Host + " : " + scSmtp.UseDefaultCredentials.ToString(), Config.User, "Mail.sendMail");
      }
      catch(Exception ex)
      {
//        System.Threading.Thread.Sleep(2000);
        Log4Net.Logger.loggError(ex, "Error", Config.User, "Mail.sendMail"); 
        Log4Net.Logger.loggError(ex, "Error while sending mail (Port, Host, UseDefCred, user, password: " + scSmtp.Port +  " : " + scSmtp.Host + " : " + scSmtp.UseDefaultCredentials.ToString() + " : " + user + " : " + password, Config.User, "Mail.sendMail"); 
      }
    }
	}
}
