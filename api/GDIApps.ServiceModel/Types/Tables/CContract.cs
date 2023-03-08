using System;
using System.Collections.Generic;
using ServiceStack;
using ServiceStack.DataAnnotations;

namespace GDIApps.ServiceModel.Types;

[CompositeIndex(true, nameof(ClientId), nameof(ContractNo))]
public class CContract : AuditBase
{
    [AutoIncrement]
    public int Id { get; set; }  

    [Required]
    [References(typeof(Client))]
    public int ClientId { get; set;}

    [Required]
    [StringLength(100)]    
    public string ContractNo { get; set; } = string.Empty;

    [Required]
    [StringLength(1000)]    
    public string Description { get; set; } = string.Empty;

    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }

    public Decimal? TotalAmount { get; set; }

    [Required]
    [StringLength(100)]    
    public string Currency { get; set; } = string.Empty;
    public bool? IsActive { get; set; }

    [StringLength(100)]    
    public string? PIC { get; set; } = string.Empty;
    public Decimal? RemainingAmount { get; set; }
    public int? PaymentTermDays { get; set; }

    // [Reference]
    // public List<Project>? ProjectList { get; set; }
}

public class CContractView : CContract
{
    public string ClientCode { get; set; } = string.Empty;
    public string ClientName { get; set; } = string.Empty;
}

[ValidateIsAuthenticated]
[Tag("Clients")]
[AutoApply(Behavior.AuditQuery)]
public class QueryCContracts : QueryDb<CContract, CContractView>, IJoin<CContract, Client> {
    public int? ClientId { get; set; }
    [ValidateNull]
    public string[]? ClientCodes {get; set;}
    public string? ClientCodeContains { get; set; }
    public string? ClientNameContains { get; set; }

    public string? ContractNoContains { get; set;}
}

[ValidateIsAuthenticated]
[Tag("Clients")]
[AutoApply(Behavior.AuditCreate)]
public class CreateCContract : ICreateDb<CContract>, IReturn<CRUDResponse>
{
    public int ClientId { get; set;}
    public string ContractNo { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string Currency { get; set; } = "IDR";
    public decimal? TotalAmount { get; set; }
    public bool? IsActive { get; set; }
}

[ValidateIsAuthenticated]
[Tag("Clients")]
[AutoApply(Behavior.AuditModify)]
public class UpdateCContract : IPatchDb<CContract>, IReturn<CRUDResponse>
{
    public int Id { get; set; } 
    public int ClientId { get; set;}
    public string ContractNo { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string? Currency { get; set; } = "IDR";
    public decimal? TotalAmount { get; set; }
    public bool? IsActive { get; set; }
}

[ValidateIsAuthenticated]
[Tag("Clients")]
// [AutoApply(Behavior.AuditSoftDelete)]
public class DeleteCContract : IDeleteDb<CContract>, IReturnVoid
{
    public int Id { get; set; }        
}