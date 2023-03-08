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

[ValidateIsAuthenticated]
[Tag("LookupData")]
[AutoApply(Behavior.AuditQuery)]
public class QueryLookups : QueryDb<Lookup> {
    public LOOKUPTYPE? LookupType { get; set; } 
    public string[]? LookupValues { get; set; }
    
}

[ValidateIsAuthenticated]
[Tag("LookupData")]
[AutoApply(Behavior.AuditCreate)]
public class CreateLookup : ICreateDb<Lookup>, IReturn<CRUDResponse>
{
    [ApiAllowableValues(typeof(LOOKUPTYPE))]
    public LOOKUPTYPE LookupType { get; set; }  
    public string LookupValue { get; set; } = string.Empty;
    public string LookupText { get; set; } = string.Empty;
    public bool? IsActive { get; set; }
}

[ValidateIsAuthenticated]
[Tag("LookupData")]
[AutoApply(Behavior.AuditModify)]
public class UpdateLookup : IPatchDb<Lookup>, IReturn<CRUDResponse>
{
    public int? Id { get; set; } 
    [ApiAllowableValues(typeof(LOOKUPTYPE))]
    public LOOKUPTYPE LookupType { get; set; }  
    public string LookupValue { get; set; } = string.Empty;
    public string LookupText { get; set; } = string.Empty;
    public bool? IsActive { get; set; }
}

[ValidateIsAuthenticated]
[Tag("LookupData")]
// [AutoApply(Behavior.d)]
public class DeleteLookup : IDeleteDb<Lookup>, IReturnVoid
{
    public int Id { get; set; }        
}