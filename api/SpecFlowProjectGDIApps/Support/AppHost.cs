using GDIApps.ServiceInterface;
using GDIApps.ServiceModel.Types;
using ServiceStack.Auth;
using ServiceStack.Configuration;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Funq;
using ServiceStack.Web;
using SpecFlowProjectGDIApps.Hooks;
using FluentAssertions.Common;
using BusinessRules;

namespace SpecFlowProjectGDIApps.Support
{
    public class AppHost : AppSelfHostBase
    {
        IDbConnectionFactory _db;
        public AppHost(IDbConnectionFactory db) : base(nameof(Hooks1), typeof(TodosServices).Assembly) {
            _db = db;
        }

        public override void Configure(Container container)
        {
            SetConfig(new HostConfig { DebugMode = true });
         


            container.AddSingleton<IDbConnectionFactory>(_db);
           
            container.AddSingleton<IAuthRepository>(c =>
                new OrmLiteAuthRepository<AppUser, UserAuthDetails>(c.Resolve<IDbConnectionFactory>())
                {
                    UseDistinctRoleTables = true
                });
           
            Plugins.Add(new AuthFeature(() => new CustomUserSession(),
            GetAuthProviders()));
            var authRepo = container.Resolve<IAuthRepository>();
            authRepo.InitSchema();
            CreateUser(authRepo, "admin@email.com", "Admin User", "p@55wOrd", roles: new[] { RoleNames.Admin });
            CreateUser(authRepo, "manager@email.com", "The Manager", "p@55wOrd", roles: new[] { "Employee", "Manager" });
            CreateUser(authRepo, "employee@email.com", "A Employee", "p@55wOrd", roles: new[] { "Employee" });
           

            container.AddSingleton<ICrudEvents>(c =>
               new OrmLiteCrudEvents(c.Resolve<IDbConnectionFactory>()));

            Plugins.Add(new AutoQueryDataFeature());

            // For Bookings https://github.com/NetCoreApps/BookingsCrud
            Plugins.Add(new AutoQueryFeature
            {
                MaxLimit = 1000,
                //IncludeTotal = true,
            });

            Resolve<ICrudEvents>().InitSchema();
            this.AssertPlugin<AuthFeature>().AuthEvents.Add(new AppUserAuthEvents());
        }
        public void CreateUser(IAuthRepository authRepo, string email, string name, string password, string[] roles)
        {
            if (authRepo.GetUserAuthByUserName(email) == null)
            {
                var newAdmin = new AppUser { Email = email, DisplayName = name };
                var user = authRepo.CreateUserAuth(newAdmin, password);
                

                authRepo.AssignRoles(user, roles);
            }
        }
        public IDbConnectionFactory Db
        {
            get
            {
                return Resolve<IDbConnectionFactory>();
            }
        }
        public IAuthRepository Authrepository
        {
            get
            {
                return Resolve<IAuthRepository>();
            }
        }



        public virtual IAuthProvider[] GetAuthProviders()
        {
            return new IAuthProvider[] { //Www-Authenticate should contain basic auth, therefore register this provider first
                    new BasicAuthProvider(AppSettings), //Sign-in with Basic Auth
                    new CredentialsAuthProvider(AppSettings) //HTML Form post of UserName/Password credentials
                  
                };
        }
    }
    public class AppUserAuthEvents : AuthEvents
    {
        public override void OnAuthenticated(IRequest req, IAuthSession session, IServiceBase authService,
            IAuthTokens tokens, Dictionary<string, string> authInfo)
        {
            var authRepo = HostContext.AppHost.GetAuthRepository(req);
            using (authRepo as IDisposable)
            {
                var userAuth = (AppUser)authRepo.GetUserAuth(session.UserAuthId);
                userAuth.ProfileUrl = session.GetProfileUrl();
                userAuth.LastLoginIp = req.UserHostAddress;
                userAuth.LastLoginDate = DateTime.UtcNow;
                authRepo.SaveUserAuth(userAuth);
            }
        }
    }
}
