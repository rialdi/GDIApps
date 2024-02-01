using ServiceStack;
using ServiceStack.DataAnnotations;
using System.Collections;
using ServiceStack.OrmLite;
using GDIApps.ServiceModel.Types;

namespace GDIApps.Migrations;

public class Migration1005 : MigrationBase
{
    public override void Up()
    {
        Db.CreateTable<EmpLeave>();
        Db.CreateTable<EmpReimbursement>();
    }

    public override void Down()
    {
        Db.DropTable<EmpReimbursement>();
        Db.DropTable<EmpLeave>();
    }
}