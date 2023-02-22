using ServiceStack;
using ServiceStack.Configuration;

using System.Collections;
using System.Collections.Generic;
using GDIApps.ServiceModel.Types;
using ServiceStack.DataAnnotations;

namespace GDIApps.ServiceModel;

[ValidateIsAuthenticated]
[Tag("Invoices")]
[AutoApply(Behavior.AuditQuery)]
public class QueryInvoiceDetails : QueryDb<InvoiceDetail> {
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
public class CreateInvoiceDetail : ICreateDb<InvoiceDetail>, IReturn<CRUDResponse>
{
    public int InvoiceId { get; set;}
    public string No { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Amount { get; set; }
}

[ValidateIsAuthenticated]
[Tag("Invoices")]
[AutoApply(Behavior.AuditModify)]
public class UpdateInvoiceDetail : IPatchDb<InvoiceDetail>, IReturn<CRUDResponse>
{
    public int Id { get; set; } 
    public int? InvoiceId { get; set;}
    public string? No { get; set; } 
    public string? Description { get; set; }
    public decimal? Amount { get; set; }
}

[ValidateIsAuthenticated]
[Tag("Invoices")]
// [AutoApply(Behavior.AuditSoftDelete)]
public class DeleteInvoiceDetail : IDeleteDb<InvoiceDetail>, IReturnVoid
{
    public int Id { get; set; }        
}