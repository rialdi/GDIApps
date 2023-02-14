using BusinessRules;
using BusinessRules.CommonServiceData;
using GDIApps.ServiceModel.Types;
using GDIApps.ServiceModel.Types.Tables;
using Moq;
using ServiceStack;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using SpecFlowProjectGDIApps.Drivers;
using System;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
namespace SpecFlowProjectGDIApps.StepDefinitions
{
    [Binding]
    public class SimpleOvertimeStepDefinitions
    {
        ScenarioContext _context;
        IDriver _driver;
        DatabaseDriver dbDriver;
        public SimpleOvertimeStepDefinitions(ScenarioContext context, IDriver driver)
        {
            _driver = driver;
            _context = context;
            var cnf = context.Get<IDbConnectionFactory>("DbConn");
            dbDriver = new DatabaseDriver(cnf);
            using (var cn = cnf.Open())
            {
                cn.CreateTableIfNotExists<Overtime>();
               // cn.CreateTableIfNotExists<Lookup>();
            }
        }
       

        [Given(@"Exist Employee AD user information From Common Service")]
        public void GivenExistEmployeeADUserInformationFromCommonService(Table table)
        {
            IEnumerable<dynamic> existUser = table.CreateDynamicSet(doTypeConversion: false);
            if (_context.ScenarioInfo.Tags.Contains("mockingCommonService"))
            {
                var mock = _context.Get<Mock<IExternalData>>("MockExternalData");
                List<ActiveDirectoryUser> users = new List<ActiveDirectoryUser>();
                foreach(var userData in existUser)
                {
                    var user = new ActiveDirectoryUser()
                    {
                        EMAIL = userData.Email,
                        EMPLOYEEID = userData.EMPLOYEEID,
                        FULL_NAME = userData.FULL_NAME,
                        EMPLOYEE_ID = userData.EMPLOYEEID,
                        USERNAME = userData.USERNAME
                    };
                    users.Add(user);
                }
                mock.Setup(x => x.ExecutequeryByParam<List<ActiveDirectoryUser>>(It.IsAny<string>(), It.IsAny<string>()))
                    .Returns(users);
            }
            else
            {
                var commonService = new CommonService();
                foreach(var userData in existUser)
                {
                    ActiveDirectoryUser  userAd = commonService.GetADUserById(userData.EMPLOYEE_ID);
                    userAd.Should().NotBeNull();
                    userAd.USERNAME.Should().Be(userData.USERNAME); 
                    userAd.EMAIL.Should().Be(userData.EMAIL);
                    userAd.EMPLOYEE_ID.Should().Be(userData.EMPLOYEE_ID);
                    userAd.FULL_NAME.Should().Be(userData.FULL_NAME);
                    
                }
            }
        }

        [Given(@"Todays date is ""([^""]*)""")]
        public void GivenTodaysDateIs(string p0)
        {
           // throw new PendingStepException();
        }

    

        [Given(@"user able to select date from (.*) days ago until today")]
        public void GivenUserAbleToSelectDateFromDaysAgoUntilToday(int p0)
        {
           // throw new PendingStepException();
        }

        [Given(@"user able to select these following employee data")]
        public void GivenUserAbleToSelectTheseFollowingEmployeeData(Table table)
        {
            var supposedOptions = table.CreateDynamicSet(doTypeConversion: false);

          List<Employee> emps=   _driver.GetEmployeeOptionsByLoggedUser();
            foreach(dynamic dataExpexted in supposedOptions) {
                var emp = emps.FirstOrDefault(e => e.EMPLOYEE_ID == dataExpexted.EMPLOYEE_ID);
                emp.Should().NotBeNull();
              emp.NAME.Should().Be(dataExpexted.NAME);
                emp.POS_DEPT.Should().Be(dataExpexted.POS_DEPT);
            }
       
        }

        [When(@"User Create new Claim with date ""([^""]*)"" and selected employees are")]
        public void WhenUserCreateNewClaimWithDateAndSelectedEmployeesAre(string p0, Table table)
        {
            //dd-MMM-yyyy
            _driver.CreateDraftClaim(p0, table);
        }

        [Then(@"Exist following overtime data with status ""([^""]*)""")]
        public void ThenExistFollowingOvertimeDataWithStatus(string status, Table table)
        {
            dbDriver.CheckIfOTExisitWithStatus(status, table);
        }

        [Then(@"With following overtime data to be inputed for each overtime record")]
        public void ThenWithFollowingOvertimeDataToBeInputedForEachOvertimeRecord(Table table)
        {
            throw new PendingStepException();
        }

        [When(@"User select an overtime with ot number ""([^""]*)"" and do the following changes")]
        public void WhenUserSelectAnOvertimeWithOtNumberAndDoTheFollowingChanges(string p0, Table table)
        {
            throw new PendingStepException();
        }

        [When(@"User Submit the overtime")]
        public void WhenUserSubmitTheOvertime()
        {
            throw new PendingStepException();
        }
        [Given(@"Following user is logged in as ""([^""]*)"" at Screen Approval")]
        public void GivenFollowingUserIsLoggedInAsAtScreenApproval(string tEAMLEADER, Table table)
        {
            throw new PendingStepException();
        }

        [Given(@"Exist following overtime data with status ""([^""]*)""")]
        public void GivenExistFollowingOvertimeDataWithStatus(string p0, Table table)
        {
            throw new PendingStepException();
        }

        [When(@"User Approve overtime with OT Number ""([^""]*)""")]
        public void WhenUserApproveOvertimeWithOTNumber(string p0)
        {
            throw new PendingStepException();
        }


    }
}
