using ServiceStack;
using ServiceStack.DataAnnotations;
using ServiceStack.OrmLite;
using GDIApps.ServiceModel.Types;

namespace GDIApps.Migrations;

public class Migration1001 : MigrationBase
{
    public override void Up()
    {
        Db.CreateTable<ContactUs>();
        Db.CreateTable<Email>();
        Db.CreateTable<EmailTemplate>();

        // CreateEmailTemplate(
        //     EMAIL_TEMPLATE_CODE.APPUSER_EMAIL_CONFIRM, 
        //     "For AppUser Confirm Email",
        //     true,
        //     "-",
        //     "-"
        // );
    }

    public void CreateEmailTemplate(
        EMAIL_TEMPLATE_CODE emailTemplateCode, string description, bool isActive,
        string emailTemplateText, string emailTemplateHtml
    ) =>
        Db.Insert(new EmailTemplate {
            Code = emailTemplateCode,
            Description = description,
            IsActive = isActive,
            EmailTemplateText = emailTemplateText,
            HtmlTemplate = emailTemplateHtml,
            CreatedBy="Admin@email.com",
            CreatedDate= DateTime.Now,
            ModifiedBy="Admin@email.com",
            ModifiedDate = DateTime.Now
        });

    public override void Down()
    {
        Db.DropTable<ContactUs>();
        Db.DropTable<Email>();
        Db.DropTable<EmailTemplate>();
    }
}
