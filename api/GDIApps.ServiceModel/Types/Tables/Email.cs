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