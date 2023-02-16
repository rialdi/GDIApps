using ServiceStack;
using ServiceStack.Configuration;

using System.Collections;
using System.Collections.Generic;
using GDIApps.ServiceModel.Types;
using ServiceStack.DataAnnotations;

namespace GDIApps.ServiceModel;

[ValidateIsAuthenticated]
[Tag("Clients")]
[AutoApply(Behavior.AuditQuery)]
public class QueryClients : QueryDb<Client> {
    public int[]? Ids {get; set;} = null;
    public string[]? Codes {get; set;}
    public string? CodeEndsWith {get; set;}
    public string? Name {get; set;}
    [Default(typeof(bool), "true")]
    public bool? IsActive { get; set; }
}

[ValidateIsAuthenticated]
[Tag("Clients")]
[AutoApply(Behavior.AuditCreate)]
public class CreateClient : ICreateDb<Client>, IReturn<CRUDResponse>
{
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool? IsActive { get; set; }
}

[ValidateIsAuthenticated]
[Tag("Clients")]
[AutoApply(Behavior.AuditModify)]
public class UpdateClient : IPatchDb<Client>, IReturn<CRUDResponse>
{
    public int? Id { get; set; } 
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool? IsActive { get; set; }
}

[ValidateIsAuthenticated]
[Tag("Clients")]
// [AutoApply(Behavior.AuditSoftDelete)]
public class DeleteClient : IDeleteDb<Client>, IReturnVoid
{
    public int Id { get; set; }        
}