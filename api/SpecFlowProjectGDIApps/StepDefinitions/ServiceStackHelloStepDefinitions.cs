using ServiceStack.Auth;
using ServiceStack;
using SpecFlowProjectGDIApps.Support;
using System;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using GDIApps.ServiceModel;

namespace SpecFlowProjectGDIApps.StepDefinitions
{
    [Binding]
    public class ServiceStackHelloStepDefinitions
    {
        ScenarioContext _context;
        public ServiceStackHelloStepDefinitions(ScenarioContext context)
        {
            _context = context;
        }
        [Given(@"Exist url service stack  ""([^""]*)""")]
        public void GivenExistUrlServiceStack(string p0)
        {
            var host = _context.Get<AppHost>("Host");
            host.Should().NotBeNull();
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
