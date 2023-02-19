using GDIApps.ServiceModel.Types.Tables;
using ServiceStack;
using ServiceStack.Data;
using SpecFlowProjectGDIApps.Drivers;
using System;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using modl= GDIApps.ServiceModel;
using ServiceStack.OrmLite;
using ServiceStack.Auth;
using GDIApps.ServiceModel.Types;
using GDIApps.ServiceModel;

namespace SpecFlowProjectGDIApps.StepDefinitions
{
    [Binding]
    public class CreateOvertimeStepDefinitions
    {
        ScenarioContext _context;
        public CreateOvertimeStepDefinitions(ScenarioContext context)
        {
            _context= context;
            var cnf = context.Get<IDbConnectionFactory>("DbConn");
          //  dbDriver = new DatabaseDriver(cnf);
            using (var cn = cnf.Open())
            {
                cn.CreateTableIfNotExists<SimpleOvertime>();
            }

        }
        [Given(@"user ""([^""]*)"" want create New Overtime data like following")]
        public void GivenUserWantCreateNewOvertimeDataLikeFollowing(string username, Table table)
        {
            //   var client = _context.Get<IServiceClient>("Client");
            _context.Add("UserName", username);
            _context.Add("Input",table.CreateDynamicInstance(doTypeConversion: false));
        }

        [When(@"User Submit The Overtime Data")]
        public void WhenUserSubmitTheOvertimeData()
        {
            var input = _context.Get<dynamic>("Input");

            var client = _context.Get<IServiceClient>("Client");
            client.Send(new Authenticate()
            {
                provider = CredentialsAuthProvider.Name,
                UserName = "admin@email.com",
                Password = "p@55wOrd"
            });
            string ot = input.OvertimeDate;
            CreateNewOTRequest request = new CreateNewOTRequest()
            {
                OvertimeDate = input.OvertimeDate,
                Reason = input.Reason,
                Task = input.Task,
                UserName = input.UserName
            };
            var res = client.Post(request);
            _context.Add("OTItems",res.Items);


        }

        [Then(@"Exist Following Data in Database")]
        public void ThenExistFollowingDataInDatabase(Table table)
        {
            var compareData = table.CreateDynamicSet(doTypeConversion: false);
            var listOt = _context.Get<List<SimpleOvertime>>("OTItems");
            foreach(var data in compareData)
            {
                listOt.Exists(d=>d.OTNumber== data.OTNumber).Should().BeTrue();
               // var OTreal=listOt.FirstOrDefault(d=>d.OTNumber==data.OTNumber);
               
            }
        }
    }
}
