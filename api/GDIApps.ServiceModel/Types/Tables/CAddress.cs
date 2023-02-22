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