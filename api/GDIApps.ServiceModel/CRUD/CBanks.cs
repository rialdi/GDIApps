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
public class QueryCBanks : QueryDb<CBank> {
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
public class CreateCBank : ICreateDb<CBank>, IReturn<CRUDResponse>
{
    public int ClientId { get; set;}
    public string BankName { get; set; } = string.Empty;
    public string AccountName { get; set; } = string.Empty;
    public string AccountNo { get; set; } = string.Empty;
    public string Currency { get; set; } = "IDR";
    public string? SwiftCode { get; set; }
    public bool? IsMain { get; set; }
}

[ValidateIsAuthenticated]
[Tag("Clients")]
[AutoApply(Behavior.AuditModify)]
public class UpdateCBank : IPatchDb<CBank>, IReturn<CRUDResponse>
{
    public int Id { get; set; } 
    public int ClientId { get; set;}
    public string? BankName { get; set; }
    public string? AccountName { get; set; }
    public string? AccountNo { get; set; }
    public string? Currency { get; set; }
    public string? SwiftCode { get; set; }
    public bool? IsMain { get; set; }
}

[ValidateIsAuthenticated]
[Tag("Clients")]
// [AutoApply(Behavior.AuditSoftDelete)]
public class DeleteCBank : IDeleteDb<CBank>, IReturnVoid
{
    public int Id { get; set; }        
}