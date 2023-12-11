using ServiceStack;
using ServiceStack.DataAnnotations;
using System.Collections;
using ServiceStack.OrmLite;
using GDIApps.ServiceModel.Types;

namespace GDIApps.Migrations;

public class Migration1004 : MigrationBase
{
    public override void Up()
    {
        Db.CreateTable<ProjectPlan>();
        Db.DropAndCreateTable<ProjectTask>();
        Db.CreateTable<ProjectTaskDoc>();
        Db.CreateTable<TimeSheet>();
    }

    public override void Down()
    {
        Db.DropTable<TimeSheet>();
        Db.DropTable<ProjectTaskDoc>();
        Db.DropTable<ProjectTask>();
        Db.DropTable<ProjectPlan>();
    }

}