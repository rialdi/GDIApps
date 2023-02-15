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