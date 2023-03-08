using System;
using ServiceStack;
using ServiceStack.DataAnnotations;

namespace GDIApps.ServiceModel.Types;

[CompositeIndex(true, nameof(Code))]
public class EmailTemplate : AuditBase
{
    [AutoIncrement]
    public int Id { get; set; }  

    [Required]
    [StringLength(100)]    
    public EMAIL_TEMPLATE_CODE Code { get; set; }

    [Required]
    [StringLength(1000)]    
    public string Description { get; set; } = string.Empty;

    // [Default(typeof(bool), "true")]
    public bool? IsActive { get; set; }

    [Required]
    [StringLength(Int32.MaxValue)]
    public string EmailTemplateText { get; set; } = string.Empty;

    [Required]
    [StringLength(Int32.MaxValue)]
    public string HtmlTemplate { get; set; } = string.Empty;
}

[ValidateIsAuthenticated]
[Tag("Emails")]
[AutoApply(Behavior.AuditQuery)]
public class QueryEmailTemplates : QueryDb<EmailTemplate> {
    public EMAIL_TEMPLATE_CODE? Code {get; set;}
}

[ValidateIsAuthenticated]
[Tag("Emails")]
[AutoApply(Behavior.AuditCreate)]
public class CreateEmailTemplate : ICreateDb<EmailTemplate>, IReturn<CRUDResponse>
{
    [ApiAllowableValues(typeof(EMAIL_TEMPLATE_CODE))]
    public EMAIL_TEMPLATE_CODE Code { get; set; }  
    public string Description { get; set; } = string.Empty;
    public string EmailTemplateText { get; set; } = string.Empty;
    public string HtmlTemplate { get; set; } = string.Empty;
    public bool? IsActive { get; set; }
}

[ValidateIsAuthenticated]
[Tag("Emails")]
[AutoApply(Behavior.AuditModify)]
public class UpdateEmailTemplate : IPatchDb<EmailTemplate>, IReturn<CRUDResponse>
{
    public int? Id { get; set; } 
    [ApiAllowableValues(typeof(EMAIL_TEMPLATE_CODE))]
    public EMAIL_TEMPLATE_CODE Code { get; set; }  
    public string Description { get; set; } = string.Empty;
    public string EmailTemplateText { get; set; } = string.Empty;
    public bool? IsActive { get; set; }
}

[ValidateIsAuthenticated]
[Tag("Emails")]
// [AutoApply(Behavior.AuditSoftDelete)]
public class DeleteEmailTemplate : IDeleteDb<EmailTemplate>, IReturnVoid
{
    public int Id { get; set; }        
}