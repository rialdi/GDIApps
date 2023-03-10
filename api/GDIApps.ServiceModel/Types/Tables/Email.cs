using System;
using ServiceStack;
using ServiceStack.DataAnnotations;

namespace GDIApps.ServiceModel.Types;

[Description("Emails")]
[Notes("Captures an Email Sending from application")]
public class Email : AuditBase
{
    [AutoIncrement]
    [Required]
    public int Id { get; set; }
    [Required]
    public EMAIL_TEMPLATE_CODE Code { get; set; } = EMAIL_TEMPLATE_CODE.APPUSER_REGISTRATION;
    public string From { get; set; } = string.Empty;
    [Required]
    public string To { get; set; } = string.Empty;
    [Required]
    public string Subject { get; set; } = string.Empty;
    [Required]
    [StringLength(Int32.MaxValue)]
    public string Body { get; set; } = string.Empty;
    public string SendStatusMessage { get; set; } = string.Empty;
}

[ValidateIsAuthenticated]
[Tag("Emails")]
[AutoApply(Behavior.AuditQuery)]
public class QueryEmails : QueryDb<Email> {
    public string? ToContains { get; set; } 
    public string? SubjectContains { get; set; }
}

[Tag("Emails")]
[ValidateIsAuthenticated]
public class CreateEmail : ICreateDb<Email>, IReturn<CRUDResponse>
{
    public EMAIL_TEMPLATE_CODE Code { get; set; } = EMAIL_TEMPLATE_CODE.APPUSER_REGISTRATION;
    public string From { get; set; } = string.Empty;
    public string To { get; set; } = string.Empty;
    public string Subject { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
    public string SendStatusMessage { get; set; } = string.Empty;
}

[ValidateIsAuthenticated]
[Tag("Emails")]
public class UpdateEmail : IPatchDb<Email>, IReturn<CRUDResponse>
{
    public int Id { get; set; } 
    public EMAIL_TEMPLATE_CODE Code { get; set; } = EMAIL_TEMPLATE_CODE.APPUSER_REGISTRATION;
    public string From { get; set; } = string.Empty;
    public string To { get; set; } = string.Empty;
    public string Subject { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
    public string SendStatusMessage { get; set; } = string.Empty;
}

[ValidateIsAuthenticated]
[Tag("Emails")]
public class DeleteEmail : IDeleteDb<Project>, IReturnVoid
{
    public int Id { get; set; }        
}