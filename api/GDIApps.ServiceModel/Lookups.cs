using ServiceStack;
using ServiceStack.Configuration;

using System.Collections;
using System.Collections.Generic;
using GDIApps.ServiceModel.Types;


namespace GDIApps.ServiceModel;

[ValidateIsAuthenticated]
[Tag("Lookups")]
[AutoApply(Behavior.AuditQuery)]
public class QueryLookups : QueryDb<Lookup> {
    public LOOKUPTYPE? LOOKUPTYPE {get; set;}
}

[ValidateIsAuthenticated]
[Tag("Lookups")]
[AutoApply(Behavior.AuditCreate)]
public class CreateLookup : ICreateDb<Lookup>, IReturn<CRUDResponse>
{
    [ApiAllowableValues(typeof(LOOKUPTYPE))]
    public LOOKUPTYPE LOOKUPTYPE { get; set; }  
    public string LookupValue { get; set; } = string.Empty;
    public string LookupText { get; set; } = string.Empty;
    public bool? IsActive { get; set; }
}

[ValidateIsAuthenticated]
[Tag("Lookups")]
[AutoApply(Behavior.AuditModify)]
public class UpdateLookup : IPatchDb<Lookup>, IReturn<CRUDResponse>
{
    public int? Id { get; set; } 
    [ApiAllowableValues(typeof(LOOKUPTYPE))]
    public LOOKUPTYPE LOOKUPTYPE { get; set; }  
    public string LookupValue { get; set; } = string.Empty;
    public string LookupText { get; set; } = string.Empty;
    public bool? IsActive { get; set; }
}

[ValidateIsAuthenticated]
[Tag("Lookups")]
[AutoApply(Behavior.AuditSoftDelete)]
public class DeleteLookup : IDeleteDb<Lookup>, IReturnVoid
{
    public int Id { get; set; }        
}