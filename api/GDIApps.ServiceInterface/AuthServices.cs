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
        var existingUser = AuthRepository.GetUserAuthByUserName(request.Email);
        return AuthRepository.UpdateUserAuth(existingUser, request);
    }

    // public object Any (GetUserInfo request)
    // {   
    //     return (AppUser)AuthRepository.GetUserAuthByUserName(request.UserNameOrEmail);
    // }

    public object Any (GetUserInfoDetail request)
    {   
        return AuthRepository.GetUserAuthByUserName(request.UserNameOrEmail);
    }
}