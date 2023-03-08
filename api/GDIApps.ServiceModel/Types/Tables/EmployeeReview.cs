using System;
using System.Collections.Generic;
using ServiceStack;
using ServiceStack.DataAnnotations;

namespace GDIApps.ServiceModel.Types;

[CompositeIndex(true, nameof(ReviewDate), nameof(ReviewerId), nameof(ReviewedEmployeeId))]
public class EmployeeReview : AuditBase
{
    [AutoIncrement]
    public int Id { get; set; }  

    [Required]
    public int PeriodYear { get; set; }

    [Required]
    public string PeriodQuarter { get; set; } = string.Empty;

    [Required]
    public DateTime ReviewDate { get; set; }

    [Required]
    [References(typeof(AppUser))]
    public int ReviewerId { get; set;}

    [Required]
    [References(typeof(AppUser))]
    public int ReviewedEmployeeId { get; set;}


    public string ReviewNotes { get; set; } = string.Empty;

    [Reference]
    public List<EmployeeReviewDetail>? ReviewDetailList { get; set; }

}



[ValidateIsAuthenticated]
[Tag("Reviews")]
[AutoApply(Behavior.AuditQuery)]
public class QueryEmployeeReviews : QueryDb<EmployeeReview> {
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


