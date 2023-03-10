﻿
using Dynamitey;
using GDIApps.ValeCommonRules;
using Microsoft.Extensions.Hosting;
using NUnit.Framework.Internal;
using ServiceStack;
using ServiceStack.Auth;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using SpecFlowProjectGDIApps.Drivers;
using SpecFlowProjectGDIApps.Support;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist.ValueRetrievers;

namespace SpecFlowProjectGDIApps.Hooks
{
    [Binding]
    public sealed class Hooks1
    {
         enum TestType
        {
            UI,API,RULES
        }
        static TestType TestMode = TestType.API;
     static  ServiceStackHost appHost;
       const string BaseUri = "http://localhost:5001/";

        //public Hooks1(ScenarioContext context)
        //{
        //    Licensing.RegisterLicense("18930-e1JlZjoxODkzMCxOYW1lOlNhbmR5IEt3YXJ5LFR5cGU6SW5kaWUsTWV0YTowLEhhc2g6RTNYNmMyR0E5VzBHdTZQWDVyTmd4bmllSjNra2dRUGovdFVrTjR3aXZYVXpjTlRwN25ZMFVoOGUzS29IQzczTEZ3TnZqcmpyQVZEYnNWYzVTMFZ6OFlCcmhBakdJTGJWbndOd05uVnVXQXBRNjIzbmRHVThnc1dVRHdxaWlTNDNXdGpJeHFrbWdybTg4a09BWkFTYnh2Z0NncVFwK1dtZVhTTmdobHIwZWZVPSxFeHBpcnk6MjAyMy0wOS0wM30=");
        //    AppHost host = new AppHost();
        //    appHost = host.Init()
        //        .Start(BaseUri);

        //}
       
        // For additional details on SpecFlow hooks see http://go.specflow.org/doc-hooks
        [BeforeTestRun]
        public static void InitHost()
        {
            //add null suppport
            TechTalk.SpecFlow.Assist.Service.Instance.ValueRetrievers.Register(new NullValueRetriever("<null>"));

            if (TestMode == TestType.API)
            {
                Licensing.RegisterLicense("18930-e1JlZjoxODkzMCxOYW1lOlNhbmR5IEt3YXJ5LFR5cGU6SW5kaWUsTWV0YTowLEhhc2g6RTNYNmMyR0E5VzBHdTZQWDVyTmd4bmllSjNra2dRUGovdFVrTjR3aXZYVXpjTlRwN25ZMFVoOGUzS29IQzczTEZ3TnZqcmpyQVZEYnNWYzVTMFZ6OFlCcmhBakdJTGJWbndOd05uVnVXQXBRNjIzbmRHVThnc1dVRHdxaWlTNDNXdGpJeHFrbWdybTg4a09BWkFTYnh2Z0NncVFwK1dtZVhTTmdobHIwZWZVPSxFeHBpcnk6MjAyMy0wOS0wM30=");
                  var db = new OrmLiteConnectionFactory(":memory:", SqliteDialect.Provider);
                
             //   var db = new OrmLiteConnectionFactory("TestDb.sqlite", SqliteDialect.Provider);
                
                AppHost host = new AppHost();
                appHost = host.Init()
                    .Start(BaseUri);
            }
        }

        [BeforeScenario("@tag1")]
        public void BeforeScenarioWithTag()
        {
            // Example of filtering hooks using tags. (in this case, this 'before scenario' hook will execute if the feature/scenario contains the tag '@tag1')
            // See https://docs.specflow.org/projects/specflow/en/latest/Bindings/Hooks.html?highlight=hooks#tag-scoping

            //TODO: implement logic that has to run before executing each scenario
        }

        [BeforeScenario(Order = 0)]
        public void FirstBeforeScenario(ScenarioContext context)
        {
            if (TestMode == TestType.API)
            {
                IServiceClient client = new JsonServiceClient("http://localhost:5001/");
                var db = appHost.Container.Resolve<IDbConnectionFactory>();
                var authresp = appHost.Container.Resolve<IAuthRepository>();
                if (context.ScenarioInfo.Tags.Contains("mockCommonService"))
                {
                    Moq.Mock<ICommonService> _externalData = new Moq.Mock<ICommonService>();
                    appHost.Container.AddSingleton<ICommonService>(_externalData.Object);
                    context.Add("MockExternalData", _externalData);
                }
                else
                {
                    var commonService = new CommonService();
                    appHost.Container.AddSingleton<ICommonService>(commonService);
                  //  appHost.Container.AddTransient<IExternalData, CommonService>();
                }
                context.Add("DbConn", db);
                context.Add("AuthRepo", authresp);
                context.Add("Client", client);
                context.Add("Host", appHost);
                //  ServiceStackDriver drv = new ServiceStackDriver(context);
                context.ScenarioContainer.RegisterTypeAs<ServiceStackDriver, IDriver>();
            }
            // Example of ordering the execution of hooks
            // See https://docs.specflow.org/projects/specflow/en/latest/Bindings/Hooks.html?highlight=order#hook-execution-order

            //TODO: implement logic that has to run before executing each scenario
        }

        [AfterScenario]
        public void AfterScenario()
        {
            //TODO: implement logic that has to run after executing each scenario
        }
        [AfterTestRun]
        public static void TearDown()
        {
           // appHost.Dispose();
        }
    }
}