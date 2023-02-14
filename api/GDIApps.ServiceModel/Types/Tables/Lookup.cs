
using System;
using System.Collections.Generic;
using ServiceStack;
using ServiceStack.DataAnnotations;

namespace GDIApps.ServiceModel.Types;

[CompositeIndex(true, nameof(LookupType), nameof(LookupValue))]
public class Lookup : AuditBase
{
    [AutoIncrement]
    public int Id { get; set; }

   
    [Required]
    [StringLength(100)]
    public LOOKUPTYPE LookupType { get; set; }    
    [Required]
    [StringLength(100)]    
    public string LookupValue { get; set; } = string.Empty;
    [Required]
    [StringLength(1000)]
    public string LookupText { get; set; } = string.Empty;
    public bool? IsActive { get; set; }
    [Compute, Ignore]
    public string LookupDisplay {
        get {
            return "[" + LookupValue + "] " + LookupText;
        }
    }
}