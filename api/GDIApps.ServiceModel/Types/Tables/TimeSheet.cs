using System;
using System.Collections.Generic;
using ServiceStack;
using ServiceStack.DataAnnotations;

namespace GDIApps.ServiceModel.Types;

[CompositeIndex(true, nameof(AppUserId), nameof(TSDate))]
public class TimeSheet : AuditBase
{
    [AutoIncrement]
    public int Id { get; set; }  

    [Required]
    [References(typeof(AppUser))]
    public int AppUserId { get; set;}

    [Required]
    public DateTime TSDate { get; set; }

    [Reference]
    public List<TimeSheetDetail>? TSDetailList { get; set; }
}

[ValidateIsAuthenticated]
[Tag("TimeSheets")]
[AutoApply(Behavior.AuditQuery)]
public class QueryTimeSheets : QueryDb<TimeSheet> {
}

[ValidateIsAuthenticated]
[Tag("TimeSheets")]
[AutoApply(Behavior.AuditCreate)]
public class CreateTimeSheet : ICreateDb<TimeSheet>, IReturn<CRUDResponse>
{  
    public int AppUserId { get; set;}

    public DateTime TSDate { get; set; }
}

[ValidateIsAuthenticated]
[Tag("TimeSheets")]
[AutoApply(Behavior.AuditModify)]
public class UpdateTimeSheet : IPatchDb<TimeSheet>, IReturn<CRUDResponse>
{
    public int Id { get; set; } 
    public int AppUserId { get; set;}

    public DateTime TSDate { get; set; }
}

[ValidateIsAuthenticated]
[Tag("TimeSheets")]
public class DeleteTimeSheet : IDeleteDb<TimeSheet>, IReturnVoid
{
    public int Id { get; set; }        
}

