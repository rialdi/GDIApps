using System;
using TechTalk.SpecFlow;


using Moq;
using TechTalk.SpecFlow.Assist;
using ServiceStack;
using Microsoft.Extensions.Hosting;
using SpecFlowProjectGDIApps.Support;
using System.Xml.Linq;
using GDIApps.ValeCommonRules;
using GDIApps.ValeCommonRules.CommonServiceData;

namespace SpecFlowProjectGDIApps.StepDefinitions
{
    [Binding]
    public class CommonServiceStepDefinitions
    {
        ScenarioContext _context;
        public CommonServiceStepDefinitions(ScenarioContext context) {
            _context = context;
        }
        [Given(@"BaseAPIUrl is ""([^""]*)""")]
        public void GivenBaseAPIUrlIs(string p0)
        {
            _context.Add("BaseUriCommonService", p0);
        }

        [Given(@"Exist Employee AD user information From Common Service")]
        public void GivenExistEmployeeADUserInformationFromCommonService(Table table)
        {
            IEnumerable<dynamic> existUser = table.CreateDynamicSet(doTypeConversion: false);
            if (_context.ScenarioInfo.Tags.Contains("mockCommonService"))
            {
                var mock = _context.Get<Mock<ICommonService>>("MockExternalData");
                List<ActiveDirectoryUser> users = new List<ActiveDirectoryUser>();
                foreach (var userData in existUser)
                {
                    var user = new ActiveDirectoryUser()
                    {
                        EMAIL = userData.EMAIL,
                        EMPLOYEEID = userData.EMPLOYEE_ID,
                        FULL_NAME = userData.FULL_NAME,
                        EMPLOYEE_ID = userData.EMPLOYEE_ID,
                        USERNAME = userData.USERNAME
                    };
                    users.Add(user);
                }
                mock.Setup(x => x.ExecutequeryByParam<List<ActiveDirectoryUser>>(It.IsAny<string>(), It.IsAny<string>()))
                    .Returns(users);
            }
            else
            {

                var commonService = new CommonService(_context.Get<string>());
                foreach (var userData in existUser)
                {
                    ActiveDirectoryUser userAd = commonService.GetADUserById(userData.EMPLOYEE_ID);
                    userAd.Should().NotBeNull();
                    userAd.USERNAME.Should().Be(userData.USERNAME);
                    userAd.EMAIL.Should().Be(userData.EMAIL);
                    userAd.EMPLOYEE_ID.Should().Be(userData.EMPLOYEE_ID);
                    userAd.FULL_NAME.Should().Be(userData.FULL_NAME);

                }
            }
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
                var mock = _context.Get<Mock<ICommonService>>("MockExternalData");
                List<Employee> emps=new List<Employee>();
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
                    emps.Add(new Employee()
                    {
                        EMPLOYEE_ID = empId,
                        NAME = name,
                        POSITION_ID = empInput.POSITION_ID,
                        POS_DEPT = empInput.POS_DEPT,
                        SUPERIOR_NAME = empInput.SUPERIOR_NAME,
                        SUPERIOR_POS = empInput.SUPERIOR_POS,
                        MOR_NAME = empInput.MOR_NAME,
                        MOR_POS = empInput.MOR_POS
                    });
                }
                mock.Setup(x => x.ExecutequeryByParam<List<Employee>>("QRY_EMP_APPROVAL", It.IsAny<string>())).Returns(emps);
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
            var commonservice = appHost.Container.Resolve<ICommonService>();
          var emp=  commonservice.GetEmployeeById(p0);
            emp.Should().NotBeNull();
            _context.Add("EmployeeDataCount", 1);
        }



    }
}
