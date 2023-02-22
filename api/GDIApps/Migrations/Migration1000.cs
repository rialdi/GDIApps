using ServiceStack;
using ServiceStack.DataAnnotations;
using ServiceStack.OrmLite;
using GDIApps.ServiceModel.Types;

namespace GDIApps.Migrations;

public class Migration1000 : MigrationBase
{
    public override void Up()
    {
        Db.CreateTable<Lookup>();
        Db.CreateTable<ContactUs>();
        Db.CreateTable<Email>();
        Db.CreateTable<EmailTemplate>();
        Db.CreateTable<Client>();
        Db.CreateTable<Project>();
        
        #region Seed Data

        CreateEmailTemplate(
            EMAIL_TEMPLATE_CODE.APPUSER_EMAIL_CONFIRM, 
            "For AppUser Confirm Email",
            true,
            "-",
            "-"
        );

        long lookupId = 0;
        lookupId = CreateDataLookup(LOOKUPTYPE.PRIORITY, "LOW", "LOW", true);
        lookupId = CreateDataLookup(LOOKUPTYPE.PRIORITY, "MEDIUM", "MEDIUM", true);
        lookupId = CreateDataLookup(LOOKUPTYPE.PRIORITY, "HIGH", "HIGH", true);

        lookupId = CreateDataLookup(LOOKUPTYPE.STATUS, "DRAFT", "DRAFT", true);
        lookupId = CreateDataLookup(LOOKUPTYPE.STATUS, "INPROGRESS", "IN PROGRESS", true);
        lookupId = CreateDataLookup(LOOKUPTYPE.STATUS, "COMPLETED", "COMPLETED", true);

        long clientId = 0;
        long projectId = 0;
        clientId = CreateDataClient("GDI", "GLOBAL DINAMIKA INFORMATIKA", "", true);
        
        clientId = CreateDataClient("VALE", "PT VALE INDONESIA", "", true);
        projectId = CreateDataProject(clientId, "IMACSUI", "IMACS UI", "", true);

        clientId = CreateDataClient("POI", "PREMIER OIL INDONESIA", "", true);
        projectId = CreateDataProject(clientId, "POIBSFQR", "POI - Balance Sheet Recon", "", true);

        clientId = CreateDataClient("PMSI", "PT. Philip Morris Sampoerna International Service Center", "", true);
       
       
        #endregion
    }
    
    public override void Down()
    {
        Db.DropTable<Lookup>();
        Db.DropTable<ContactUs>();
        Db.DropTable<Email>();
        Db.DropTable<EmailTemplate>();
        Db.DropTable<Client>();
        Db.DropTable<Project>();
    }

    #region Private Methods

    private void CreateEmailTemplate(
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

    private long CreateDataLookup(
        LOOKUPTYPE lookupType, string lookupValue, string lookupText, bool isActive 
    ) =>
        Db.Insert(new Lookup {
            LookupType = lookupType,
            LookupValue = lookupValue,
            LookupText = lookupText,
            IsActive = isActive,
            CreatedBy="Admin@email.com",
            CreatedDate= DateTime.Now,
            ModifiedBy="Admin@email.com",
            ModifiedDate = DateTime.Now
        }
    );

    private long CreateDataClient(
        string code, string name, string description, bool isActive
    ) =>
        Db.Insert(new Client {
            Code = code,
            Name = name,
            Description = description,
            IsActive = isActive,
            CreatedBy="Admin@email.com",
            CreatedDate= DateTime.Now,
            ModifiedBy="Admin@email.com",
            ModifiedDate = DateTime.Now
        }
    );

    private long CreateDataProject(
        long clientId, string code, string name, string description, bool isActive
    ) =>
        Db.Insert(new Project {
            ClientId = (int)clientId,
            Code = code,
            Name = name,
            Description = description,
            IsActive = isActive,
            CreatedBy="Admin@email.com",
            CreatedDate= DateTime.Now,
            ModifiedBy="Admin@email.com",
            ModifiedDate = DateTime.Now
        }
    );

    #endregion
}
