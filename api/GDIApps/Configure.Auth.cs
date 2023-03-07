// using Microsoft.AspNetCore.Hosting;
// using ServiceStack;
// using System.Collections;
// using System.Linq;
using ServiceStack.Auth;
using ServiceStack.FluentValidation;
using ServiceStack.OrmLite;
using GDIApps.ServiceModel.Types;

[assembly: HostingStartup(typeof(GDIApps.ConfigureAuth))]

namespace GDIApps;

// Add any additional metadata properties you want to store in the Users Typed Session
public class CustomUserSession : AuthUserSession
{
}

// Custom Validator to add custom validators to built-in /register Service requiring DisplayName and ConfirmPassword
public class CustomRegistrationValidator : RegistrationValidator
{
    public CustomRegistrationValidator()
    {
        RuleSet(ApplyTo.Post, () =>
        {
            RuleFor(x => x.DisplayName).NotEmpty();
            RuleFor(x => x.ConfirmPassword).NotEmpty();
        });
    }
}

public class ConfigureAuth : IHostingStartup
{
    public void Configure(IWebHostBuilder builder) => builder
        //.ConfigureServices(services => services.AddSingleton<ICacheClient>(new MemoryCacheClient()))
        .ConfigureAppHost(appHost =>
        {
            var appSettings = appHost.AppSettings;
            appHost.Plugins.Add(new AuthFeature(() => new CustomUserSession(),
                new IAuthProvider[] {
                    new JwtAuthProvider(appSettings) {
                        AuthKeyBase64 = appSettings.GetString("AuthKeyBase64") ?? "cARl12kvS/Ra4moVBIaVsrWwTpXYuZ0mZf/gNLUhDW5=",
                    },
                    new CredentialsAuthProvider(appSettings),     /* Sign In with Username / Password credentials */
                    new FacebookAuthProvider(appSettings),        /* Create App https://developers.facebook.com/apps */
                    new GoogleAuthProvider(appSettings),          /* Create App https://console.developers.google.com/apis/credentials */
                    new MicrosoftGraphAuthProvider(appSettings),  /* Create App https://apps.dev.microsoft.com */
                })
            {
                IncludeDefaultLogin = false
            });

            using var db = appHost.GetDbConnection();
            if(db.TableExists<AppRole>()) {
                var appRole = db.Select<AppRole>().Select(s=> s.RoleName).ToArray();
                appHost.RegisterService(typeof(RegisterService).AddAttributes(
                    new RequiredRoleAttribute(
                        appRole
                    )
                ));
            }
            else {
                var appRoleFromSetting = appSettings.GetList("ApplicationRoles").ToArray();
                appHost.RegisterService(typeof(RegisterService).AddAttributes(
                    new RequiredRoleAttribute(
                        appSettings.GetList("ApplicationRoles").ToArray()
                    )
                ));
            }

            appHost.Plugins.Add(new RegistrationFeature()); //Enable /register Service

            //override the default registration validation with your own custom implementation
            appHost.RegisterAs<CustomRegistrationValidator, IValidator<Register>>();
        });
}