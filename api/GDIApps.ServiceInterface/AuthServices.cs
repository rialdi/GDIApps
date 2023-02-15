using ServiceStack;
using ServiceStack.Auth;
using GDIApps.ServiceModel.Types;

namespace GDIApps.ServiceInterface;

[ValidateIsAuthenticated]
public class AuthServices : Service
{
    public object Any (UpdatePassword request)
    {
        var existingUser = AuthRepository.GetUserAuthByUserName(request.Username);
        return AuthRepository.UpdateUserAuth(existingUser, existingUser, request.Password);
    }

    public object Any (UpdateAppUser request)
    {
        var existingUser = AuthRepository.GetUserAuthByUserName(request.Email) as AppUser;
        var updateUser = existingUser;
        updateUser.FullName = request.FullName != null ? request.FullName : existingUser.FullName;
        updateUser.PhoneNumber = request.PhoneNumber != null ? request.PhoneNumber : existingUser.PhoneNumber;
        updateUser.ProfileUrl = request.ProfileUrl != null ? request.ProfileUrl : existingUser.ProfileUrl;
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

    public object Any(UploadUserProfile request) 
    {
        var existingUser = AuthRepository.GetUserAuthByUserName(request.Email) as AppUser;
        var updateUser = existingUser;
        updateUser.ProfileUrl = request.ProfileUrl != null ? request.ProfileUrl : existingUser.ProfileUrl;
        return AuthRepository.UpdateUserAuth(existingUser, updateUser);
    }
}