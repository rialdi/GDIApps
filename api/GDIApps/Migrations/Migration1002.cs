using ServiceStack;
using ServiceStack.DataAnnotations;
using ServiceStack.OrmLite;
using GDIApps.ServiceModel.Types;

namespace GDIApps.Migrations;

public class Migration1002 : MigrationBase
{
    public override void Up()
    {
        Db.CreateTable<InvoiceAttachment>();
    }
    
    public override void Down()
    {
        Db.DropTable<InvoiceAttachment>();
    }
}
