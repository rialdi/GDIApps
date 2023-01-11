using System.Net;
// using System.Net.Mail;
using System.Threading;
using GDIApps.ServiceModel.Types;
using ServiceStack;
using ServiceStack.Configuration;
using ServiceStack.OrmLite;

using System;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;

namespace GDIApps.ServiceInterface;

public interface IEmailer
{
    void SendEmail(Email email);
}

public class SmtpEmailer : RepositoryBase, IEmailer
{
    public IAppSettings AppSettings { get; set; }
    public SmtpConfig Config { get; set; }

    public void SendEmail(Email email)
    {
        email.SendStatusMessage = "Success";
        try
        {
            var newMimeEmail = new MimeMessage();
            newMimeEmail.From.Add(MailboxAddress.Parse(Config.UserName));
            newMimeEmail.To.Add(MailboxAddress.Parse(email.To));
            newMimeEmail.Subject = email.Subject;
            newMimeEmail.Body = new TextPart (TextFormat.Html) { Text = email.Body };

            // send email
            using var smtp = new SmtpClient();
            smtp.Connect(Config.Host, Config.Port, SecureSocketOptions.Auto);
            smtp.Authenticate(Config.UserName, Config.Password);
            smtp.Send(newMimeEmail);
            smtp.Disconnect(true);
        }
        catch(Exception ex)
        {
            email.SendStatusMessage = "Error : " + ex.Message;
        }

        email.From = Config.UserName;
        email.CreatedBy = email.From;
        email.CreatedDate = DateTime.UtcNow;
        email.ModifiedBy = email.From;
        email.ModifiedDate = DateTime.UtcNow;
        
        Db.Save(email);
    }
}

public class DbEmailer : RepositoryBase, IEmailer
{
    public void SendEmail(Email email)
    {
        Thread.Sleep(1000);  //simulate processing delay
        Db.Save(email);
    }
}