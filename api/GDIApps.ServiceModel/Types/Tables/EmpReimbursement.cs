using System;
using System.Collections.Generic;
using ServiceStack;
using ServiceStack.DataAnnotations;

namespace GDIApps.ServiceModel.Types;

[CompositeIndex(true,  nameof(AppUserId), nameof(Year), nameof(SubmitDate), nameof(ReimbursementType), nameof(ShortDesc))]
public class EmpReimbursement : AuditBase
{
    #nullable disable
    [AutoIncrement]
    public int Id { get; set; }  

    [Required]
    [References(typeof(AppUser))]
    public int AppUserId { get; set;}

    [Required]
    public int Year { get; set; }

    [Required]
    public DateTime SubmitDate { get; set; }

    [Required]
    public REIMBURSEMENT_TYPE ReimbursementType { get; set; }
    
    [Required]
    public string ShortDesc { get; set; }

    [StringLength(4000)]
    public string LongDesc { get; set; }

    public string FamilyMemberName { get; set; } 

    [Required]
    public decimal Amount { get; set; }

    [Input(Type = "file"), UploadTo("reimbursementfile")]
    public string AttahmentFile { get; set; }
    
    public REIMBURSEMENT_STATUS Status { get; set; }

    public string StatusNotes { get; set; }
    #nullable restore
}

[ValidateIsAuthenticated]
[Tag("EmpReimbursements")]
[AutoApply(Behavior.AuditQuery)]
public class QueryEmpReimbursements : QueryDb<EmpReimbursement> 
{    
    #nullable disable
    public int AppUserId { get; set; }
    public int Year { get; set; }
    public string ReimbursementTypes { get; set; }
}

[ValidateIsAuthenticated]
[Tag("EmpReimbursements")]
[AutoApply(Behavior.AuditCreate)]
public class CreateEmpReimbursement : ICreateDb<EmpReimbursement>, IReturn<CRUDResponse>
{  
    #nullable disable
    public int AppUserId { get; set;}
    public int Year { get; set; }
    public DateTime SubmitDate { get; set; }
    [ApiAllowableValues(typeof(REIMBURSEMENT_TYPE))]
    public REIMBURSEMENT_TYPE ReimbursementType { get; set; }
    public string ShortDesc { get; set; }
    public string LongDesc { get; set; }
    public string FamilyMemberName { get; set; } 
    public decimal Amount { get; set; }

    [Input(Type = "file"), UploadTo("reimbursementfile")]
    public string AttahmentFile { get; set; }
    [ApiAllowableValues(typeof(REIMBURSEMENT_STATUS))]
    public REIMBURSEMENT_STATUS Status { get; set; }
    public string StatusNotes { get; set; }
    #nullable restore
}

[ValidateIsAuthenticated]
[Tag("EmpReimbursements")]
[AutoApply(Behavior.AuditModify)]
public class UpdateEmpReimbursement : IPatchDb<EmpReimbursement>, IReturn<CRUDResponse>
{
    public int Id { get; set; } 
    #nullable disable
    public int AppUserId { get; set;}
    public int Year { get; set; }
    public DateTime SubmitDate { get; set; }
    [ApiAllowableValues(typeof(REIMBURSEMENT_TYPE))]
    public REIMBURSEMENT_TYPE ReimbursementType { get; set; }
    public string ShortDesc { get; set; }
    public string LongDesc { get; set; }
    public string FamilyMemberName { get; set; } 
    public decimal Amount { get; set; }

    [Input(Type = "file"), UploadTo("reimbursementfile")]
    public string AttahmentFile { get; set; }
    [ApiAllowableValues(typeof(REIMBURSEMENT_STATUS))]
    public REIMBURSEMENT_STATUS Status { get; set; }
    public string StatusNotes { get; set; }
    #nullable restore
}

[ValidateIsAuthenticated]
[Tag("EmpReimbursements")]
public class DeleteEmpReimbursement : IDeleteDb<EmpReimbursement>, IReturnVoid
{
    public int Id { get; set; }        
}

