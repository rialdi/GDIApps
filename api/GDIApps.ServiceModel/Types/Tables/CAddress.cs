using System;
using System.Collections.Generic;
using ServiceStack;
using ServiceStack.DataAnnotations;

namespace GDIApps.ServiceModel.Types;

[CompositeIndex(true, nameof(ClientId), nameof(AddressName))]
public class CAddress : AuditBase
{
    [AutoIncrement]
    public int Id { get; set; }  

    [Required]
    [References(typeof(Client))]
    public int ClientId { get; set;}

    [Required]
    [StringLength(100)]    
    public string AddressName { get; set; } = string.Empty;

    [StringLength(100)] 
    public string? Country { get; set; } 

    [StringLength(100)] 
    public string? Province { get; set; } 

    [StringLength(100)]    
    public string? City { get; set; }

    [StringLength(100)]    
    public string? District { get; set; }

    [StringLength(100)]    
    public string? Village { get; set; }

    [StringLength(300)]    
    public string? Address1 { get; set; }

    [StringLength(300)]    
    public string? Address2 { get; set; }

    [StringLength(100)]    
    public string? PostalCode { get; set; }

    [StringLength(100)]    
    public string? PhoneNo { get; set; }

    public bool? IsMain { get; set; }
}

public class CAddressView : CAddress
{
    public string ClientCode { get; set; } = string.Empty;
    public string ClientName { get; set; } = string.Empty;
}

[ValidateIsAuthenticated]
[Tag("Clients")]
[AutoApply(Behavior.AuditQuery)]
public class QueryCAddresss : QueryDb<CAddress, CAddressView>, IJoin<CAddress, Client>  {
    public string? Code { get; set; }
    public int? ClientId { get; set; }

    [ValidateNull]
    public string[]? ClientCodes {get; set;}
    public string? ClientCodeContains { get; set; }
    public string? ClientNameContains { get; set; }
    public string? AddressNameContains { get; set; }
    
}

[ValidateIsAuthenticated]
[Tag("Clients")]
[AutoApply(Behavior.AuditCreate)]
public class CreateCAddress : ICreateDb<CAddress>, IReturn<CRUDResponse>
{
    public int ClientId { get; set;}
    public string AddressName { get; set; } = string.Empty;
    public string? Country { get; set; }
    public string? Province { get; set; }
    public string? City { get; set; }
    public string? District { get; set; }
    public string? Village { get; set; }
    public string? Address1 { get; set; }
    public string? Address2 { get; set; }
    public string? PostalCode { get; set; }
    public string? PhoneNo { get; set; }
    public bool? IsMain { get; set; }
}

[ValidateIsAuthenticated]
[Tag("Clients")]
[AutoApply(Behavior.AuditModify)]
public class UpdateCAddress : IPatchDb<CAddress>, IReturn<CRUDResponse>
{
    public int Id { get; set; } 
    public int ClientId { get; set;}
    public string? AddressName { get; set; }
    
    [AutoDefault(Eval = null)]
    public string? Country { get; set; } 
    
    [AutoDefault(Eval = null)]
    public string? Province { get; set; }

    [AutoDefault(Eval = null)]
    public string? City { get; set; }
    
    [AutoDefault(Eval = null)]
    public string? District { get; set; }
    
    [AutoDefault(Eval = null)]
    public string? Village { get; set; }
    
    public string? Address1 { get; set; }
    public string? Address2 { get; set; }
    [AutoDefault(Eval = null)]
    public string? PostalCode { get; set; }
    [AutoDefault(Eval = null)]
    public string? PhoneNo { get; set; }
    public bool? IsMain { get; set; }
}

[ValidateIsAuthenticated]
[Tag("Clients")]
// [AutoApply(Behavior.AuditSoftDelete)]
public class DeleteCAddress : IDeleteDb<CAddress>, IReturnVoid
{
    public int Id { get; set; }        
}