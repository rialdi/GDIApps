using ServiceStack;
using ServiceStack.DataAnnotations;
using ServiceStack.OrmLite;
using GDIApps.ServiceModel.Types;

namespace GDIApps.Migrations;

public class Migration1002 : MigrationBase
{
    public override void Up()
    {
        Db.CreateTable<Client>();
        Db.CreateTable<Project>();

        long clientId = 0;
        long projectId = 0;
        clientId = CreateDataClient("POI", "PREMIER OIL INDONESIA", "", true);
        projectId = CreateDataProject(clientId, "POIBSFQR", "POI - Balance Sheet Recon", "", true);



        clientId = CreateDataClient("VALE", "PT VALE INDONESIA", "", true);
        projectId = CreateDataProject(clientId, "IMACSUI", "IMACS UI", "", true);

        // CreateEmailTemplate(
        //     EMAIL_TEMPLATE_CODE.APPUSER_EMAIL_CONFIRM, 
        //     "For AppUser Confirm Email",
        //     true,
        //     "-",
        //     "-"
        // );
    }

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

    public override void Down()
    {
        Db.DropTable<ContactUs>();
        Db.DropTable<Email>();
        Db.DropTable<EmailTemplate>();
    }
}
