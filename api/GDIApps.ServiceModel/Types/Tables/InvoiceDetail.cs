using System;
using System.Collections.Generic;
using ServiceStack;
using ServiceStack.DataAnnotations;

namespace GDIApps.ServiceModel.Types;

[CompositeIndex(true, nameof(InvoiceId), nameof(No))]
public class InvoiceDetail : AuditBase
{
    [AutoIncrement]
    public int Id { get; set; }  

    [Required]
    [References(typeof(Invoice))]
    public int InvoiceId { get; set;}

    [Required]
    [StringLength(100)]    
    public string No { get; set; } = string.Empty;

    [Required]
    [StringLength(1000)]    
    public string Description { get; set; } = string.Empty;

     [Required]
    public decimal Amount { get; set; }
}