using System;
using System.Collections.Generic;
using ServiceStack;
using ServiceStack.DataAnnotations;

namespace GDIApps.ServiceModel.Types;

[CompositeIndex(true, nameof(ClientId), nameof(BankName), nameof(AccountNo))]
public class CBank : AuditBase
{
    [AutoIncrement]
    public int Id { get; set; }  

    [Required]
    [References(typeof(Client))]
    public int ClientId { get; set;}

    [Required]
    [StringLength(100)]    
    public string BankName { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]    
    public string AccountName { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]    
    public string AccountNo { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]    
    public string Currency { get; set; } = string.Empty;

    [StringLength(100)]    
    public string? SwiftCode { get; set; }
    public bool? IsMain { get; set; }
}

public class CBankView : CBank
{
    public string ClientCode { get; set; } = string.Empty;
    public string ClientName { get; set; } = string.Empty;

    [Compute, Ignore]
    public string BankDisplay {
        get {
            return "[" + BankName + "] " + AccountNo;
        }
    }
}

[ValidateIsAuthenticated]
[Tag("Clients")]
[AutoApply(Behavior.AuditQuery)]
public class QueryCBanks : QueryDb<CBank, CBankView>, IJoin<CBank, Client>  {
    public int? ClientId { get; set; }
    [ValidateNull]
    public string[]? ClientCodes {get; set;}
    public string? ClientCodeContains { get; set; }
    public string? ClientNameContains { get; set; }
    public string? BankNameContains { get; set;}
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
    [AutoDefault(Eval = null)]
    public string? SwiftCode { get; set; }
    [AutoDefault(Eval = null)]
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
    [AutoDefault(Eval = null)]
    public string? SwiftCode { get; set; }
    [AutoDefault(Eval = null)]
    public bool? IsMain { get; set; }
}

[ValidateIsAuthenticated]
[Tag("Clients")]
// [AutoApply(Behavior.AuditSoftDelete)]
public class DeleteCBank : IDeleteDb<CBank>, IReturnVoid
{
    public int Id { get; set; }        
}