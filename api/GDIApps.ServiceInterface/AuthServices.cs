using ServiceStack;
using ServiceStack.Admin;
using ServiceStack.Auth;
using System.Linq;
using ServiceStack.OrmLite;
using GDIApps.ServiceModel.Types;

namespace GDIApps.ServiceInterface;

[ValidateIsAuthenticated]
public class AuthServices : Service
{
    public IAutoQueryDb AutoQuery { get; set; }
    public object Any (UpdatePassword request)
    {
        IUserAuth userAuth;
        var isValidCurrPassword = AuthRepository.TryAuthenticate(request.Username, request.CurrPassword, out userAuth);

        if(!isValidCurrPassword)
        {
            throw new System.Exception("Invalid Current Password");
        }
        // var existingUser = AuthRepository.GetUserAuthByUserName(request.Username);
        return AuthRepository.UpdateUserAuth(userAuth, userAuth, request.NewPassword);
    }

    public object Any (UpdateAppUser request)
    {
        var existingUser = AuthRepository.GetUserAuthByUserName(request.Email) as AppUser;
        var updateUser = existingUser;
        updateUser.FullName = request.FullName != null ? request.FullName : existingUser.FullName;
        updateUser.FirstName = request.FirstName != null ? request.FirstName : existingUser.FirstName;
        updateUser.PhoneNumber = request.PhoneNumber != null ? request.PhoneNumber : existingUser.PhoneNumber;
        // updateUser.ProfileUrl = request.ProfileUrl != null ? request.ProfileUrl : existingUser.ProfileUrl;
        return AuthRepository.UpdateUserAuth(existingUser, updateUser);
    }

    // public object Any (GetUserInfo request)
    // {   
    //     return (AppUser)AuthRepository.GetUserAuthByUserName(request.UserNameOrEmail);
    // }

    public object Any (GetUserInfoDetail request)
    {   
        return AuthRepository.GetUserAuthByUserName(request.UserNameOrEmail);
    }

    public object Any (GetUserInfoDetailById request)
    {
        var userAuth = AuthRepository.GetUserAuth(request.UserAuthId);
        // return AuthRepository.GetUserAuthByUserName(userAuth.UserName);

        return userAuth;
    }

    public object Any(UploadUserProfile request) 
    {
        var existingUser = AuthRepository.GetUserAuthByUserName(request.Email) as AppUser;
        var updateUser = existingUser;
        updateUser.ProfileUrl = request.ProfileUrl != null ? request.ProfileUrl : existingUser.ProfileUrl;
        return AuthRepository.UpdateUserAuth(existingUser, updateUser);
    }

    public object Any (GetUserList request)
    { 
        var userAuthList = AuthRepository.GetUserAuths();
        foreach(var userAuth in userAuthList)
        {
            userAuth.Roles = AuthRepository.GetRoles(userAuth).ToList();
            userAuth.Permissions = AuthRepository.GetPermissions(userAuth).ToList();
        }
        return userAuthList;
    }

    public object Any (GetUserListByRoles request)
    { 
        var db = AutoQuery.GetDb<AppUser>();
        var returnList = db.Select(db.From<AppUser>()
            .Join<UserAuthRole>((appUser,authRole) => appUser.Id == authRole.UserAuthId)
            .Where<UserAuthRole>(x => x.Role == request.RoleName || request.RoleName == string.Empty));

        return new QueryResponse<AppUser>{
            Total = returnList.Count,
            Results = returnList
        };
    }
}