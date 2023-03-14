using System;
using System.Collections.Generic;
using ServiceStack;
using ServiceStack.DataAnnotations;

namespace GDIApps.ServiceModel.Types;

[CompositeIndex(true, nameof(TSDate), nameof(AppUserId), nameof(ClientId), nameof(ProjectId), nameof(No))]
public class TimeSheet : AuditBase
{
    [AutoIncrement]
    public int Id { get; set; }  

    [Required]
    public DateTime TSDate { get; set; }

    [Required]
    [References(typeof(AppUser))]
    public int AppUserId { get; set;}

    [Required]
    [References(typeof(Client))]
    public int  ClientId { get; set; }

    [Required]
    [References(typeof(Project))]
    public int  ProjectId { get; set; }

    public int No {get; set;}

    [Required]
    [StringLength(1000)] 
    public string TaskName { get; set; } = string.Empty;


    // [Reference]
    // public Project Project { get; set;}

    // [Reference]
    // public AppUser AppUser { get; set;}

    // [ReferenceField(typeof(AppUser),"AppUserId","UserName")]
    // public string AppUserUserName { get; set;}
}

public class TimeSheetView : TimeSheet
{
    #nullable disable

    public string AppUserUserName { get; set;}
    public string AppUserFullName { get; set;}
    public string ClientCode { get; set;} 
    public string ClientName { get; set;} 
    public string ProjectCode { get; set;} 
    public string ProjectName { get; set;} 

    #nullable restore
}

[ValidateIsAuthenticated]
[Tag("TimeSheets")]
[AutoApply(Behavior.AuditQuery)]
public class QueryTimeSheets : QueryDb<TimeSheet, TimeSheetView>, 
    IJoin<TimeSheet, AppUser>, 
    IJoin<TimeSheet, Client>, 
    IJoin<TimeSheet, Project> {
    public int[]? AppUserIds { get; set; }
    public DateTime[]? TSDateBetween {get; set;}
    public int[]? ClientIds { get; set; }
    public int[]? ProjectIds { get; set; }
}

[ValidateIsAuthenticated]
[Tag("TimeSheets")]
[AutoApply(Behavior.AuditCreate)]
public class CreateTimeSheet : ICreateDb<TimeSheet>, IReturn<CRUDResponse>
{  
    public DateTime TSDate { get; set; }
    public int AppUserId { get; set;}
    public int  ClientId { get; set; }
    public int  ProjectId { get; set; }
    public int No {get; set;}
    public string TaskName { get; set; } = string.Empty;

}

[ValidateIsAuthenticated]
[Tag("TimeSheets")]
[AutoApply(Behavior.AuditModify)]
public class UpdateTimeSheet : IPatchDb<TimeSheet>, IReturn<CRUDResponse>
{
    public int Id { get; set; } 
    public DateTime TSDate { get; set; }
    public int AppUserId { get; set;}
    public int  ClientId { get; set; }
    public int  ProjectId { get; set; }
    public int No {get; set;}
    public string TaskName { get; set; } = string.Empty;
}

[ValidateIsAuthenticated]
[Tag("TimeSheets")]
public class DeleteTimeSheet : IDeleteDb<TimeSheet>, IReturnVoid
{
    public int Id { get; set; }        
}

