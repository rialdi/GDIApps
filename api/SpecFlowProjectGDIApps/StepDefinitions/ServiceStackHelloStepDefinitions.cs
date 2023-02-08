using ServiceStack.Auth;
using ServiceStack;
using SpecFlowProjectGDIApps.Support;
using System;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using GDIApps.ServiceModel;
using SpecFlowProjectGDIApps.Drivers;
using ServiceStack.Data;

namespace SpecFlowProjectGDIApps.StepDefinitions
{
    [Binding]
    public class ServiceStackHelloStepDefinitions
    {
        ScenarioContext _context;
        IDriver _driver;
        DatabaseDriver dbDriver;
        public ServiceStackHelloStepDefinitions(ScenarioContext context,IDriver driver)
        {
            _context = context;
            _driver = driver;
            dbDriver = new DatabaseDriver(_context.Get<IDbConnectionFactory>("DbConn"));
        }
        [Given(@"Exist url service stack  ""([^""]*)""")]
        public void GivenExistUrlServiceStack(string p0)
        {
            var host = _context.Get<AppHost>("Host");
     
            host.Should().NotBeNull();
            _driver.CheckHost(p0);
        }

        [Given(@"User login as")]
        public void GivenUserLoginAs(Table table)
        {
            var client = _context.Get<IServiceClient>("Client");
            dynamic dnmc = table.CreateDynamicInstance();
            client.Send(new Authenticate()
            {
                provider = CredentialsAuthProvider.Name,
                UserName = dnmc.UserName,
                Password = dnmc.Password
            });
        }

        [When(@"User rquest hello world with name ""([^""]*)""")]
        public void WhenUserRquestHelloWorldWithName(string name)
        {
            var client = _context.Get<IServiceClient>("Client");
          var res=  client.Get(new Hello { Name = name });
         
            _context.Add("ResponseHello", res.Result);
        }

        [Then(@"the hello response should be ""([^""]*)""")]
        public void ThenTheHelloResponseShouldBe(string p0)
        {
            var res = _context.Get<string>("ResponseHello");
            p0.Should().Be(res);
        }
    }
}
