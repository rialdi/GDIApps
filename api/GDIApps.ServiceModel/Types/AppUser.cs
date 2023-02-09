using ServiceStack;
using System.Collections;
using System.Collections.Generic;
using ServiceStack.Auth;
using ServiceStack.Configuration;
using System;

namespace GDIApps.ServiceModel.Types;

[ValidateIsAuthenticated]
public class AppUser : UserAuth
{
    [Format(FormatMethods.IconRounded)]
    [Input(Type = "file"), UploadTo("userprofile")]
    public string ProfileUrl { get; set; } = string.Empty;
    public string LastLoginIp { get; set; } = string.Empty;
    public DateTime? LastLoginDate { get; set; }
    public string EmployeeId {get; set;} = string.Empty;
    public Department Department {get; set;} = Department.None;
    public bool IsArchived { get; set; } = false;
    public DateTime? ArchivedDate { get; set; }
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
public class UpdateAppUser: AppUser
{
}

[Tag("appusers")]
public class AppUserConfirmEmail
{
    public string UserName { get; set; } = string.Empty;
    [ApiAllowableValues(typeof(EMAIL_TEMPLATE_CODE))]
    public EMAIL_TEMPLATE_CODE EmailTemplateCode { get; set; }   
}