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
}