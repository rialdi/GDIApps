using System;
using System.Collections.Generic;
using ServiceStack;
using ServiceStack.DataAnnotations;

namespace GDIApps.ServiceModel.Types;

[CompositeIndex(true, nameof(ProjectId), nameof(VersionNo), nameof(TaskCode))]
public class ProjectPlan : AuditBase
{
    [AutoIncrement]
    public int Id { get; set; }  

    [Required]
    [References(typeof(Project))]
    public int ProjectId { get; set; }

    [Required]
    public int VersionNo { get; set; }

    [Required]
    public int TaskLevel { get; set; }

    [Required]
    public int TaskNo { get; set; }

    [Required]
    public string ParentCode { get; set; } = string.Empty;

    [Required]
    public string TaskCode { get; set; } = string.Empty;

    public string DependecyTaskCode { get; set; } = string.Empty;

    [References(typeof(AppUser))]
    public int? AppUserId { get; set;}

    [Required]
    public string TaskTitle { get; set; } = string.Empty;
    public decimal DurationDays { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public decimal CompletedPercentage { get; set; }
    public decimal ResourceCost { get; set; }

    public bool HasChild { get; set; }
}


public class ProjectPlanView : ProjectPlan
{
    [Compute, Ignore]
    public string CodeTitle {
        get {
            var tabString = "";
            for(int i=1; i < TaskLevel; i++)
            {
                // tabString += "&nbsp;&nbsp;";

                tabString += "  ";
            }
            return  tabString + TaskCode + ") ";// + TaskTitle;
        }
    }

    // [Compute, Ignore]
    // public bool HasChild {
    //     get {
    //         return true;
    //     }
    // }

    // [Compute, Ignore]
    // public string Group1 {
    //     get {
    //         return string.Empty;
    //     }
    // }

    // [Compute, Ignore]
    // public string Group2 {
    //     get {
    //         if(TaskLevel >= 2)
    //         {
    //             if(TaskLevel == 2) return ParentCode;
    //             else return ParentCode.Substring(0, ParentCode.IndexOf("."));
    //         }
    //         else
    //         {
    //             return string.Empty;
    //         }
    //     }
    // }

    // [Compute, Ignore]
    // public string Group3 {
    //     get {
            
    //         if(TaskLevel >= 3)
    //         {
    //             if(TaskLevel == 3) return ParentCode;
    //             else {
    //                 return ParentCode.Substring(0, ParentCode.IndexOf(".",3));
    //             }

    //         }
    //         else
    //         {
    //             return string.Empty;
    //         }
    //     }
    // }

    // [Compute, Ignore]
    // public string Group4 {
    //     get {
    //         return  (TaskLevel >= 4) ? ParentCode : string.Empty;
    //     }
    // }
}

[ValidateIsAuthenticated]
[Tag("Projects")]
public class QueryProjectPlans : QueryDb<ProjectPlan, ProjectPlanView>{
    public int? ProjectId { get; set;}
    public int? VersionNo { get; set; }
}


[Tag("Projects")]
[ValidateIsAuthenticated]
public class CreateProjectPlan : ICreateDb<ProjectPlan>, IReturn<CRUDResponse>
{
    // #pragma warning disable CS8618 
    #nullable disable
    public int ProjectId { get; set; }
    public int VersionNo { get; set; }
    public int TaskLevel { get; set; }
    public int TaskNo { get; set; }
    public string ParentCode { get; set; }
    public string TaskCode { get; set; } 
    public string DependecyTaskCode { get; set; }
    public int? AppUserId { get; set;}
    public string TaskTitle { get; set; } 
    public decimal DurationDays { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public decimal CompletedPercentage { get; set; }
    public decimal ResourceCost { get; set; }

    #nullable restore
    // #pragma warning restore CS8618 
}

[ValidateIsAuthenticated]
[Tag("Projects")]
public class UpdateProjectPlan : IPatchDb<ProjectPlan>, IReturn<CRUDResponse>
{
    // #pragma warning disable CS8618 
    #nullable disable
    public int Id { get; set; } 
    public int ProjectId { get; set; }
    public int VersionNo { get; set; }
    public int TaskLevel { get; set; }
    public int TaskNo { get; set; }
    public string ParentCode { get; set; } 
    public string TaskCode { get; set; } 
    public string DependecyTaskCode { get; set; } 
    public int? AppUserId { get; set;}
    public string TaskTitle { get; set; } 
    public decimal DurationDays { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public decimal CompletedPercentage { get; set; }
    public decimal ResourceCost { get; set; }

    #nullable restore
    // #pragma warning restore CS8618 
}

[ValidateIsAuthenticated]
[Tag("Projects")]
public class DeleteProjectPlan : IDeleteDb<Project>, IReturnVoid
{
    public int Id { get; set; }        
}


public class UpdateProjectPlanInBatch : IReturn<CRUDResponse> {
    public int ProjectId { get; set; }
    public int VersionNo { get; set; }

    // #pragma warning disable CS8618 
    #nullable disable
    public List<ProjectPlan> ProjectPlanList { get; set; }

    #nullable restore
    // #pragma warning restore CS8618 
}