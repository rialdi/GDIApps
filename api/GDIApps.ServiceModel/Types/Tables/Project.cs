using System;
using System.Collections.Generic;
using ServiceStack;
using ServiceStack.DataAnnotations;

namespace GDIApps.ServiceModel.Types;

[CompositeIndex(true, nameof(Code))]
public class Project : AuditBase
{
    [AutoIncrement]
    public int Id { get; set; }  

    [Required]
    [References(typeof(Client))]
    public int ClientId { get; set;}

    [References(typeof(CContract))]
    public int? CContractId { get; set;}

    [Required]
    [StringLength(100)]    
    public string Code { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]    
    public string Name { get; set; } = string.Empty;

    [StringLength(1000)]    
    public string? Description { get; set; }
    [StringLength(100)]  
    public string? ProjectOwner { get; set; }
    [StringLength(100)]
    public string? ProjectManager { get; set; }
    public decimal? NominalValue { get; set; }
    public int? DurationDays { get; set; }
    public DateTime? EstimatedStartDate { get; set; }
    public DateTime? EstimatedEndDate { get; set; }
    public DateTime? ActualtartDate { get; set; }
    public DateTime? ActualEndDate { get; set; }
    
    // [Default(typeof(bool), "true")]
    public bool? IsActive { get; set; }
}

public class ProjectView : Project
{
    public string ClientCode { get; set; } = string.Empty;
    public string ClientName { get; set; } = string.Empty;
    public string? CContractContractNo { get; set; }
}


[ValidateIsAuthenticated]
[Tag("Projects")]
[AutoApply(Behavior.AuditQuery)]
public class QueryProjects : QueryDb<Project, ProjectView>, IJoin<Project, Client>  {
    public string? Code { get; set; }
    public int? ClientId { get; set; }
    // public int? CContractId { get; set; }
    // public string? CContractContractNoContains { get; set; }

    [ValidateNull]
    public string[]? ClientCodes {get; set;}

    public string? ClientCodeContains { get; set; }
    public string? ClientNameContains { get; set; }

    public string? NameContains { get; set; }
    public string? CodeContains { get; set; }

    public bool IsActive { get; set; } = true;
}

[ValidateIsAuthenticated]
[Tag("Projects")]
[AutoApply(Behavior.AuditCreate)]
public class CreateProject : ICreateDb<Project>, IReturn<CRUDResponse>
{
    public int ClientId { get; set; }
    public int? CContractId { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? ProjectOwner { get; set; }
    public string? ProjectManager { get; set; }
    public decimal? NominalValue { get; set; }
    public int? DurationDays { get; set; }
    public DateTime? EstimatedStartDate { get; set; }
    public DateTime? EstimatedEndDate { get; set; }
    public DateTime? ActualtartDate { get; set; }
    public DateTime? ActualEndDate { get; set; }
    public bool? IsActive { get; set; }
}

[ValidateIsAuthenticated]
[Tag("Projects")]
[AutoApply(Behavior.AuditModify)]
public class UpdateProject : IPatchDb<Project>, IReturn<CRUDResponse>
{
    public int Id { get; set; } 
    public int? ClientId { get; set; }
    [AutoDefault(Eval = null)]
    public int? CContractId { get; set; }
    public string? Code { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? ProjectOwner { get; set; }
    public string? ProjectManager { get; set; }
    public decimal? NominalValue { get; set; }
    public int? DurationDays { get; set; }
    public DateTime? EstimatedStartDate { get; set; }
    public DateTime? EstimatedEndDate { get; set; }
    public DateTime? ActualtartDate { get; set; }
    public DateTime? ActualEndDate { get; set; }
    public bool? IsActive { get; set; }
}

[ValidateIsAuthenticated]
[Tag("Projects")]
// [AutoApply(Behavior.AuditSoftDelete)]
public class DeleteProject : IDeleteDb<Project>, IReturnVoid
{
    public int Id { get; set; }        
}