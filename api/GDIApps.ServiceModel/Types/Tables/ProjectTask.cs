using System;
using System.Collections.Generic;
using ServiceStack;
using ServiceStack.DataAnnotations;

namespace GDIApps.ServiceModel.Types;

[CompositeIndex(true, nameof(ProjectId), nameof(No))]
public class ProjectTask : AuditBase
{
    [AutoIncrement]
    public int Id { get; set; }  

    [Required]
    [References(typeof(Project))]
    public int ProjectId { get; set; }

    [Required]
    public int No { get; set; }
    
    [Required]
    [StringLength(200)] 
    public string TaskName { get; set; } = string.Empty;


    [StringLength(1000)] 
    public string Description { get; set; } = string.Empty;
    [Required]
    public decimal DurationDays { get; set; }
    public DateTime? PlanStart { get; set; }
    public DateTime? PlanEnd { get; set; }
    public DateTime? ActualStart { get; set; }
    public DateTime? ActualEnd { get; set; }

    // In Percent
    [Required]
    public decimal Completed { get; set; }
    [Required]
    [StringLength(100)]
    public TASK_STATUS Status { get; set; }

    [References(typeof(ProjectTeam))]
    public int? ProjectTeamId { get; set;}
}

[ValidateIsAuthenticated]
[Tag("Projects")]
[AutoApply(Behavior.AuditQuery)]
public class QueryProjectTasks : QueryDb<ProjectTask> {
    public int? ClientId {get; set;}
}

[ValidateIsAuthenticated]
[Tag("Projects")]
[AutoApply(Behavior.AuditCreate)]
public class CreateProjectTask : ICreateDb<ProjectTask>, IReturn<CRUDResponse>
{
    public int ProjectId { get; set; }
    public int No { get; set; }
    public string TaskName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal DurationDays { get; set; }
    public DateTime? PlanStart { get; set; }
    public DateTime? PlanEnd { get; set; }
    public DateTime? ActualStart { get; set; }
    public DateTime? ActualEnd { get; set; }
    // In Percent
    public decimal Completed { get; set; }

    [ApiAllowableValues(typeof(TASK_STATUS))]
    public TASK_STATUS Status { get; set; }

    public int? ProjectTeamId { get; set;}
}

[ValidateIsAuthenticated]
[Tag("Projects")]
[AutoApply(Behavior.AuditModify)]
public class UpdateProjectTask : IPatchDb<ProjectTask>, IReturn<CRUDResponse>
{
    public int Id { get; set; } 
    public int ProjectId { get; set; }
    public int No { get; set; }
    public string TaskName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal DurationDays { get; set; }
    public DateTime? PlanStart { get; set; }
    public DateTime? PlanEnd { get; set; }
    public DateTime? ActualStart { get; set; }
    public DateTime? ActualEnd { get; set; }
    // In Percent
    public decimal Completed { get; set; }

    [ApiAllowableValues(typeof(TASK_STATUS))]
    public TASK_STATUS Status { get; set; }

    [AutoDefault(Eval = null)]
    public int? ProjectTeamId { get; set;}
}

[ValidateIsAuthenticated]
[Tag("Projects")]
// [AutoApply(Behavior.AuditSoftDelete)]
public class DeleteProjectTask : IDeleteDb<ProjectTask>, IReturnVoid
{
    public int Id { get; set; }        
}
