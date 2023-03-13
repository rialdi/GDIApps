using ServiceStack;
using ServiceStack.DataAnnotations;
using System.Collections;
using ServiceStack.OrmLite;
using GDIApps.ServiceModel.Types;

namespace GDIApps.Migrations;

public class Migration1002 : MigrationBase
{
    public override void Up()
    {
        Db.CreateTable<ProjectDoc>();
        Db.CreateTable<ProjectTeam>();
        Db.CreateTable<ProjectTask>();

        Db.CreateTable<OtherTask>();

        Db.CreateTable<TimeSheet>();
        Db.CreateTable<TimeSheetDetail>();

        Db.CreateTable<ReviewMasterQuestion>();
        Db.CreateTable<EmployeeReview>();
        Db.CreateTable<EmployeeReviewDetail>();

        Db.CreateTable<DailyScrumMeeting>();

        int projectId = GetProjectId("GDIAPPS");
        int appUserId = 1; // Rialdi
        
        CreateProjectTeam(projectId, appUserId, PROJECT_TEAM_ROLE.TEAM_LEAD);
        
        #region Seed Project Task Data
        decimal projectTaskDurationDays = 3;
        DateTime? projectTaskPlanStart = new DateTime(2023, 03, 1);
        DateTime? projectTaskPlanEnd = ((DateTime) projectTaskPlanStart).AddDays((double) projectTaskDurationDays);
        DateTime? projectTaskActualStart = new DateTime(2023, 03, 1);
        DateTime? projectTaskActualEnd = ((DateTime) projectTaskActualStart).AddDays((double) projectTaskDurationDays);
        CreateProjectTask(projectId, 1, "Roles & Menus from Database", "Get Roles & Menu From Database", projectTaskDurationDays, 
            projectTaskPlanStart, projectTaskPlanEnd, projectTaskActualStart, projectTaskActualEnd, 0, TASK_STATUS.COMPLETED
        );
        projectTaskPlanStart = new DateTime(2023, 03, 5);
        projectTaskPlanEnd = ((DateTime) projectTaskPlanStart).AddDays((double) projectTaskDurationDays);
        projectTaskActualStart = new DateTime(2023, 03, 5);
        projectTaskActualEnd = null;

        CreateProjectTask(projectId, 2, "Common Features", "Develop Common Features", projectTaskDurationDays, 
            projectTaskPlanStart, projectTaskPlanEnd, projectTaskActualStart, projectTaskActualEnd, 0, TASK_STATUS.IN_PROGRESS
        );
        projectTaskPlanStart = new DateTime(2023, 03, 9);
        projectTaskPlanEnd = ((DateTime) projectTaskPlanStart).AddDays((double) projectTaskDurationDays);
        projectTaskActualStart = null;
        projectTaskActualEnd = null;
        CreateProjectTask(projectId, 3, "Employee Review", "Create Employee Review Modules", projectTaskDurationDays, 
            projectTaskPlanStart, projectTaskPlanEnd, projectTaskActualStart, projectTaskActualEnd, 0, TASK_STATUS.YET_TO_START
        );
        #endregion

        #region Seed Other Task Data
        DateTime? otherTaskPlanStart = new DateTime(2023, 03, 9);
        DateTime? otherTaskPlanEnd = ((DateTime) otherTaskPlanStart).AddDays((double) projectTaskDurationDays);
        DateTime? otherTaskActualStart = null;
        DateTime? otherTaskActualEnd = null;
        CreateOtherTask(appUserId, "Create GDI Apps Requirement",  "Create GDI Apps Requirement", 3, 
            otherTaskPlanStart, otherTaskPlanEnd, otherTaskActualStart, otherTaskActualEnd, 0, TASK_STATUS.YET_TO_START);
        #endregion
    
        int timeSheetId = (int) CreateTimeSheet(projectId, appUserId, DateTime.Today);
        CreateTimeSheetDetail(timeSheetId, projectId, "Common Features");

        #region Seed Data Master Question

        #region Seed Data Master Question Team Leader
        CreateReviewMasterQuestion(REVIEW_TYPE.TEAM_LEADER, REVIEW_QUESTION_CATEGORY.EMPLOYEE_COMPETENCY, 1, "Work Ethic", "");
        CreateReviewMasterQuestion(REVIEW_TYPE.TEAM_LEADER, REVIEW_QUESTION_CATEGORY.EMPLOYEE_COMPETENCY, 2, "Problem Solving", "");
        CreateReviewMasterQuestion(REVIEW_TYPE.TEAM_LEADER, REVIEW_QUESTION_CATEGORY.EMPLOYEE_COMPETENCY, 3, "Teamwork", "");
        CreateReviewMasterQuestion(REVIEW_TYPE.TEAM_LEADER, REVIEW_QUESTION_CATEGORY.EMPLOYEE_COMPETENCY, 4, "Critical Thinking", "");
        CreateReviewMasterQuestion(REVIEW_TYPE.TEAM_LEADER, REVIEW_QUESTION_CATEGORY.EMPLOYEE_COMPETENCY, 5, "Integrity", "");
        CreateReviewMasterQuestion(REVIEW_TYPE.TEAM_LEADER, REVIEW_QUESTION_CATEGORY.EMPLOYEE_COMPETENCY, 6, "Communication", "");
        CreateReviewMasterQuestion(REVIEW_TYPE.TEAM_LEADER, REVIEW_QUESTION_CATEGORY.EMPLOYEE_COMPETENCY, 7, "Collaboration", "");
        CreateReviewMasterQuestion(REVIEW_TYPE.TEAM_LEADER, REVIEW_QUESTION_CATEGORY.EMPLOYEE_COMPETENCY, 8, "Initiative", "");
        CreateReviewMasterQuestion(REVIEW_TYPE.TEAM_LEADER, REVIEW_QUESTION_CATEGORY.EMPLOYEE_COMPETENCY, 9, "Innovation", "");
        CreateReviewMasterQuestion(REVIEW_TYPE.TEAM_LEADER, REVIEW_QUESTION_CATEGORY.EMPLOYEE_COMPETENCY, 10, "Results Orientation", "");
        CreateReviewMasterQuestion(REVIEW_TYPE.TEAM_LEADER, REVIEW_QUESTION_CATEGORY.EMPLOYEE_COMPETENCY, 11, "Self Confidence", "");
        CreateReviewMasterQuestion(REVIEW_TYPE.TEAM_LEADER, REVIEW_QUESTION_CATEGORY.EMPLOYEE_COMPETENCY, 12, "Flexibility", "");
        CreateReviewMasterQuestion(REVIEW_TYPE.TEAM_LEADER, REVIEW_QUESTION_CATEGORY.KPI, 1, "Lead Development Team", "");
        CreateReviewMasterQuestion(REVIEW_TYPE.TEAM_LEADER, REVIEW_QUESTION_CATEGORY.KPI, 2, "Managing the successful delivery of projects will require you to plan, coordinate and lead activities across the full delivery lifecycle", "");
        CreateReviewMasterQuestion(REVIEW_TYPE.TEAM_LEADER, REVIEW_QUESTION_CATEGORY.KPI, 3, "Working closely with the client's Project Managers, coordinating business analysts, architects and developers, as well as liaising with key project stakeholders", "");
        CreateReviewMasterQuestion(REVIEW_TYPE.TEAM_LEADER, REVIEW_QUESTION_CATEGORY.KPI, 4, "Working closely with other GDI's team leaders", "");
        CreateReviewMasterQuestion(REVIEW_TYPE.TEAM_LEADER, REVIEW_QUESTION_CATEGORY.KPI, 5, "Support and assist developers at code level", "");
        CreateReviewMasterQuestion(REVIEW_TYPE.TEAM_LEADER, REVIEW_QUESTION_CATEGORY.KPI, 6, "Line management and personal development of a team of highly skilled developers", "");
        CreateReviewMasterQuestion(REVIEW_TYPE.TEAM_LEADER, REVIEW_QUESTION_CATEGORY.KPI, 7, "Managing the delivery of multiple complex simultaneous system development projects from design through to release", "");
        CreateReviewMasterQuestion(REVIEW_TYPE.TEAM_LEADER, REVIEW_QUESTION_CATEGORY.KPI, 8, "Capable of understanding and contributing to the technical solution from design through to code level", "");
        CreateReviewMasterQuestion(REVIEW_TYPE.TEAM_LEADER, REVIEW_QUESTION_CATEGORY.KPI, 9, "Provide regular and effective progress updates to and work closely with Development Project Managers to ensure the management of any delivery risks or issues", "");
        CreateReviewMasterQuestion(REVIEW_TYPE.TEAM_LEADER, REVIEW_QUESTION_CATEGORY.KPI, 10, "Define delivery phases of the project including activities, sub-activities, and milestones ensuring these are documented and used as the basis for the project event log, issues and risk log and any subsequent reporting", "");
        CreateReviewMasterQuestion(REVIEW_TYPE.TEAM_LEADER, REVIEW_QUESTION_CATEGORY.KPI, 11, "Participate in reviews and meetings and provide updates on project progress", "");
        CreateReviewMasterQuestion(REVIEW_TYPE.TEAM_LEADER, REVIEW_QUESTION_CATEGORY.KPI, 12, "Contributing to post implementation reviews helping to demonstrate success or otherwise of projects", "");
        CreateReviewMasterQuestion(REVIEW_TYPE.TEAM_LEADER, REVIEW_QUESTION_CATEGORY.KPI, 13, "Supporting project resource scheduling and capacity planning", "");
        CreateReviewMasterQuestion(REVIEW_TYPE.TEAM_LEADER, REVIEW_QUESTION_CATEGORY.KPI, 14, "Take responsibility for making key decisions to ensure the successful implementation of all initiatives", "");
        #endregion

        #region Seed Data Master Question Team Member
        CreateReviewMasterQuestion(REVIEW_TYPE.TEAM_MEMBER, REVIEW_QUESTION_CATEGORY.EMPLOYEE_COMPETENCY, 1, "Work Ethic", "");
        CreateReviewMasterQuestion(REVIEW_TYPE.TEAM_MEMBER, REVIEW_QUESTION_CATEGORY.EMPLOYEE_COMPETENCY, 2, "Problem Solving", "");
        CreateReviewMasterQuestion(REVIEW_TYPE.TEAM_MEMBER, REVIEW_QUESTION_CATEGORY.EMPLOYEE_COMPETENCY, 3, "Teamwork", "");
        CreateReviewMasterQuestion(REVIEW_TYPE.TEAM_MEMBER, REVIEW_QUESTION_CATEGORY.EMPLOYEE_COMPETENCY, 4, "Critical Thinking", "");
        CreateReviewMasterQuestion(REVIEW_TYPE.TEAM_MEMBER, REVIEW_QUESTION_CATEGORY.EMPLOYEE_COMPETENCY, 5, "Integrity", "");
        CreateReviewMasterQuestion(REVIEW_TYPE.TEAM_MEMBER, REVIEW_QUESTION_CATEGORY.EMPLOYEE_COMPETENCY, 6, "Communication", "");
        CreateReviewMasterQuestion(REVIEW_TYPE.TEAM_MEMBER, REVIEW_QUESTION_CATEGORY.EMPLOYEE_COMPETENCY, 7, "Collaboration", "");
        CreateReviewMasterQuestion(REVIEW_TYPE.TEAM_MEMBER, REVIEW_QUESTION_CATEGORY.EMPLOYEE_COMPETENCY, 8, "Initiative", "");
        CreateReviewMasterQuestion(REVIEW_TYPE.TEAM_MEMBER, REVIEW_QUESTION_CATEGORY.EMPLOYEE_COMPETENCY, 9, "Innovation", "");
        CreateReviewMasterQuestion(REVIEW_TYPE.TEAM_MEMBER, REVIEW_QUESTION_CATEGORY.EMPLOYEE_COMPETENCY, 10, "Results Orientation", "");
        CreateReviewMasterQuestion(REVIEW_TYPE.TEAM_MEMBER, REVIEW_QUESTION_CATEGORY.EMPLOYEE_COMPETENCY, 11, "Self Confidence", "");
        CreateReviewMasterQuestion(REVIEW_TYPE.TEAM_MEMBER, REVIEW_QUESTION_CATEGORY.EMPLOYEE_COMPETENCY, 12, "Flexibility", "");
        CreateReviewMasterQuestion(REVIEW_TYPE.TEAM_MEMBER, REVIEW_QUESTION_CATEGORY.KPI, 1, "The employee achieves the objectives of the job.", "");
        CreateReviewMasterQuestion(REVIEW_TYPE.TEAM_MEMBER, REVIEW_QUESTION_CATEGORY.KPI, 2, "The employee meets criteria for performance.", "");
        CreateReviewMasterQuestion(REVIEW_TYPE.TEAM_MEMBER, REVIEW_QUESTION_CATEGORY.KPI, 3, "The employee fulfills all the requirements of the job.", "");
        CreateReviewMasterQuestion(REVIEW_TYPE.TEAM_MEMBER, REVIEW_QUESTION_CATEGORY.KPI, 4, "The employee demonstrates expertise in all job-related tasks.", "");
        CreateReviewMasterQuestion(REVIEW_TYPE.TEAM_MEMBER, REVIEW_QUESTION_CATEGORY.KPI, 5, "The employee can manage more responsibility than currently assigned.", "");
        CreateReviewMasterQuestion(REVIEW_TYPE.TEAM_MEMBER, REVIEW_QUESTION_CATEGORY.KPI, 6, "The employee meets deadlines.", "");
        CreateReviewMasterQuestion(REVIEW_TYPE.TEAM_MEMBER, REVIEW_QUESTION_CATEGORY.KPI, 7, "The employee helps others.", "");
        CreateReviewMasterQuestion(REVIEW_TYPE.TEAM_MEMBER, REVIEW_QUESTION_CATEGORY.KPI, 8, "The employee volunteers to do things not required by the job.", "");
        #endregion

        #region Seed Data Master Question QA
        CreateReviewMasterQuestion(REVIEW_TYPE.QA, REVIEW_QUESTION_CATEGORY.EMPLOYEE_COMPETENCY, 1, "Work Ethic", "");
        CreateReviewMasterQuestion(REVIEW_TYPE.QA, REVIEW_QUESTION_CATEGORY.EMPLOYEE_COMPETENCY, 2, "Problem Solving", "");
        CreateReviewMasterQuestion(REVIEW_TYPE.QA, REVIEW_QUESTION_CATEGORY.EMPLOYEE_COMPETENCY, 3, "Teamwork", "");
        CreateReviewMasterQuestion(REVIEW_TYPE.QA, REVIEW_QUESTION_CATEGORY.EMPLOYEE_COMPETENCY, 4, "Critical Thinking", "");
        CreateReviewMasterQuestion(REVIEW_TYPE.QA, REVIEW_QUESTION_CATEGORY.EMPLOYEE_COMPETENCY, 5, "Integrity", "");
        CreateReviewMasterQuestion(REVIEW_TYPE.QA, REVIEW_QUESTION_CATEGORY.EMPLOYEE_COMPETENCY, 6, "Communication", "");
        CreateReviewMasterQuestion(REVIEW_TYPE.QA, REVIEW_QUESTION_CATEGORY.EMPLOYEE_COMPETENCY, 7, "Collaboration", "");
        CreateReviewMasterQuestion(REVIEW_TYPE.QA, REVIEW_QUESTION_CATEGORY.EMPLOYEE_COMPETENCY, 8, "Initiative", "");
        CreateReviewMasterQuestion(REVIEW_TYPE.QA, REVIEW_QUESTION_CATEGORY.EMPLOYEE_COMPETENCY, 9, "Innovation", "");
        CreateReviewMasterQuestion(REVIEW_TYPE.QA, REVIEW_QUESTION_CATEGORY.EMPLOYEE_COMPETENCY, 10, "Results Orientation", "");
        CreateReviewMasterQuestion(REVIEW_TYPE.QA, REVIEW_QUESTION_CATEGORY.EMPLOYEE_COMPETENCY, 11, "Self Confidence", "");
        CreateReviewMasterQuestion(REVIEW_TYPE.QA, REVIEW_QUESTION_CATEGORY.EMPLOYEE_COMPETENCY, 12, "Flexibility", "");
        CreateReviewMasterQuestion(REVIEW_TYPE.QA, REVIEW_QUESTION_CATEGORY.KPI, 1, "Provide standard project documentation (BRD, Guidance and Technical Documentation)", "");
        CreateReviewMasterQuestion(REVIEW_TYPE.QA, REVIEW_QUESTION_CATEGORY.KPI, 2, "Testing applications and identifying deficiencies.", "");
        CreateReviewMasterQuestion(REVIEW_TYPE.QA, REVIEW_QUESTION_CATEGORY.KPI, 3, "Investigate product quality in order to make improvements to achieve better customer satisfaction", "");
        CreateReviewMasterQuestion(REVIEW_TYPE.QA, REVIEW_QUESTION_CATEGORY.KPI, 4, "Collaborate with the Product Development team to ensure consistent project execution", "");
        CreateReviewMasterQuestion(REVIEW_TYPE.QA, REVIEW_QUESTION_CATEGORY.KPI, 5, "Collect quality data", "");
        #endregion
        
        #endregion

        #region Seed Data Employee Review

        DateTime reviewDate = new DateTime(2022, 12, 15, 10, 05,00);
        int reviewerId = GetAppUserId("rialdi@ptgdi.com");
        int reviewedEmployeeId = GetAppUserId("aminullah@ptgdi.com");
        int employeeReviewId = (int) CreateEmployeeReview(2022, "Q4", reviewDate, reviewerId, reviewedEmployeeId, "Excelent");
        CreateEmployeeReviewDetail( employeeReviewId, REVIEW_TYPE.TEAM_LEADER, REVIEW_QUESTION_CATEGORY.EMPLOYEE_COMPETENCY, 1, "Work Ethic", 5);
        
        #endregion
    
        CreateDailyScrumMeeting(appUserId, DateTime.Now, "No Blockers", "GDIAPPS - Create Module Get Roles & Menus From DB", "GDIAPPS - Employee Reviews"); 
    
    }

