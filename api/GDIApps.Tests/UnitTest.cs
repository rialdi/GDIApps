using NUnit.Framework;
using ServiceStack;
using ServiceStack.Testing;
using GDIApps.ServiceInterface;
using GDIApps.ServiceModel;

namespace GDIApps.Tests
{
    public class UnitTest
    {
        private readonly ServiceStackHost appHost;

        public UnitTest()
        {
            Licensing.RegisterLicense("18930-e1JlZjoxODkzMCxOYW1lOlNhbmR5IEt3YXJ5LFR5cGU6SW5kaWUsTWV0YTowLEhhc2g6RTNYNmMyR0E5VzBHdTZQWDVyTmd4bmllSjNra2dRUGovdFVrTjR3aXZYVXpjTlRwN25ZMFVoOGUzS29IQzczTEZ3TnZqcmpyQVZEYnNWYzVTMFZ6OFlCcmhBakdJTGJWbndOd05uVnVXQXBRNjIzbmRHVThnc1dVRHdxaWlTNDNXdGpJeHFrbWdybTg4a09BWkFTYnh2Z0NncVFwK1dtZVhTTmdobHIwZWZVPSxFeHBpcnk6MjAyMy0wOS0wM30=");
            appHost = new BasicAppHost().Init();
            appHost.Container.AddTransient<MyServices>();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown() => appHost.Dispose();

        [Test]
        public void Can_call_MyServices()
        {
            var service = appHost.Container.Resolve<MyServices>();

            var response = (HelloResponse)service.Any(new Hello { Name = "World" });

            Assert.That(response.Result, Is.EqualTo("Hello, World!"));
        }
    }
}
