using Dynamitey;
using ServiceStack.Auth;
using ServiceStack;
using SpecFlowProjectGDIApps.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using GDIApps.ServiceModel;
using GDIApps.ServiceModel.Types;
using ServiceStack.Configuration;
using static ServiceStack.Diagnostics.Events;
using BusinessRules.CommonServiceData;
using System.Globalization;

namespace SpecFlowProjectGDIApps.Drivers
{
    public class ServiceStackDriver:IDriver
    {
         string UserName = "admin@email.com";
         string Password = "p@55wOrd";
        ScenarioContext _context;
        public ServiceStackDriver(ScenarioContext context) { 
            _context=context;
        }

        public void CheckAndLogIfUserAdmin(Table table)
        {
            dynamic userData = table.CreateDynamicInstance();
            var host = _context.Get<AppHost>("Host");
          var auth=  host.Resolve<IAuthRepository>();
            var user = (AppUser)auth.GetUserAuthByUserName(userData.UserName);
            user.Id.Should().BeGreaterThan(0);
            (auth as IManageRoles).HasRole(user.Id,RoleNames.Admin).Should().BeTrue();
          
            _context.Add("UserName",userData.UserName);
            _context.Add("Password", userData.Password);
        }

        public void CheckHost(string p0)
        {
            var host=_context.Get<AppHost>("Host");
            host.Should().NotBeNull();
        }

        public void CreateDraftClaim(string p0, Table table)
        {
            var date = DateTime.ParseExact(p0, "dd-MMM-yyyy", CultureInfo.GetCultureInfo("en-us"));
            DateTime.TryParseExact(p0, "dd-MMM-yyyy", CultureInfo.GetCultureInfo("en-us"),DateTimeStyles.None,out var resultParse).Should().BeTrue();
            IEnumerable<dynamic> selectedEmployees = table.CreateDynamicSet();
            
        }

        public void DeleteLookupById(int id)
        {
            var client = _context.Get<IServiceClient>("Client");

            client.Send(new Authenticate()
            {
                provider = CredentialsAuthProvider.Name,
                UserName = _context.Get<string>("UserName"),
                Password = _context.Get<string>("Password")
            });
            GDIApps.ServiceModel.DeleteLookup delRequest=new DeleteLookup() { Id= id };
            client.Api(delRequest);
        }

        public List<Employee> GetEmployeeOptionsByLoggedUser()
        {
            throw new NotImplementedException();
        }

        public async void InputNewLookupSet(Table table)
        {
            IEnumerable<dynamic> sets = table.CreateDynamicSet();
            var client = _context.Get<IServiceClient>("Client");
         
            client.Send(new Authenticate()
            {
                provider = CredentialsAuthProvider.Name,
                UserName =_context.Get<string>("UserName"),
                Password = _context.Get<string>("Password")
            });
            foreach(var data in sets)
            {
                var request = new CreateLookup() { LookupType = LOOKUPTYPE.PRIORITY, LookupValue = data.LookupValue, LookupText = data.LookupText, IsActive = true };
              var resp=  client.Api(request);
            resp.Should().NotBeNull();
                resp.Succeeded.Should().BeTrue();
                resp.Response.Should().NotBeNull();
                resp.Response.ResponseStatus.IsSuccess().Should().BeTrue();
                resp.Response.Id.Should().BeGreaterThan(0);

            }
          

        }

        public void UpdateLookup(Lookup lookup)
        {
            var client = _context.Get<IServiceClient>("Client");

            client.Send(new Authenticate()
            {
                provider = CredentialsAuthProvider.Name,
                UserName = _context.Get<string>("UserName"),
                Password = _context.Get<string>("Password")
            });
            GDIApps.ServiceModel.UpdateLookup request=new UpdateLookup() { Id= lookup.Id,LookupText=lookup.LookupText
            ,LookupType=lookup.LookupType,
            LookupValue=lookup.LookupValue
            };
          var resp=  client.Api(request);
            resp.Succeeded.Should().BeTrue();
            resp.Response.Should().NotBeNull();
            resp.Response.ResponseStatus.IsSuccess().Should().BeTrue();
            resp.Response.Id.Should().BeGreaterThan(0);
        }
    }
}
