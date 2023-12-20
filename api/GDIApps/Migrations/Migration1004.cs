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
        SeedData();
    }

    public override void Down()
    {
        Db.DropTable<TimeSheet>();
        Db.DropTable<ProjectTaskDoc>();
        Db.DropTable<ProjectTask>();
        Db.DropTable<ProjectPlan>();
    }

    private void SeedData() {
        #region ProjectPlan
        var projectId = Db.Select<Project>(w => w.Code == "IMACSUI").Select(s => s.Id).FirstOrDefault();
        int versionNo = 1;

        CreateProjectPlanData(projectId, versionNo, 1, 1, "",  "1", "", null, "Develop UI Screen", 10, null, null, 0, 0, true);
        CreateProjectPlanData(projectId, versionNo, 2, 1, "1",  "1.1", "", null, "Web App Template", 10, null, null, 0, 0, true);
        CreateProjectPlanData(projectId, versionNo, 2, 2, "1",  "1.2", "", null, "Data Entry", 10, null, null, 0, 0, true);
        CreateProjectPlanData(projectId, versionNo, 3, 1, "1.2",  "1.2.1", "", null, "Metal Accounting", 10, null, null, 0, 0, true);
        CreateProjectPlanData(projectId, versionNo, 4, 1, "1.2.1",  "1.2.1.1", "", 1, "Non Converter", 10, null, null, 0, 0, false);
        CreateProjectPlanData(projectId, versionNo, 4, 2, "1.2.1",  "1.2.1.2", "1.2.1.1", 1, "Converter", 10, null, null, 0, 0, false);
        #endregion
    }

    private long CreateProjectPlanData(
        int projectId, int versionNo, int taskLevel, int taskNo, string parentCode, string taskCode, string dependecyTaskCode, int? appUserId,
        string taskTitle, int durationDays, DateTime? startDate, DateTime? endDate, decimal completedPercentage, decimal resourceCost, bool hasChild
    ) =>
        Db.Insert(new ProjectPlan {
            ProjectId = projectId,
            VersionNo = versionNo,
            TaskLevel = taskLevel,
            TaskNo = taskNo,
            ParentCode = parentCode,
            TaskCode = taskCode,
            DependecyTaskCode = dependecyTaskCode,
            AppUserId = appUserId,
            TaskTitle = taskTitle,
            DurationDays = durationDays,
            CompletedPercentage = completedPercentage,
            ResourceCost = resourceCost,
            HasChild = hasChild,
            CreatedBy="Admin@email.com",
            CreatedDate= DateTime.Now,
            ModifiedBy="Admin@email.com",
            ModifiedDate = DateTime.Now
        }
    );

}