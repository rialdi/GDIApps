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


// [DataContract]
// public abstract class AdminUserBase : IMeta
// {
//     [DataMember(Order = 1)] public string UserName { get; set; }
//     [DataMember(Order = 2)] public string FirstName { get; set; }
//     [DataMember(Order = 3)] public string LastName { get; set; }
//     [DataMember(Order = 4)] public string DisplayName { get; set; }
//     [DataMember(Order = 5)] public string Email { get; set; }
//     [DataMember(Order = 6)] public string Password { get; set; }
//     [DataMember(Order = 7)] public string ProfileUrl { get; set; }
//     [DataMember(Order = 8)] public Dictionary<string, string> UserAuthProperties { get; set; }
//     [DataMember(Order = 9)] public Dictionary<string, string> Meta { get; set; }
//     [DataMember(Order = 10)] public string PhoneNumber { get; set; }
// }

// [DataContract]
// public partial class AdminCreateUser : AdminUserBase, IPost, IReturn<AdminUserResponse>
// {
//     [DataMember(Order = 10)] public List<string> Roles { get; set; }
//     [DataMember(Order = 11)] public List<string> Permissions { get; set; }
// }

// [DataContract]
// public partial class AdminUpdateUser : AdminUserBase, IPut, IReturn<AdminUserResponse>
// {
//     [DataMember(Order = 10)] public string Id { get; set; }
//     [DataMember(Order = 11)] public bool? LockUser { get; set; }
//     [DataMember(Order = 12)] public bool? UnlockUser { get; set; }
//     [DataMember(Order = 13)] public List<string> AddRoles { get; set; }
//     [DataMember(Order = 14)] public List<string> RemoveRoles { get; set; }
//     [DataMember(Order = 15)] public List<string> AddPermissions { get; set; }
//     [DataMember(Order = 16)] public List<string> RemovePermissions { get; set; }
// }

// [DataContract]
// public partial class AdminGetUser : IGet, IReturn<AdminUserResponse>
// {
//     [DataMember(Order = 10)] public string Id { get; set; }
// }

// [DataContract]
// public partial class AdminDeleteUser : IDelete, IReturn<AdminDeleteUserResponse>
// {
//     [DataMember(Order = 10)] public string Id { get; set; }
// }

// [DataContract]
// public class AdminDeleteUserResponse : IHasResponseStatus
// {
//     [DataMember(Order = 1)] public string Id { get; set; }
//     [DataMember(Order = 2)] public ResponseStatus ResponseStatus { get; set; }
// }

// [DataContract]
// public partial class AdminUserResponse : IHasResponseStatus
// {
//     [DataMember(Order = 1)] public string Id { get; set; }
//     [DataMember(Order = 2)] public Dictionary<string,object> Result { get; set; }
//     [DataMember(Order = 3)] public List<Dictionary<string,object>> Details { get; set; }
//     [DataMember(Order = 4)] public ResponseStatus ResponseStatus { get; set; }
// }

// [DataContract]
// public partial class AdminQueryUsers : IGet, IReturn<AdminUsersResponse>
// {
//     [DataMember(Order = 1)] public string Query { get; set; }
//     [DataMember(Order = 2)] public string OrderBy { get; set; }
//     [DataMember(Order = 3)] public int? Skip { get; set; }
//     [DataMember(Order = 4)] public int? Take { get; set; }
// }

// [DataContract]
// public class AdminUsersResponse : IHasResponseStatus
// {
//     [DataMember(Order = 1)] public List<Dictionary<string,object>> Results { get; set; }
//     [DataMember(Order = 2)] public ResponseStatus ResponseStatus { get; set; }
// }

