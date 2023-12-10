using ServiceStack;
using ServiceStack.DataAnnotations;
using System.Collections;
using ServiceStack.OrmLite;
using GDIApps.ServiceModel.Types;

namespace GDIApps.Migrations;

public class Migration1003 : MigrationBase
{
    public override void Up()
    {
        // Db.DropAndCreateTable<ProjectTeam>();
        Db.AddColumn<ProjectTeam>(x => x.ProjectTeamRoleCurrRate);
        Db.AddColumn<ProjectTeam>(x => x.ProjectTeamRoleRatePerDay);

    }

    public override void Down()
    {
        Db.DropColumn<ProjectTeam>(x => x.ProjectTeamRoleCurrRate);
        Db.DropColumn<ProjectTeam>(x => x.ProjectTeamRoleRatePerDay);
    }

}