    public override void Down()
    {
        Db.DropTable<DailyScrumMeeting>();

        Db.DropTable<EmployeeReviewDetail>();
        Db.DropTable<EmployeeReview>();

        Db.DropTable<ReviewMasterQuestion>();

        Db.DropTable<TimeSheetDetail>();
        Db.DropTable<TimeSheet>();

        Db.DropTable<OtherTask>();
        Db.DropTable<ProjectTask>();
        Db.DropTable<ProjectDoc>();
        Db.DropTable<ProjectTeam>();
    }

    private int GetAppUserId(string email)
    {
        int returnId = 0;
        var foundItem = Db.Select<AppUser>(w => w.Email == email).FirstOrDefault();
        if(foundItem != null) {
            returnId = foundItem.Id;
        }
        return returnId;
    }

    private int GetProjectId(string code)
    {
        int returnId = 0;
        var foundItem = Db.Select<Project>(w => w.Code == code).FirstOrDefault();
        if(foundItem != null) {
            returnId = foundItem.Id;
        }
        return returnId;
    }

    private long CreateProjectTask(int projectId, int no, string taskName, string description, decimal durationDays,
        DateTime? planStart, DateTime? planEnd, DateTime? actualStart, DateTime? actualEnd, decimal completed,
        TASK_STATUS status
    ) =>
        Db.Insert(new ProjectTask {
            ProjectId = projectId,
            No = no,
            TaskName = taskName,
            Description = description,
            DurationDays = durationDays,
            PlanStart = planStart,
            PlanEnd = planEnd,
            ActualStart = actualStart,
            ActualEnd = actualEnd,
            Completed = completed,
            Status = status,
            CreatedBy="Admin@email.com",
            CreatedDate= DateTime.Now,
            ModifiedBy="Admin@email.com",
            ModifiedDate = DateTime.Now
        });

