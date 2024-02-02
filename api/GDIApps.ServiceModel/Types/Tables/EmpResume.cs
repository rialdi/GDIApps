using System;
using ServiceStack;
using ServiceStack.DataAnnotations;

namespace GDIApps.ServiceModel.Types;

[CompositeIndex(true,  nameof(AppUserId), nameof(ResumeType), nameof(ResumeNo))]
public class EmpResume : AuditBase
{
    #nullable disable
    [AutoIncrement]
    public int Id { get; set; }  

    [Required]
    [References(typeof(AppUser))]
    public int AppUserId { get; set;}
    [Required]
    [ApiAllowableValues(typeof(EMP_RESUME_TYPE))]
    public EMP_RESUME_TYPE ResumeType { get; set; }
    [Required]
    public int ResumeNo { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    [Required]
    public string Title { get; set; }
    public string SubTitle { get; set; }
    public string Description { get; set; }
    public DateTime? CertificationPublishedDate { get; set; }
    public DateTime? CertificationExpiredDate { get; set; }
    [Input(Type = "file"), UploadTo("resumeatt")]
    public string AttachmentFile { get; set; } = string.Empty;
    #nullable restore
}

[ValidateIsAuthenticated]
[Tag("EmpResumes")]
[AutoApply(Behavior.AuditQuery)]
public class QueryEmpResumes : QueryDb<EmpResume> 
{    
    #nullable disable
    public int? AppUserId { get; set; }
    #nullable restore
}

[ValidateIsAuthenticated]
[Tag("EmpResumes")]
[AutoApply(Behavior.AuditCreate)]
public class CreateEmpResume : ICreateDb<EmpResume>, IReturn<CRUDResponse>
{  
    #nullable disable
    public int AppUserId { get; set;}
    [ApiAllowableValues(typeof(EMP_RESUME_TYPE))]
    public EMP_RESUME_TYPE ResumeType { get; set; }
     public int ResumeNo { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string Title { get; set; }
    public string SubTitle { get; set; }
    public string Description { get; set; }
    public DateTime? CertificationPublishedDate { get; set; }
    public DateTime? CertificationExpiredDate { get; set; }
    [Input(Type = "file"), UploadTo("resumeatt")]
    public string AttachmentFile { get; set; }
    #nullable restore
}

[ValidateIsAuthenticated]
[Tag("EmpResumes")]
[AutoApply(Behavior.AuditModify)]
public class UpdateEmpResume : IPatchDb<EmpResume>, IReturn<CRUDResponse>
{
    #nullable disable
    public int Id { get; set; } 
    public int AppUserId { get; set;}
    [ApiAllowableValues(typeof(EMP_RESUME_TYPE))]
    public EMP_RESUME_TYPE ResumeType { get; set; }
     public int ResumeNo { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string Title { get; set; }
    public string SubTitle { get; set; }
    public string Description { get; set; }
    public DateTime? CertificationPublishedDate { get; set; }
    public DateTime? CertificationExpiredDate { get; set; }
    [Input(Type = "file"), UploadTo("resumeatt")]
    public string AttachmentFile { get; set; }
    #nullable restore
}

[ValidateIsAuthenticated]
[Tag("EmpResumes")]
public class DeleteEmpResume : IDeleteDb<EmpResume>, IReturnVoid
{
    public int Id { get; set; }        
}

