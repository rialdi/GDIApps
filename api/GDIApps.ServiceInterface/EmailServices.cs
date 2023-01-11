using System.Linq;
using System.Collections.Generic;
using ServiceStack;
using ServiceStack.FluentValidation;
using ServiceStack.OrmLite;
using ServiceStack.Script;
using ServiceStack.Auth;
using ServiceStack.IO;
using GDIApps.ServiceModel;
using GDIApps.ServiceModel.Types;

namespace GDIApps.ServiceInterface;

public class ContactUsEmailValidator : AbstractValidator<ContactUsEmail>
{
    public ContactUsEmailValidator()
    {
        RuleFor(x => x.Subject).NotEmpty();
    }
}

public class EmailServices : Service
{
    public IEmailer Emailer { get; set; }
    public IAutoQueryDb AutoQuery { get; set; }
    public ContactUsEmailResponse Any(ContactUsEmail request)
    {
        var contact = Db.SingleById<ContactUs>(request.ContactId);
        if (contact == null)
            throw HttpError.NotFound("Contact does not exist");

        var msg = new Email { From = "demo@servicestack.net", To = contact.Email }.PopulateWith(request);
        Emailer.SendEmail(msg);

        return new ContactUsEmailResponse { Email = contact.Email };
    }
    
    public object Any(AppUserConfirmEmail request)
    {
        using var db = AutoQuery.GetDb<EmailTemplate>(Request);
        var currEmailTemplate = db.Select<EmailTemplate>(w => w.Code == request.EmailTemplateCode).FirstOrDefault();
        var appUser = AuthRepository.GetUserAuthByUserName(request.UserName);

        if(appUser == null || currEmailTemplate == null)
            return null;

        var context = new ScriptContext {
            PageFormats = { new MarkdownPageFormat() },
            Args = {
                ["appUser"] = appUser,
            }
        }.Init();

        context.VirtualFiles.WriteFile("email.md", currEmailTemplate.EmailTemplateText);
        context.VirtualFiles.WriteFile("layout.html", currEmailTemplate.HtmlTemplate);

        var textEmail = new PageResult(context.GetPage("email")).Result;
        var htmlEmail = new PageResult(context.GetPage("email")) {
            Layout = "layout",
            PageTransformers = { MarkdownPageFormat.TransformToHtml }
        }.Result;

        var emailTo = appUser.Email;
        var emailSubject = "[GDIApps] Confirm Email";

        var msg = new Email { 
            Code = EMAIL_TEMPLATE_CODE.APPUSER_EMAIL_CONFIRM,
            To = emailTo,
            Subject = emailSubject,
            Body = htmlEmail 
        }.PopulateWith(request);
        Emailer.SendEmail(msg);

        return null;
    }
}
