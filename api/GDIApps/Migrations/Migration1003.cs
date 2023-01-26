using ServiceStack;
using ServiceStack.DataAnnotations;
using ServiceStack.OrmLite;
using GDIApps.ServiceModel.Types;

namespace GDIApps.Migrations;

public class Migration1003 : MigrationBase
{
    public override void Up()
    {
        Db.CreateTable<Lookup>();

        long lookupId = 0;
        lookupId = CreateDataLookup(LOOKUPTYPE.PRIORITY, "LOW", "LOW", true);
        lookupId = CreateDataLookup(LOOKUPTYPE.PRIORITY, "MEDIUM", "MEDIUM", true);
        lookupId = CreateDataLookup(LOOKUPTYPE.PRIORITY, "HIGH", "HIGH", true);

        lookupId = CreateDataLookup(LOOKUPTYPE.STATUS, "DRAFT", "DRAFT", true);
        lookupId = CreateDataLookup(LOOKUPTYPE.STATUS, "INPROGRESS", "IN PROGRESS", true);
        lookupId = CreateDataLookup(LOOKUPTYPE.STATUS, "COMPLETED", "COMPLETED", true);
    }

    public override void Down()
    {
        Db.DropTable<Client>();
        Db.DropTable<Project>();
    }

    #region Private Methods
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
