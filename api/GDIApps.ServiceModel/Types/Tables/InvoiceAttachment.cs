
using System;
using System.Collections.Generic;
using ServiceStack;
using ServiceStack.DataAnnotations;

namespace GDIApps.ServiceModel.Types;

public class InvoiceAttachment
{
    [AutoIncrement]
    public int Id { get; set; }  

    [References(typeof(Invoice))]
    public int InvoiceId { get; set; }

    [Required]
    public string FileName { get; set; } = string.Empty;

    [Input(Type = "file"), UploadTo("invoiceAttachments")]
    public string? AttachmentUrl { get; set; }
}

[ValidateIsAuthenticated]
[Tag("Invoices")]
public class QueryInvoiceAttachments : QueryDb<InvoiceAttachment>{
    public int? InvoiceId {get; set;}
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
public class UpdateInvoiceAttachment : IPatchDb<InvoiceAttachment>, IReturn<CRUDResponse>
{
    public int Id { get; set; } 
    public string FileName { get; set; } = string.Empty;
}

[ValidateIsAuthenticated]
[Tag("Invoices")]
public class DeleteInvoiceAttachment : IDeleteDb<Invoice>, IReturnVoid
{
    public int Id { get; set; }        
}

