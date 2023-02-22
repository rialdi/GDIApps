using System;
using System.Collections.Generic;
using ServiceStack;
using ServiceStack.DataAnnotations;

namespace GDIApps.ServiceModel.Types;

[CompositeIndex(true, nameof(CContractId), nameof(InvoiceNo))]
public class Invoice : AuditBase
{
    [AutoIncrement]
    public int Id { get; set; }  

    [Required]
    [References(typeof(Client))]
    public int ClientId { get; set;}

    [References(typeof(CContract))]
    public int CContractId { get; set;}

    [References(typeof(CBank))]
    public int CBankId { get; set;}

    [References(typeof(CAddress))]
    public int CAddressId { get; set;}

    [Required]
    [StringLength(100)]    
    public string InvoiceNo { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]    
    public string PaymentTerm { get; set; } = string.Empty;

    [Required]
    public DateTime InvoiceDate { get; set; }

    [StringLength(1000)]    
    public string? Description { get; set; }

    [StringLength(100)]  
    public string? PONumber { get; set; }

    [StringLength(100)]
    public string? VAT { get; set; }
    public string? WHT { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal VATAmount { get; set; }
    
    public string? InvoiceStatus { get; set; }
}

public class InvoiceView : Invoice
{
    public string ClientCode { get; set; } = string.Empty;
    public string ClientName { get; set; } = string.Empty;
    public string? CContractContractNo { get; set; }
}