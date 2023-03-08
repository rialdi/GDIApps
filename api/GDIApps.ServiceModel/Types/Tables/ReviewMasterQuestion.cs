using System;
using System.Collections.Generic;
using ServiceStack;
using ServiceStack.DataAnnotations;

namespace GDIApps.ServiceModel.Types;

[CompositeIndex(true, nameof(ReviewType), nameof(Category), nameof(No))]
public class ReviewMasterQuestion : AuditBase 
{
    [AutoIncrement]
    public int Id { get; set; }  

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
    public string? Description { get; set; }
}


[ValidateIsAuthenticated]
[Tag("Reviews")]
[AutoApply(Behavior.AuditQuery)]
public class QueryReviewMasterQuestions : QueryDb<ReviewMasterQuestion> {
}

[ValidateIsAuthenticated]
[Tag("Reviews")]
[AutoApply(Behavior.AuditCreate)]
public class CreateReviewMasterQuestion : ICreateDb<ReviewMasterQuestion>, IReturn<CRUDResponse>
{
    [ApiAllowableValues(typeof(REVIEW_TYPE))]
    public REVIEW_TYPE ReviewType { get; set; }

    [ApiAllowableValues(typeof(REVIEW_QUESTION_CATEGORY))]
    public REVIEW_QUESTION_CATEGORY Category { get; set; }

    public int No { get; set; }

    public string Question { get; set; } = string.Empty;

    [AutoDefault(Eval = null)]
    public string? Description { get; set; }
}

[ValidateIsAuthenticated]
[Tag("Reviews")]
[AutoApply(Behavior.AuditModify)]
public class UpdateReviewMasterQuestion : IPatchDb<ReviewMasterQuestion>, IReturn<CRUDResponse>
{
    public int Id { get; set; } 
    
    [ApiAllowableValues(typeof(REVIEW_TYPE))]
    public REVIEW_TYPE ReviewType { get; set; }

    [ApiAllowableValues(typeof(REVIEW_QUESTION_CATEGORY))]
    public REVIEW_QUESTION_CATEGORY Category { get; set; }

    public int No { get; set; }

    public string Question { get; set; } = string.Empty;

    public string? Description { get; set; }
}

[ValidateIsAuthenticated]
[Tag("Reviews")]
public class DeleteReviewMasterQuestion : IDeleteDb<ReviewMasterQuestion>, IReturnVoid
{
    public int Id { get; set; }        
}
