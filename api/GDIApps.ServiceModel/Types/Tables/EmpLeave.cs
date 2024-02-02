using System;
using ServiceStack;
using ServiceStack.DataAnnotations;

namespace GDIApps.ServiceModel.Types;

[CompositeIndex(true,  nameof(AppUserId), nameof(Year), nameof(StartDate), nameof(LeaveType))]
public class EmpLeave : AuditBase
{
    [AutoIncrement]
    public int Id { get; set; }  

    [Required]
    [References(typeof(AppUser))]
    public int AppUserId { get; set;}

    [Required]
    public int Year { get; set; }
    [Required]
    public EMPLOYEE_LEAVE_TYPE LeaveType { get; set; }
    [Required]
    public bool IsPlanned { get; set; }
    [Required]
    public DateTime StartDate { get; set; }
    [Required]
    public DateTime EndDate { get; set; }
    [Required]
    public int TotalWorkingDays { get; set; }

    [Required]
    [StringLength(1000)] 
    public string Notes { get; set; } = string.Empty;
    public APPROVAL_STATUS ApprovalStatus { get; set; }
}

[ValidateIsAuthenticated]
[Tag("EmpLeaves")]
[AutoApply(Behavior.AuditQuery)]
public class QueryEmpLeaves : QueryDb<EmpLeave> 
{    
    #nullable disable
    public int AppUserId { get; set; }
    public int Year { get; set; }

    public string LeaveTypes { get; set; }

    public bool? IsPlanned { get; set; }
    
}

[ValidateIsAuthenticated]
[Tag("EmpLeaves")]
[AutoApply(Behavior.AuditCreate)]
public class CreateEmpLeave : ICreateDb<EmpLeave>, IReturn<CRUDResponse>
{  
    #nullable disable
    public int AppUserId { get; set;}

    public int Year { get; set; }
    [ApiAllowableValues(typeof(EMPLOYEE_LEAVE_TYPE))]
    public EMPLOYEE_LEAVE_TYPE? LeaveType { get; set; }
    public bool IsPlanned { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int TotalWorkingDays { get; set; }
    public string Notes { get; set; } = string.Empty;
    [ApiAllowableValues(typeof(APPROVAL_STATUS))]
    public APPROVAL_STATUS? ApprovalStatus { get; set; }
    #nullable restore
}

[ValidateIsAuthenticated]
[Tag("EmpLeaves")]
[AutoApply(Behavior.AuditModify)]
public class UpdateEmpLeave : IPatchDb<EmpLeave>, IReturn<CRUDResponse>
{
    public int Id { get; set; } 
    #nullable disable
    public int AppUserId { get; set;}
    public int Year { get; set; }
    [ApiAllowableValues(typeof(EMPLOYEE_LEAVE_TYPE))]
    public EMPLOYEE_LEAVE_TYPE? LeaveType { get; set; }
    public bool IsPlanned { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int TotalWorkingDays { get; set; }
    public string Notes { get; set; } = string.Empty;
    [ApiAllowableValues(typeof(APPROVAL_STATUS))]
    public APPROVAL_STATUS? ApprovalStatus { get; set; }
    #nullable restore
}

[ValidateIsAuthenticated]
[Tag("EmpLeaves")]
public class DeleteEmpLeave : IDeleteDb<EmpLeave>, IReturnVoid
{
    public int Id { get; set; }        
}