    private long CreateProjectTeam(
        int projectId, int appUserId, PROJECT_TEAM_ROLE projectTeamRole
    ) =>
        Db.Insert(new ProjectTeam {
            ProjectId = projectId,
            AppUserId = appUserId,
            ProjectTeamRole = projectTeamRole
        });

    private long CreateOtherTask(
        int appUserId, string taskName, string taskDesc, decimal durationDays,
        DateTime? planStart, DateTime? planEnd, DateTime? actualStart, DateTime? actualEnd, decimal completed,TASK_STATUS status
    ) =>
        Db.Insert(new OtherTask {
            AppUserId = appUserId,
            TaskName = taskName,
            TaskDesc = taskDesc,
            DurationDays = durationDays,
            PlanStart = planStart,
            PlanEnd = planEnd,
            ActualStart = actualStart,
            ActualEnd = actualEnd,
            Completed = completed,
            Status = status,
            CreatedBy="Admin@email.com",
            CreatedDate= DateTime.Now,
            ModifiedBy="Admin@email.com",
            ModifiedDate = DateTime.Now
        });

    private long CreateTimeSheet(
        int projectId, int appUserId, DateTime tsDate
    ) =>
        Db.Insert(new TimeSheet {
            AppUserId = appUserId,
            TSDate = tsDate,
            CreatedBy="Admin@email.com",
            CreatedDate= DateTime.Now,
            ModifiedBy="Admin@email.com",
            ModifiedDate = DateTime.Now
        });

