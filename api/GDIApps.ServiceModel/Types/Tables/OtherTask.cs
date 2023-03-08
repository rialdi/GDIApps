using System;
using System.Collections.Generic;
using ServiceStack;
using ServiceStack.DataAnnotations;

namespace GDIApps.ServiceModel.Types;

[CompositeIndex(true, nameof(AppUserId), nameof(PlanStart), nameof(TaskName))]
public class OtherTask : AuditBase
{
    [AutoIncrement]
    public int Id { get; set; }  

    [Required]
    [References(typeof(AppUser))]
    public int AppUserId { get; set;}

    [Required]
    [StringLength(1000)]
    public string TaskName { get; set; } = string.Empty;

    [StringLength(4000)]
    public string TaskDesc { get; set; } = string.Empty;

    [Required]
    public decimal DurationDays { get; set; }
    
    public DateTime? PlanStart { get; set; }
    public DateTime? PlanEnd { get; set; }
    public DateTime? ActualStart { get; set; }
    public DateTime? ActualEnd { get; set; }

    [Required]
    // In Percent
    public decimal Completed { get; set; }

    [Required]
    [StringLength(100)]
    public TASK_STATUS Status { get; set; }     
}

[ValidateIsAuthenticated]
[Tag("OtherTasks")]
[AutoApply(Behavior.AuditQuery)]
public class QueryOtherTasks : QueryDb<OtherTask> {
}

[ValidateIsAuthenticated]
[Tag("OtherTasks")]
[AutoApply(Behavior.AuditCreate)]
public class CreateOtherTask : ICreateDb<OtherTask>, IReturn<CRUDResponse>
{
    public int AppUserId { get; set;}

    public string TaskName { get; set; } = string.Empty;

    public string TaskDesc { get; set; } = string.Empty;

    public decimal DurationDays { get; set; }
    public DateTime? PlanStart { get; set; }
    public DateTime? PlanEnd { get; set; }
    public DateTime? ActualStart { get; set; }
    public DateTime? ActualEnd { get; set; }

    public decimal Completed { get; set; }

    [ApiAllowableValues(typeof(TASK_STATUS))]
    public TASK_STATUS Status { get; set; }    
}

[ValidateIsAuthenticated]
[Tag("OtherTasks")]
[AutoApply(Behavior.AuditModify)]
public class UpdateOtherTask : IPatchDb<OtherTask>, IReturn<CRUDResponse>
{
    public int AppUserId { get; set;}

    public string TaskName { get; set; } = string.Empty;
    public string TaskDesc { get; set; } = string.Empty;

    public decimal DurationDays { get; set; }
    [AutoDefault(Eval = null)]
    public DateTime? PlanStart { get; set; }
    [AutoDefault(Eval = null)]
    public DateTime? PlanEnd { get; set; }
    [AutoDefault(Eval = null)]
    public DateTime? ActualStart { get; set; }
    [AutoDefault(Eval = null)]
    public DateTime? ActualEnd { get; set; }

    public decimal Completed { get; set; }

    [ApiAllowableValues(typeof(TASK_STATUS))]
    public TASK_STATUS Status { get; set; }    
}

[ValidateIsAuthenticated]
[Tag("OtherTasks")]
public class DeleteOtherTask : IDeleteDb<OtherTask>, IReturnVoid
{
    public int Id { get; set; }        
}
