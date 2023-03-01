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
    public int? CContractId { get; set;}

    [References(typeof(CBank))]
    public int? CBankId { get; set;}

    [References(typeof(CAddress))]
    public int? CAddressId { get; set;}

    [Required]
    [StringLength(100)]    
    public string InvoiceNo { get; set; } = string.Empty;

    [StringLength(100)]    
    public int? PaymentTermDays { get; set; }

    [Required]
    public DateTime InvoiceDate { get; set; }

    [StringLength(1000)]    
    public string? Description { get; set; }

    [StringLength(100)]  
    public string? PONumber { get; set; }

    [StringLength(100)]
    public string? VAT { get; set; }
    [StringLength(100)]
    public string? WHT { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal? VATAmount { get; set; }
    public string? InvoiceStatus { get; set; }

    [Reference]
    public List<InvoiceAttachment> Attachments { get; set; } = new List<InvoiceAttachment>();
}

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

    
    // [Format(FormatMethods.Attachment)]
    // public string FilePath { get; set; } = string.Empty;
    // public string ContentType { get; set; } = string.Empty;
    // [Format(FormatMethods.Bytes)]
    // public long ContentLength { get; set; }
}
public class InvoiceView
{
    public int Id { get; set; }
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

    // Get Client Info
    public int ClientId { get; set;}
    public string ClientCode { get; set; } = string.Empty;
    public string ClientName { get; set; } = string.Empty;
    
    // Get CContract Info
    public int? CContractId { get; set;}
    public string? ContractNo { get; set; }
    public string CContractDescription { get; set; } = string.Empty;
    
    // Get CBank Info
    public int? CBankId { get; set;}
    public string? CBankBankName { get; set; }
    public string CBankAccountName { get; set; } = string.Empty;
    public string CBankAccountNo { get; set; } = string.Empty;

    // Get CAddress Info
    public int? CAddressId { get; set;}
    public string CAddressAddressName { get; set; } = string.Empty;
    public string? CAddressCountry { get; set; }
    public string? CAddressProvince { get; set; }
    public string? CAddressCity { get; set; }
    public string? CAddressDistrict { get; set; }
    public string? CAddressVillage { get; set; }
    public string? CAddressAddress1 { get; set; }
    public string? CAddressAddress2 { get; set; }
    public string? CAddressPostalCode { get; set; }
    public string? CAddressPhoneNo { get; set; }
}