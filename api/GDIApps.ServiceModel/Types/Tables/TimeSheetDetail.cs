using System;
using System.Collections.Generic;
using ServiceStack;
using ServiceStack.DataAnnotations;

namespace GDIApps.ServiceModel.Types;

[CompositeIndex(true, nameof(TimeSheetId), nameof(ProjectId), nameof(TaskName))]
public class TimeSheetDetail : AuditBase
{
    [AutoIncrement]
    public int Id { get; set; }  

    [Required]
    [References(typeof(TimeSheet))]
    public int TimeSheetId { get; set;}

    [References(typeof(Project))]
    public int?  ProjectId { get; set; }

    [Required]
    [StringLength(1000)] 
    public string TaskName { get; set; } = string.Empty;
}

[ValidateIsAuthenticated]
[Tag("TimeSheets")]
[AutoApply(Behavior.AuditQuery)]
public class QueryTimeSheetDetails : QueryDb<TimeSheetDetail> {
}

[ValidateIsAuthenticated]
[Tag("TimeSheets")]
[AutoApply(Behavior.AuditCreate)]
public class CreateTimeSheetDetail : ICreateDb<TimeSheetDetail>, IReturn<CRUDResponse>
{
    public int TimeSheetId { get; set;}
    public int?  ProjectId { get; set; }
    public string TaskName { get; set; } = string.Empty;
}

[ValidateIsAuthenticated]
[Tag("TimeSheets")]
[AutoApply(Behavior.AuditModify)]
public class UpdateTimeSheetDetail : IPatchDb<TimeSheetDetail>, IReturn<CRUDResponse>
{
    public int Id { get; set; }   
    public int TimeSheetId { get; set;}
    [AutoDefault(Eval = null)]
    public int?  ProjectId { get; set; }
    public string TaskName { get; set; } = string.Empty;
}

[ValidateIsAuthenticated]
[Tag("TimeSheets")]
public class DeleteTimeSheetDetail : IDeleteDb<TimeSheetDetail>, IReturnVoid
{
    public int Id { get; set; }        
}