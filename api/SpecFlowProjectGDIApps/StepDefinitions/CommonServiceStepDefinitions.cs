using System;
using TechTalk.SpecFlow;
using BusinessRules.CommonServiceData;
using BusinessRules;
using Moq;
using TechTalk.SpecFlow.Assist;
using ServiceStack;
using Microsoft.Extensions.Hosting;
using SpecFlowProjectGDIApps.Support;

namespace SpecFlowProjectGDIApps.StepDefinitions
{
    [Binding]
    public class CommonServiceStepDefinitions
    {
        ScenarioContext _context;
        public CommonServiceStepDefinitions(ScenarioContext context) {
            _context = context;
        }
        [When(@"calling common service with code ""([^""]*)"" and parameter ""([^""]*)""")]
        public void WhenCallingCommonServiceWithCodeAndParameter(string p0, string p1)
        {
            var commonService = new CommonService();
            var employee = commonService.ExecutequeryByParam<List<Employee>>(p0, p1);
            _context.Add("EmployeeDataCount", employee.Count);
        }

        [Then(@"the result is contains (.*) data")]
        public void ThenTheResultIsContainsData(int p0)
        {
           _context.Get<int>("EmployeeDataCount").Should().Be(p0);
        }

        [When(@"calling common service employee with BN ""([^""]*)""")]
        public void WhenCallingCommonServiceEmployeeWithBN(string p0)
        {
            var commonService = new CommonService();
            var employee = commonService.GetEmployeeById(p0);
            employee.Should().NotBeNull();
            _context.Add("EmployeeDataCount", 1);
        }

        [Given(@"Exist Following Employees Data From Common Service")]
        public void GivenExistFollowingEmployeesDataFromCommonService(Table table)
        {
            IEnumerable<dynamic> empInputs = table.CreateDynamicSet(doTypeConversion: false);
            if (_context.ScenarioInfo.Tags.Contains("mockCommonService"))
            {
                var mock = _context.Get<Mock<IExternalData>>("MockExternalData");
                foreach (var empInput in empInputs)
                {
                    string empId = empInput.EMPLOYEE_ID;
                    string name = empInput.NAME;
                    mock.Setup(x => x.GetEmployeeById(empId)).Returns(new Employee() { 
                        EMPLOYEE_ID = empId,
                        NAME = name,
                        POSITION_ID= empInput.POSITION_ID,
                        POS_DEPT= empInput.POS_DEPT,
                        SUPERIOR_NAME= empInput.SUPERIOR_NAME,
                        SUPERIOR_POS= empInput.SUPERIOR_POS,
                        MOR_NAME= empInput.MOR_NAME,
                        MOR_POS= empInput.MOR_POS
                    });
                }
            }

        }
        //[Given(@"Exist Following Employees Data From Common Service")]
        //public void GivenExistFollowingEmployeeDataFromCommonService(Table table)
        //{
        //    dynamic empInput = table.CreateDynamicInstance();
        //    if (_context.ScenarioInfo.Tags.Contains("mockCommonService"))
        //    {
        //        var mock = _context.Get<Mock<IExternalData>>("MockExternalData");

        //        string empId = empInput.EMPLOYEE_ID;
        //        string name = empInput.NAME;
        //        mock.Setup(x => x.GetEmployeeById(empId)).Returns(new Employee() { EMPLOYEE_ID = empId, NAME = name });

        //    }
        //    else
        //    {

        //    }
        //}

        [When(@"calling common service via injection to get employee with BN ""([^""]*)""")]
        public void WhenCallingCommonServiceViaInjectionToGetEmployeeWithBN(string p0)
        {
            var appHost =_context.Get<AppHost>("Host");
            var commonservice = appHost.Container.Resolve<IExternalData>();
          var emp=  commonservice.GetEmployeeById(p0);
            emp.Should().NotBeNull();
            _context.Add("EmployeeDataCount", 1);
        }



    }
}
