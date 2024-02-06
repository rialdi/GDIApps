using System;
using System.Collections.Generic;
using ServiceStack;
using ServiceStack.DataAnnotations;

namespace GDIApps.ServiceModel.Types;

public class DailyScrumMeeting : AuditBase
{
    [AutoIncrement]
    public int Id { get; set; }  

    [Required]
    [References(typeof(AppUser))]
    public int AppUserId { get; set;}

    [Required]
    public DateTime MeetingDate { get; set; }

    [Required]
    [StringLength(4000)]
    public string Blockers { get; set; } =  string.Empty;

    [Required]
    [StringLength(4000)]
    public string WhatDidYouDoYesterday { get; set; } =  string.Empty;

    [Required]
    [StringLength(4000)]
    public string WhatGoalsToday { get; set; } =  string.Empty;
}

[ValidateIsAuthenticated]
[Tag("Reviews")]
[AutoApply(Behavior.AuditQuery)]
public class QueryDailyScrumMeetings : QueryDb<DailyScrumMeeting> {
    public int? AppUserId { get; set;}
}

[ValidateIsAuthenticated]
[Tag("Reviews")]
[AutoApply(Behavior.AuditCreate)]
public class CreateDailyScrumMeeting : ICreateDb<DailyScrumMeeting>, IReturn<CRUDResponse>
{
    #nullable disable
    public int AppUserId { get; set;}
    public DateTime MeetingDate { get; set; }
    public string Blockers { get; set; } =  string.Empty;
    public string WhatDidYouDoYesterday { get; set; } =  string.Empty;
    public string WhatGoalsToday { get; set; } =  string.Empty;
    #nullable restore
}

[ValidateIsAuthenticated]
[Tag("Reviews")]
[AutoApply(Behavior.AuditModify)]
public class UpdateDailyScrumMeeting : IPatchDb<DailyScrumMeeting>, IReturn<CRUDResponse>
{
    #nullable disable
    public int Id { get; set; } 
    public int AppUserId { get; set;}
    public DateTime MeetingDate { get; set; }
    public string Blockers { get; set; }
    public string WhatDidYouDoYesterday { get; set; }
    public string WhatGoalsToday { get; set; }
    #nullable restore
}

[ValidateIsAuthenticated]
[Tag("Reviews")]
public class DeleteDailyScrumMeeting : IDeleteDb<DailyScrumMeeting>, IReturnVoid
{
    public int Id { get; set; }        
}