    private long CreateTimeSheetDetail(
        int timesheetId, int? projectId, string taskName
    ) =>
        Db.Insert(new TimeSheetDetail {
            TimeSheetId = timesheetId,
            ProjectId = projectId,
            TaskName = taskName,
            CreatedBy="Admin@email.com",
            CreatedDate= DateTime.Now,
            ModifiedBy="Admin@email.com",
            ModifiedDate = DateTime.Now
        });

    private long CreateReviewMasterQuestion(
        REVIEW_TYPE reviewType, REVIEW_QUESTION_CATEGORY category, int no, string question, string description
    ) =>
        Db.Insert(new ReviewMasterQuestion {
            ReviewType = reviewType,
            Category = category,
            No = no,
            Question = question,
            Description = description,
            CreatedBy="Admin@email.com",
            CreatedDate= DateTime.Now,
            ModifiedBy="Admin@email.com",
            ModifiedDate = DateTime.Now
        });

    private long CreateEmployeeReview(
        int periodYear, string periodQuarter, DateTime reviewDate, int reviewerId, int reviewedEmployeeId, string reviewNotes
    ) =>
        Db.Insert(new EmployeeReview {
            PeriodYear = periodYear,
            PeriodQuarter = periodQuarter,
            ReviewDate = reviewDate,
            ReviewerId = reviewerId,
            ReviewedEmployeeId = reviewedEmployeeId,
            ReviewNotes = reviewNotes,
            CreatedBy="Admin@email.com",
            CreatedDate= DateTime.Now,
            ModifiedBy="Admin@email.com",
            ModifiedDate = DateTime.Now
        });

    private long CreateEmployeeReviewDetail(
        int employeeReviewId,
        REVIEW_TYPE reviewType, REVIEW_QUESTION_CATEGORY category, int no, string question, int questionValue
    ) =>
        Db.Insert(new EmployeeReviewDetail {
            EmployeeReviewId = employeeReviewId,
            ReviewType = reviewType,
            Category = category,
            No = no,
            Question = question,
            QuestionValue = questionValue,
            // CreatedBy="Admin@email.com",
            // CreatedDate= DateTime.Now,
            // ModifiedBy="Admin@email.com",
            // ModifiedDate = DateTime.Now
        });

    private long CreateDailyScrumMeeting(
        int appUserId, DateTime meetingDate, string blockers, string whatDidYouDoYesterday, string whatGoalsToday
    ) =>
        Db.Insert(new DailyScrumMeeting {
            AppUserId = appUserId,
            MeetingDate = meetingDate,
            Blockers = blockers,
            WhatDidYouDoYesterday = whatDidYouDoYesterday,
            WhatGoalsToday = whatGoalsToday,
            CreatedBy="Admin@email.com",
            CreatedDate= DateTime.Now,
            ModifiedBy="Admin@email.com",
            ModifiedDate = DateTime.Now
        });
}