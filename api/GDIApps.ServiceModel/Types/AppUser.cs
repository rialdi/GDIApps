using ServiceStack;
using System.Collections;
using System.Collections.Generic;
using ServiceStack.Auth;
using ServiceStack.Configuration;
using System;
using ServiceStack.DataAnnotations;
using System.Runtime.Serialization;

namespace GDIApps.ServiceModel.Types;

[ValidateIsAuthenticated]
public class AppUser : UserAuth
{
    [Format(FormatMethods.IconRounded)]
    [Input(Type = "file"), UploadTo("userprofile")]
    public string? ProfileUrl { get; set; }
    public string? LastLoginIp { get; set; }
    public DateTime? LastLoginDate { get; set; }
    public string? EmployeeId {get; set;}
    public Department Department {get; set;} = Department.None;
    public bool IsArchived { get; set; } = false;
    public DateTime? ArchivedDate { get; set; }

    // [ApiAllowableValues(typeof(GENDER))]
    // public override string Gender {get; set; } = GENDER.MALE.ToString();
    
    // public override string FirstName {get; set;} = string.Empty;
    // public string TwitterUserId { get; set; }
    // public string TwitterScreenName { get; set; }

    // public string FacebookUserId { get; set; }

    // public string GoogleUserId { get; set; }

    // public string GoogleProfilePageUrl { get; set; }

    // public string MicrosoftUserId { get; set; }
}

[Tag("appusers")]
[ValidateIsAuthenticated]
public class UpdatePassword: IReturn<ResponseStatus>
{
    public string Username {get; set;} = string.Empty;
    public string Password {get; set;} = string.Empty;
}

[Tag("appusers")]
[ValidateIsAuthenticated]
public class GetUserList : IReturn<AppUser[]>
{
    // public string UserNameOrEmail { get; set; } = string.Empty;
}

[Tag("appusers")]
[ValidateIsAuthenticated]
public class GetUserInfo : IReturn<AppUser>
{
    public string UserNameOrEmail { get; set; } = string.Empty;
}

[Tag("appusers")]
[ValidateIsAuthenticated]
public class GetUserInfoDetail : IReturn<AppUser>
{
    public string UserNameOrEmail { get; set; } = string.Empty;
}

[Tag("appusers")]
[ValidateIsAuthenticated]
public class UpdateAppUser
{
    public string Email {get;set;}  = string.Empty;
    public string? Gender {get;set;} 
    public string? FirstName {get;set;} 
    public string? LastName {get;set;} 
    public string? FullName {get;set;} 
    public string? NickName {get;set;} 
    public string? DisplayName {get;set;} 
    public string? PhoneNumber {get;set;} 
    public DateTime? BirthDate { get; set; }
    public string? Address {get;set;} 
    public string? Address2 {get;set;} 
    public string? City {get;set;} 
    public string? State {get;set;} 
    public string? Country {get;set;} 
    public string? PostCode {get;set;} 
    // [Format(FormatMethods.IconRounded)]
    // [Input(Type = "file"), UploadTo("userprofile")]
    // public string? ProfileUrl {get;set;} 
}

[Tag("appusers")]
public class AppUserConfirmEmail
{
    public string UserName { get; set; } = string.Empty;
    [ApiAllowableValues(typeof(EMAIL_TEMPLATE_CODE))]
    public EMAIL_TEMPLATE_CODE EmailTemplateCode { get; set; }   
}

[ValidateIsAuthenticated]
[Tag("appusers")]
[Route("/uploaduserprofile/{Id}")]
public class UploadUserProfile : IPost, IReturn<UploadUserProfile>
{
    public string Email {get;set;} = string.Empty;

    [Input(Type = "file"), UploadTo("userprofile")]
    public string? ProfileUrl {get;set;} 
}


[Tag("appusers")]
[ValidateIsAuthenticated]
public class GetUserListByRoles : IReturn<AppUser[]>
{
    public string RoleName {get;set;} = string.Empty;
    // public string UserNameOrEmail { get; set; } = string.Empty;
}