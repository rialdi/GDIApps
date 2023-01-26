using ServiceStack;
using ServiceStack.Configuration;

using System.Collections;
using System.Collections.Generic;
using GDIApps.ServiceModel.Types;

namespace GDIApps.ServiceModel;

[ValidateIsAuthenticated]
[Tag("EmailTemplates")]
[AutoApply(Behavior.AuditQuery)]
public class QueryEmailTemplates : QueryDb<EmailTemplate> {
    public EMAIL_TEMPLATE_CODE? Code {get; set;}
}

[ValidateIsAuthenticated]
[Tag("EmailTemplates")]
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
[Tag("EmailTemplates")]
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
[Tag("EmailTemplates")]
[AutoApply(Behavior.AuditSoftDelete)]
public class DeleteEmailTemplate : IDeleteDb<EmailTemplate>, IReturnVoid
{
    public int Id { get; set; }        
}