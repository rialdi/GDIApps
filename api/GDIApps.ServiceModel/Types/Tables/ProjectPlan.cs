using System;
using System.Collections.Generic;
using ServiceStack;
using ServiceStack.DataAnnotations;

namespace GDIApps.ServiceModel.Types;

[CompositeIndex(true, nameof(ProjectId), nameof(VersionNo), nameof(TaskCode))]
public class ProjectPlan
{
    [AutoIncrement]
    public int Id { get; set; }  

    [Required]
    [References(typeof(Project))]
    public int ProjectId { get; set; }

    [Required]
    public int VersionNo { get; set; }

    [Required]
    public string TaskCode { get; set; } = string.Empty;

    public string ParentCode { get; set; } = string.Empty;

    [Required]
    public int TaskLevel { get; set; }

    [Required]
    public int TaskNo { get; set; }

    [Required]
    public string TaskTitle { get; set; } = string.Empty;
    public decimal DurationDays { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public decimal Percemtage { get; set; }

    public string Assigned { get; set; } = string.Empty;
    public decimal ResourceCost { get; set; }
}


[ValidateIsAuthenticated]
[Tag("Projects")]
public class QueryProjectPlans : QueryDb<ProjectPlan>{
    public int? ProjectId { get; set;}
}


[Tag("Projects")]
[ValidateIsAuthenticated]
public class CreateProjectPlan : ICreateDb<ProjectPlan>, IReturn<CRUDResponse>
{
    public int ProjectId { get; set; }
    public int VersionNo { get; set; }
    public string TaskCode { get; set; } = string.Empty;
    public string ParentCode { get; set; } = string.Empty;
    public string DependecyTaskCode { get; set; } = string.Empty;
    public int TaskLevel { get; set; }
    public int TaskNo { get; set; }
    public string TaskTitle { get; set; } = string.Empty;
    public decimal DurationDays { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal Percemtage { get; set; }
    public string Assigned { get; set; } = string.Empty;
    public decimal ResourceCost { get; set; }
}

[ValidateIsAuthenticated]
[Tag("Projects")]
public class UpdateProjectPlan : IPatchDb<ProjectPlan>, IReturn<CRUDResponse>
{
    public int Id { get; set; } 
    public int ProjectId { get; set; }
    public int VersionNo { get; set; }
    public string TaskCode { get; set; } = string.Empty;
    public string ParentCode { get; set; } = string.Empty;
    public string DependecyTaskCode { get; set; } = string.Empty;
    public int TaskLevel { get; set; }
    public int TaskNo { get; set; }
    public string TaskTitle { get; set; } = string.Empty;
    public decimal DurationDays { get; set; }
    
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal Percemtage { get; set; }
    public string Assigned { get; set; } = string.Empty;
    public decimal ResourceCost { get; set; }
}

[ValidateIsAuthenticated]
[Tag("Projects")]
public class DeleteProjectPlan : IDeleteDb<Project>, IReturnVoid
{
    public int Id { get; set; }        
}
