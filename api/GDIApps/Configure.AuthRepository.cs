using ServiceStack;
using ServiceStack.Web;
using ServiceStack.Data;
using ServiceStack.Html;
using ServiceStack.Auth;
using ServiceStack.Configuration;
using GDIApps.ServiceModel.Types;

[assembly: HostingStartup(typeof(GDIApps.ConfigureAuthRepository))]

namespace GDIApps;

public class AppUserAuthEvents : AuthEvents
{
    public override async Task OnAuthenticatedAsync(IRequest httpReq, IAuthSession session, IServiceBase authService,
        IAuthTokens tokens, Dictionary<string, string> authInfo, CancellationToken token = default)
    {
        var authRepo = HostContext.AppHost.GetAuthRepositoryAsync(httpReq);
        using (authRepo as IDisposable)
        {
            var userAuth = (AppUser)await authRepo.GetUserAuthAsync(session.UserAuthId, token);
            userAuth.ProfileUrl = session.GetProfileUrl();
            userAuth.LastLoginIp = httpReq.UserHostAddress;
            userAuth.LastLoginDate = DateTime.UtcNow;

            var existingRoles = await authRepo.GetRolesAsync(userAuth);
            if(existingRoles.Count == 0)
            {
                await authRepo.AssignRolesAsync(userAuth,new[] { "Guest" });
            }
            await authRepo.SaveUserAuthAsync(userAuth, token);
        }
    }
}

public class ConfigureAuthRepository : IHostingStartup
{
    public void Configure(IWebHostBuilder builder) => builder
        .ConfigureServices(services => services.AddSingleton<IAuthRepository>(c =>
            new OrmLiteAuthRepository<AppUser, UserAuthDetails>(c.Resolve<IDbConnectionFactory>()) {
                UseDistinctRoleTables = true
            }))
        .ConfigureAppHost(appHost => {
            var authRepo = appHost.Resolve<IAuthRepository>();
            authRepo.InitSchema();
            CreateUser(authRepo, "rialdi@ptgdi.com", "Rialdi", "Formula01", roles: new[] { RoleNames.Admin, "Manager" });
            CreateUser(authRepo, "manager@email.com", "The Manager", "Formula01", roles: new[] { "Manager" });
            CreateUser(authRepo, "employee@email.com", "A Employee", "Formula01", roles: new[] { "Guest" });

            // Removing unused UserName in Admin Users UI 
            appHost.Plugins.Add(new ServiceStack.Admin.AdminUsersFeature {
                // Show custom fields in Search Results
                QueryUserAuthProperties = new() {
                    nameof(AppUser.Id),
                    nameof(AppUser.Email),
                    nameof(AppUser.FirstName),
                    nameof(AppUser.LastName),
                    nameof(AppUser.Gender),
                    nameof(AppUser.Country),
                    nameof(AppUser.PhoneNumber),
                    nameof(AppUser.DisplayName),
                    nameof(AppUser.Department),
                    nameof(AppUser.CreatedDate),
                    nameof(AppUser.LastLoginDate),
                },

                QueryMediaRules = new()
                {
                    MediaRules.ExtraSmall.Show<AppUser>(x => new { x.Id, x.Email, x.DisplayName }),
                    MediaRules.Small.Show<AppUser>(x => x.Department),
                },

                // Add Custom Fields to Create/Edit User Forms
                FormLayout = new() {
                    Input.For<AppUser>(x => x.Email),
                    Input.For<AppUser>(x => x.FirstName, c => c.FieldsPerRow(2)),
                    Input.For<AppUser>(x => x.LastName, c => c.FieldsPerRow(2)),
                    Input.For<AppUser>(x => x.UserName, c => c.FieldsPerRow(2)),
                    Input.For<AppUser>(x => x.Gender, c => { 
                        c.Type = Input.Types.Select;
                        c.AllowableValues = new string[]{"MALE", "FEMALE"};
                        // c.AllowableValues(GENDER as string);
                        c.FieldsPerRow(2);
                    }),
                    Input.For<AppUser>(x => x.DisplayName),
                    Input.For<AppUser>(x => x.Company),
                    Input.For<AppUser>(x => x.Department, c => c.FieldsPerRow(2)),
                    Input.For<AppUser>(x => x.PhoneNumber, c => {
                        c.Type = Input.Types.Tel;
                        c.FieldsPerRow(2);
                    }),
                    Input.For<AppUser>(x => x.Nickname, c => {
                        c.Help = "Public alias (3-12 lower alpha numeric chars)";
                        c.Pattern = "^[a-z][a-z0-9_.-]{3,12}$";
                        //c.Required = true;
                    }),
                    Input.For<AppUser>(x => x.ProfileUrl, c => c.Type = Input.Types.Url),
                    Input.For<AppUser>(x => x.IsArchived), 
                    Input.For<AppUser>(x => x.ArchivedDate),
                }
            });

        },
        afterConfigure: appHost => {
            appHost.AssertPlugin<AuthFeature>().AuthEvents.Add(new AppUserAuthEvents());
        });

    // Add initial Users to the configured Auth Repository
    public void CreateUser(IAuthRepository authRepo, string email, string name, string password, string[] roles)
    {
        if (authRepo.GetUserAuthByUserName(email) == null)
        {
            var newAdmin = new AppUser { Email = email, DisplayName = name };
            var user = authRepo.CreateUserAuth(newAdmin, password);
            authRepo.AssignRoles(user, roles);
        }
    }
}