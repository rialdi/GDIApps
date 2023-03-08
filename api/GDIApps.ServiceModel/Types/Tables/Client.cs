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
    [StringLength(15)]    
    public string PhoneNo { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]    
    public string Email { get; set; } = string.Empty;

    [Required]
    [StringLength(1000)]    
    public string Description { get; set; } = string.Empty;

    // [Default(typeof(bool), "true")]
    public bool? IsActive { get; set; }

    [Reference]
    public List<Project>? ProjectList { get; set; }
    [Reference]
    public List<CContract>? ContractList { get; set; }
}

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
    public string PhoneNo { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool? IsActive { get; set; }
}

[ValidateIsAuthenticated]
[Tag("Clients")]
[AutoApply(Behavior.AuditModify)]
public class UpdateClient : IPatchDb<Client>, IReturn<CRUDResponse>
{
    public int Id { get; set; } 
    public string? Code { get; set; }
    public string? Name { get; set; }
    public string? PhoneNo { get; set; }
    public string? Email { get; set; }
    public string? Description { get; set; }
    public bool? IsActive { get; set; }
}

[ValidateIsAuthenticated]
[Tag("Clients")]
// [AutoApply(Behavior.AuditSoftDelete)]
public class DeleteClient : IDeleteDb<Client>, IReturnVoid
{
    public int Id { get; set; }        
}