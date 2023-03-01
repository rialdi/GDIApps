using ServiceStack;
using ServiceStack.Configuration;

using System.Collections;
using System.Collections.Generic;
using GDIApps.ServiceModel.Types;
using ServiceStack.DataAnnotations;
using System;
using System.Runtime.Serialization;

namespace GDIApps.ServiceModel;

[ValidateIsAuthenticated]
[Tag("Invoices")]
[AutoApply(Behavior.AuditQuery)]
public class QueryInvoices : QueryDb<Invoice, InvoiceView>, 
    IJoin<Invoice, Client>, 
    IJoin<Invoice, CContract>,
    IJoin<Invoice, CBank>,
    IJoin<Invoice, CAddress> {
    public int? ClientId {get; set;}
    // public int[]? Ids {get; set;} = null;
    // public string[]? Codes {get; set;}
    // public string? CodeEndsWith {get; set;}
    // public string? Name {get; set;}
    // [Default(typeof(bool), "true")]
    // public bool? IsActive { get; set; }
}

[ValidateIsAuthenticated]
[Tag("Invoices")]
[AutoApply(Behavior.AuditCreate)]
public class CreateInvoice : ICreateDb<Invoice>, IReturn<CRUDResponse>
{
    public int ClientId { get; set;}
    public int? CContractId { get; set;}
    public int? CBankId { get; set;}
    public int? CAddressId { get; set;}
    public string InvoiceNo { get; set; } = string.Empty;
    public int? PaymentTermDays { get; set; }
    public DateTime InvoiceDate { get; set; }
    public string? Description { get; set; }
    public string? PONumber { get; set; }
    public string? VAT { get; set; }
    public string? WHT { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal? VATAmount { get; set; }
    public string? InvoiceStatus { get; set; }
}

[ValidateIsAuthenticated]
[Tag("Invoices")]
[AutoApply(Behavior.AuditModify)]
public class UpdateInvoice : IPatchDb<Invoice>, IReturn<CRUDResponse>
{
    public int Id { get; set; } 
    public int ClientId { get; set;}
    [AutoDefault(Eval = null)]
    public int? CContractId { get; set;}
    [AutoDefault(Eval = null)]
    public int? CBankId { get; set;}
    [AutoDefault(Eval = null)]
    public int? CAddressId { get; set;}
    [AutoDefault(Eval = null)]
    public string? InvoiceNo { get; set; }
    [AutoDefault(Eval = null)]
    public int? PaymentTermDays { get; set; }
    [AutoDefault(Eval = null)]
    public DateTime? InvoiceDate { get; set; }
    [AutoDefault(Eval = null)]
    public string? Description { get; set; }
    [AutoDefault(Eval = null)]
    public string? PONumber { get; set; }
    [AutoDefault(Eval = null)]
    public string? VAT { get; set; }
    [AutoDefault(Eval = null)]
    public string? WHT { get; set; }
    public decimal? TotalAmount { get; set; }
    [AutoDefault(Eval = null)]
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

[Tag("Invoices")]
[ValidateIsAuthenticated]
public class CreateInvoiceAttachment : ICreateDb<InvoiceAttachment>, IReturn<CRUDResponse>
{
    public int InvoiceId { get; set; }
    public string FileName { get; set; } = string.Empty;
    [Input(Type = "file"), UploadTo("invoiceAttachments")]
    public string? AttachmentUrl { get; set; }
}

[ValidateIsAuthenticated]
[Tag("Invoices")]
public class DeleteInvoiceAttachment
{
    public int Id { get; set; }        
}

[ValidateIsAuthenticated]
[Tag("Invoices")]
public class QueryInvoiceAttachments : QueryDb<InvoiceAttachment>{
    public int InvoiceId {get; set;}
}