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

    [References(typeof(ProjectPlan))]
    public int ProjectPlanId { get; set; }

    [Required]
    public int No { get; set; }

    [Required]
    [StringLength(100)] 
    public string TaskCode { get; set; } = string.Empty;
    
    [Required]
    [StringLength(200)] 
    public string TaskName { get; set; } = string.Empty;

    [StringLength(1000)] 
    public string Description { get; set; } = string.Empty;

    public decimal? DurationDays { get; set; }

    public DateTime? DueDate { get; set; }
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

    [StringLength(1000)]
    public TASK_LABEL TaskLabel { get; set; }

    [References(typeof(ProjectTeam))]
    public int? ProjectTeamId { get; set; }

    public bool? IsArchived { get; set; }
}

public class ProjectTaskView : ProjectTask
{
    public string ProjectCode { get; set; } = string.Empty;
    public string ProjectName { get; set; } = string.Empty;
    public bool ProjectIsActive { get; set; }
}

[ValidateIsAuthenticated]
[Tag("Projects")]
[AutoApply(Behavior.AuditQuery)]
public class QueryProjectTasks : QueryDb<ProjectTask, ProjectTaskView>, IJoin<ProjectTask, Project> {
    public int? ProjectId {get; set;}
}

[ValidateIsAuthenticated]
[Tag("Projects")]
[AutoApply(Behavior.AuditCreate)]
public class CreateProjectTask : ICreateDb<ProjectTask>, IReturn<CRUDResponse>
{
    // #pragma warning disable CS8618 
    #nullable disable
    public int ProjectId { get; set; }
    public int ProjectPlanId { get; set; }
    public int No { get; set; }
    public string TaskCode { get; set; }
    public string TaskName { get; set; }
    public string Description { get; set; }
    public decimal? DurationDays { get; set; }
    public DateTime? DueDate { get; set; }
    public DateTime? PlanStart { get; set; }
    public DateTime? PlanEnd { get; set; }
    public DateTime? ActualStart { get; set; }
    public DateTime? ActualEnd { get; set; }
    public decimal Completed { get; set; }
    [ApiAllowableValues(typeof(TASK_STATUS))]
    public TASK_STATUS Status { get; set; }
    [ApiAllowableValues(typeof(TASK_LABEL))]
    public TASK_LABEL TaskLabel { get; set; }
    public int? ProjectTeamId { get; set; }
    public bool? IsArchived { get; set; }

    #nullable restore
    // #pragma warning restore CS8618 
}

[ValidateIsAuthenticated]
[Tag("Projects")]
[AutoApply(Behavior.AuditModify)]
public class UpdateProjectTask : IPatchDb<ProjectTask>, IReturn<CRUDResponse>
{
    // #pragma warning disable CS8618 
    #nullable disable
    public int Id { get; set; } 
    public int ProjectId { get; set; }
    public int ProjectPlanId { get; set; }
    public int No { get; set; }
    public string TaskCode { get; set; }
    public string TaskName { get; set; }
    public string Description { get; set; }
    public decimal? DurationDays { get; set; }
    public DateTime? DueDate { get; set; }
    public DateTime? PlanStart { get; set; }
    public DateTime? PlanEnd { get; set; }
    public DateTime? ActualStart { get; set; }
    public DateTime? ActualEnd { get; set; }
    public decimal Completed { get; set; }
    [ApiAllowableValues(typeof(TASK_STATUS))]
    public TASK_STATUS Status { get; set; }
    [ApiAllowableValues(typeof(TASK_LABEL))]
    public TASK_LABEL TaskLabel { get; set; }
    public int? ProjectTeamId { get; set; }
    public bool? IsArchived { get; set; }

    #nullable restore
    // #pragma warning restore CS8618 
}

[ValidateIsAuthenticated]
[Tag("Projects")]
// [AutoApply(Behavior.AuditSoftDelete)]
public class DeleteProjectTask : IDeleteDb<ProjectTask>, IReturnVoid
{
    public int Id { get; set; }        
}
