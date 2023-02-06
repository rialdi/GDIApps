using System;
using System.Collections.Generic;
using ServiceStack;
using ServiceStack.DataAnnotations;

namespace GDIApps.ServiceModel.Types;

[CompositeIndex(true, nameof(Code))]
public class Client : AuditBase
{
    [AutoIncrement]
    public int Id { get; set; }  

    [Required]
    [StringLength(100)]    
    public string Code { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]    
    public string Name { get; set; } = string.Empty;

    [Required]
    [StringLength(1000)]    
    public string Description { get; set; } = string.Empty;

    [Default(typeof(bool), "true")]
    public bool? IsActive { get; set; }

    [Reference]
    public List<Project>? ProjectList { get; set; }
}