using ServiceStack;
using ServiceStack.Configuration;

using System.Collections;
using System.Collections.Generic;
using GDIApps.ServiceModel.Types;
using ServiceStack.DataAnnotations;
using System;

namespace GDIApps.ServiceModel;

[ValidateIsAuthenticated]
[Tag("Invoices")]
[AutoApply(Behavior.AuditQuery)]
public class QueryInvoices : QueryDb<Invoice> {
    public int[]? Ids {get; set;} = null;
    public string[]? Codes {get; set;}
    public string? CodeEndsWith {get; set;}
    public string? Name {get; set;}
    [Default(typeof(bool), "true")]
    public bool? IsActive { get; set; }
}

[ValidateIsAuthenticated]
[Tag("Invoices")]
[AutoApply(Behavior.AuditCreate)]
public class CreateInvoice : ICreateDb<Invoice>, IReturn<CRUDResponse>
{
    public int ClientId { get; set;}
    public int CContractId { get; set;}
    public int CBankId { get; set;}
    public int CAddressId { get; set;}
    public string InvoiceNo { get; set; } = string.Empty;
    public string PaymentTerm { get; set; } = string.Empty;
    public DateTime InvoiceDate { get; set; }
    public string? Description { get; set; }
    public string? PONumber { get; set; }
    public string? VAT { get; set; }
    public string? WHT { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal VATAmount { get; set; }
    public string? InvoiceStatus { get; set; }
}

[ValidateIsAuthenticated]
[Tag("Invoices")]
[AutoApply(Behavior.AuditModify)]
public class UpdateInvoice : IPatchDb<Invoice>, IReturn<CRUDResponse>
{
    public int Id { get; set; } 
    public int? ClientId { get; set;}
    public int? CContractId { get; set;}
    public int? CBankId { get; set;}
    public int? CAddressId { get; set;}
    public string? InvoiceNo { get; set; }
    public string? PaymentTerm { get; set; }
    public DateTime? InvoiceDate { get; set; }
    public string? Description { get; set; }
    public string? PONumber { get; set; }
    public string? VAT { get; set; }
    public string? WHT { get; set; }
    public decimal? TotalAmount { get; set; }
    public decimal? VATAmount { get; set; }
    public string? InvoiceStatus { get; set; }
}

[ValidateIsAuthenticated]
[Tag("Invoices")]
// [AutoApply(Behavior.AuditSoftDelete)]
public class DeleteInvoice : IDeleteDb<Invoice>, IReturnVoid
{
    public int Id { get; set; }        
}