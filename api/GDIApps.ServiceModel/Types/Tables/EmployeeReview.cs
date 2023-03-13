using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using ServiceStack;
using ServiceStack.DataAnnotations;

namespace GDIApps.ServiceModel.Types;

[CompositeIndex(true, nameof(ReviewDate), nameof(ReviewerId), nameof(ReviewedEmployeeId))]
public class EmployeeReview : AuditBase
{
    // #pragma warning disable CS8618 
    #nullable disable

    [AutoIncrement]
    public int Id { get; set; }  
    
    [Required]
    public int PeriodYear { get; set; }

    [Required]
    public string PeriodQuarter { get; set; }

    [Required]
    public DateTime ReviewDate { get; set; }

    [Required]
    [References(typeof(AppUser))]
    public int ReviewerId { get; set;}

    [ReferenceField(typeof(AppUser),"ReviewerId","UserName")]
    public string ReviewerUserName { get; set;} 

    [Required]
    [References(typeof(AppUser))]
    public int ReviewedEmployeeId { get; set;}

    [ReferenceField(typeof(AppUser),"ReviewedEmployeeId","UserName")]
    public string ReviewedEmployeeUserName { get; set;} 


    // [ReferenceField(typeof(AppUser),"ReviewerId","FullName")]
    // public string ReviewerFullName { get; set;} 

    // [Reference]    
    // public AppUser Reviewer {get; set;}
    
    // [Reference]
    // public AppUser ReviewedEmployee {get; set;} 

    public string ReviewNotes { get; set; }

    #nullable restore
    // #pragma warning restore CS8618 

    [Reference]
    public List<EmployeeReviewDetail>? ReviewDetailList { get; set; }
}

public class EmployeeReviewView 
{
    public int Id { get; set; }  
    public int PeriodYear { get; set; }
    public string PeriodQuarter { get; set; } = string.Empty;
    public DateTime ReviewDate { get; set; }
    public int ReviewerId { get; set;}
    public int ReviewedEmployeeId { get; set;}
}



[ValidateIsAuthenticated]
[Tag("Reviews")]
[AutoApply(Behavior.AuditQuery)]
public class QueryEmployeeReviews : QueryDb<EmployeeReview>, IJoin<EmployeeReview, AppUser> {
    public int? ReviewerId { get; set;}
    public string? AppUserUserName { get; set; }
}

[ValidateIsAuthenticated]
[Tag("Reviews")]
[AutoApply(Behavior.AuditCreate)]
public class CreateEmployeeReview : ICreateDb<EmployeeReview>, IReturn<CRUDResponse>
{
    public int PeriodYear { get; set; }
    public string PeriodQuarter { get; set; } = string.Empty;
    public DateTime ReviewDate { get; set; }
    [References(typeof(AppUser))]
    public int AppUserId { get; set;}

    //ID TO BE REVIEWED
    // [References(typeof(AppUser))]
    // public int AppUserId { get; set;}
    public string ReviewNotes { get; set; } = string.Empty;
}

[ValidateIsAuthenticated]
[Tag("Reviews")]
[AutoApply(Behavior.AuditModify)]
public class UpdateEmployeeReview : IPatchDb<EmployeeReview>, IReturn<CRUDResponse>
{
    public int Id { get; set; } 
    public int PeriodYear { get; set; }
    public string PeriodQuarter { get; set; } = string.Empty;
    public DateTime ReviewDate { get; set; }
    [References(typeof(AppUser))]
    public int ReviewerId { get; set;}

    [References(typeof(AppUser))]
    public int ReviewedEmployeeId { get; set;}

    //ID TO BE REVIEWED
    // [References(typeof(AppUser))]
    // public int AppUserId { get; set;}
    public string ReviewNotes { get; set; } = string.Empty;
}

[ValidateIsAuthenticated]
[Tag("Reviews")]
public class DeleteEmployeeReview : IDeleteDb<EmployeeReview>, IReturnVoid
{
    public int Id { get; set; }        
}


