using System;
using System.Collections.Generic;
using ServiceStack;
using ServiceStack.DataAnnotations;

namespace GDIApps.ServiceModel.Types;

[CompositeIndex(true, nameof(EmployeeReviewId), nameof(ReviewType), nameof(Category), nameof(No))]
public class EmployeeReviewDetail
{
    [AutoIncrement]
    public int Id { get; set; }

    [Required]
    [References(typeof(EmployeeReview))]
    public int EmployeeReviewId { get; set;}

    [Required]
    public REVIEW_TYPE ReviewType { get; set; }

    [Required]
    public REVIEW_QUESTION_CATEGORY Category { get; set; }

    [Required]
    public int No { get; set; } 

    [Required]
    [StringLength(4000)]
    public string Question { get; set; } = string.Empty;

    [StringLength(4000)]
    public int QuestionValue { get; set; }
}



[ValidateIsAuthenticated]
[Tag("Reviews")]
// [AutoApply(Behavior.AuditQuery)]
public class QueryEmployeeReviewDetails : QueryDb<EmployeeReviewDetail> {
}

[ValidateIsAuthenticated]
[Tag("Reviews")]
[AutoApply(Behavior.AuditCreate)]
public class CreateEmployeeReviewDetail : ICreateDb<EmployeeReviewDetail>, IReturn<CRUDResponse>
{
    public int EmployeeReviewId { get; set;}
    [ApiAllowableValues(typeof(REVIEW_TYPE))]
    public REVIEW_TYPE ReviewType { get; set; }
    [ApiAllowableValues(typeof(REVIEW_QUESTION_CATEGORY))]
    public REVIEW_QUESTION_CATEGORY Category { get; set; }
    public int No { get; set; } 
    public string Question { get; set; } = string.Empty;
    public int QuestionValue { get; set; }
}

[ValidateIsAuthenticated]
[Tag("Reviews")]
// [AutoApply(Behavior.AuditModify)]
public class UpdateEmployeeReviewDetail : IPatchDb<EmployeeReviewDetail>, IReturn<CRUDResponse>
{
    public int Id { get; set; } 
    public int EmployeeReviewId { get; set;}
    [ApiAllowableValues(typeof(REVIEW_TYPE))]
    public REVIEW_TYPE ReviewType { get; set; }
    [ApiAllowableValues(typeof(REVIEW_QUESTION_CATEGORY))]
    public REVIEW_QUESTION_CATEGORY Category { get; set; }
    public int No { get; set; } 
    public string Question { get; set; } = string.Empty;
    public int QuestionValue { get; set; }
}

[ValidateIsAuthenticated]
[Tag("Reviews")]
public class DeleteEmployeeReviewDetail : IDeleteDb<EmployeeReviewDetail>, IReturnVoid
{
    public int Id { get; set; }        
}
