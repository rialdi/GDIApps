using System;
using System.Collections.Generic;
using ServiceStack;
using ServiceStack.DataAnnotations;

namespace GDIApps.ServiceModel.Types;

[CompositeIndex(true, nameof(ClientId), nameof(ProjectRoleName))]
public class CRoleRate : AuditBase
{
    [AutoIncrement]
    public int Id { get; set; }  

    [Required]
    [References(typeof(Client))]
    public int ClientId { get; set;}

    [Required]
    [StringLength(100)]    
    public PROJECT_ROLE_NAME ProjectRoleName { get; set; }

    [Required]
    [StringLength(100)]    
    public string Currency { get; set; } = string.Empty;

    [Required]
    public decimal RateInDay {get; set;}

    public bool? IsActive { get; set; }
}

public class CRoleRateView : CRoleRate
{
    public string ClientCode { get; set; } = string.Empty;
    public string ClientName { get; set; } = string.Empty;
}

[ValidateIsAuthenticated]
[Tag("Clients")]
[AutoApply(Behavior.AuditQuery)]
public class QueryCRoleRates : QueryDb<CRoleRate, CRoleRateView>, IJoin<CRoleRate, Client>  {
    public int? ClientId { get; set; }
    [ValidateNull]
    public string[]? ClientCodes {get; set;}
    public string? ClientCodeContains { get; set; }
    public string? ClientNameContains { get; set; }
    public string? ProjectRoleNameContains { get; set;}
}

[ValidateIsAuthenticated]
[Tag("Clients")]
[AutoApply(Behavior.AuditCreate)]
public class CreateCRoleRate : ICreateDb<CRoleRate>, IReturn<CRUDResponse>
{
    public int ClientId { get; set;}

    [ApiAllowableValues(typeof(PROJECT_TEAM_ROLE))]
    public PROJECT_TEAM_ROLE ProjectRoleName { get; set; }
    public string Currency { get; set; } = "IDR";
    public decimal RateInDay {get; set;}
    public bool? IsActive { get; set; }
}

[ValidateIsAuthenticated]
[Tag("Clients")]
[AutoApply(Behavior.AuditModify)]
public class UpdateCRoleRate : IPatchDb<CRoleRate>, IReturn<CRUDResponse>
{
    public int Id { get; set; } 
    public int ClientId { get; set;}
    
    [ApiAllowableValues(typeof(PROJECT_TEAM_ROLE))]
    public PROJECT_TEAM_ROLE ProjectRoleName { get; set; }
    public string Currency { get; set; } = "IDR";
    public decimal RateInDay {get; set;}
    public bool? IsActive { get; set; }
}

[ValidateIsAuthenticated]
[Tag("Clients")]
// [AutoApply(Behavior.AuditSoftDelete)]
public class DeleteCRoleRate : IDeleteDb<CRoleRate>, IReturnVoid
{
    public int Id { get; set; }        
}