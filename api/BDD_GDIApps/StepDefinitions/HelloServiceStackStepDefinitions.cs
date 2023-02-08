using BDD_GDIApps.Support;
using ServiceStack;
using ServiceStack.Auth;
using System;
using TechTalk.SpecFlow;

namespace BDD_GDIApps.StepDefinitions
{
    [Binding]
    public class HelloServiceStackStepDefinitions
    {
        ScenarioContext _context;
       
        public HelloServiceStackStepDefinitions(ScenarioContext context)
        {
            _context = context;
        }
        [Given(@"Exist url service stack  ""([^""]*)""")]
        public void GivenExistUrlServiceStack(string p0)
        {
            var host = _context.Get<AppHost>("Host");
    
        }

        [Given(@"User login as")]
        public void GivenUserLoginAs(Table table)
        {
            var client = _context.Get<IServiceClient>("Client");
            client.Send(new Authenticate()
            {
                provider = CredentialsAuthProvider.Name,
                UserName = "admin@email.com",
                Password = "p@55wOrd"
            });
        }

        [When(@"User rquest hello world with name ""([^""]*)""")]
        public void WhenUserRquestHelloWorldWithName(string adam)
        {
            throw new PendingStepException();
        }

        [Then(@"the hello response should be ""([^""]*)""")]
        public void ThenTheHelloResponseShouldBe(string p0)
        {
            throw new PendingStepException();
        }
    }
}
