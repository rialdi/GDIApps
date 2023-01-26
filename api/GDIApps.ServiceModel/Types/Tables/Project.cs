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

    [Reference]
    public Client ProjectClient { get; set; }

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
    
    [Default(typeof(bool), "true")]
    public bool? IsActive { get; set; }
}