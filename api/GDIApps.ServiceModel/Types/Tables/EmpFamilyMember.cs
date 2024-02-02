using System;
using ServiceStack;
using ServiceStack.DataAnnotations;

namespace GDIApps.ServiceModel.Types;

[CompositeIndex(true,  nameof(AppUserId), nameof(MemberType), nameof(MemberNo))]
public class EmpFamilyMember : AuditBase
{
    #nullable disable
    [AutoIncrement]
    public int Id { get; set; }  

    [Required]
    [References(typeof(AppUser))]
    public int AppUserId { get; set;}
    [Required]
    public FAMILY_MEMBER_TYPE MemberType { get; set; }
    [Required]
    public int MemberNo { get; set; }
    [Required]
    public LIVING_STATUS LivingStatus { get; set; }
    [Required]
    public GENDER Gender { get; set; }
    [Required]
    public string FullName { get; set; } 
    [Required]
    public string NickName { get; set; } 
    [Required]
    public DateTime BirthDate { get; set; }
    public string PlaceOfBirth { get; set; } 
    public string PhoneNo { get; set; } 
    [Input(Type = "file"), UploadTo("userprofile")]
    public string ProfileUrl { get; set; } = string.Empty;
    #nullable restore
}


[ValidateIsAuthenticated]
[Tag("EmpFamilyMembers")]
[AutoApply(Behavior.AuditQuery)]
public class QueryEmpFamilyMembers : QueryDb<EmpFamilyMember> 
{    
    #nullable disable
    public int? AppUserId { get; set; }
    #nullable restore
}

[ValidateIsAuthenticated]
[Tag("EmpFamilyMembers")]
[AutoApply(Behavior.AuditCreate)]
public class CreateEmpFamilyMember : ICreateDb<EmpFamilyMember>, IReturn<CRUDResponse>
{  
    #nullable disable
    public int AppUserId { get; set;}
    [ApiAllowableValues(typeof(FAMILY_MEMBER_TYPE))]
    public FAMILY_MEMBER_TYPE MemberType { get; set; }
    public int MemberNo { get; set; }
    [ApiAllowableValues(typeof(LIVING_STATUS))]
    public LIVING_STATUS LivingStatus { get; set; }
    [ApiAllowableValues(typeof(GENDER))]
    public GENDER Gender { get; set; }
    public string FullName { get; set; } 
    public string NickName { get; set; } 
    public DateTime BirthDate { get; set; }
    public string PlaceOfBirth { get; set; } 
    public string PhoneNo { get; set; } 
    [Input(Type = "file"), UploadTo("userprofile")]
    public string ProfileUrl { get; set; }
    #nullable restore
}

[ValidateIsAuthenticated]
[Tag("EmpFamilyMembers")]
[AutoApply(Behavior.AuditModify)]
public class UpdateEmpFamilyMember : IPatchDb<EmpFamilyMember>, IReturn<CRUDResponse>
{
    #nullable disable
    public int Id { get; set; } 
    public int AppUserId { get; set;}
    [ApiAllowableValues(typeof(FAMILY_MEMBER_TYPE))]
    public FAMILY_MEMBER_TYPE MemberType { get; set; }
    public int MemberNo { get; set; }
    [ApiAllowableValues(typeof(LIVING_STATUS))]
    public LIVING_STATUS LivingStatus { get; set; }
    [ApiAllowableValues(typeof(GENDER))]
    public GENDER Gender { get; set; }
    public string FullName { get; set; } 
    public string NickName { get; set; } 
    public DateTime BirthDate { get; set; }
    public string PlaceOfBirth { get; set; } 
    
    public string PhoneNo { get; set; } 
    [Input(Type = "file"), UploadTo("userprofile")]
    public string ProfileUrl { get; set; }
    #nullable restore
}

[ValidateIsAuthenticated]
[Tag("EmpFamilyMembers")]
public class DeleteEmpFamilyMember : IDeleteDb<EmpFamilyMember>, IReturnVoid
{
    public int Id { get; set; }        
}

