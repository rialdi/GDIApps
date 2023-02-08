using SpecFlowProjectGDIApps.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
namespace SpecFlowProjectGDIApps.Drivers
{
    public class ServiceStackDriver:IDriver
    {
        ScenarioContext _context;
        public ServiceStackDriver(ScenarioContext context) { 
            _context=context;
        }

        public void CheckHost(string p0)
        {
            var host=_context.Get<AppHost>("Host");
            host.Should().NotBeNull();
        }
    }
}
