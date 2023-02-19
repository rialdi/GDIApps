using GDIApps.ServiceModel.Types;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using SpecFlowProjectGDIApps.Drivers;
using System;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
namespace SpecFlowProjectGDIApps.StepDefinitions
{
    [Binding]
    public class LookupCRUDStepDefinitions

    { 
         ScenarioContext _context;
    IDriver _driver;
    DatabaseDriver dbDriver;
    public LookupCRUDStepDefinitions(ScenarioContext context, IDriver driver)
        {
        _context = context;
        _driver = driver;
            var cnf = context.Get<IDbConnectionFactory>("DbConn");
            dbDriver = new DatabaseDriver(cnf);
            using (var cn = cnf.Open())
            {
                cn.CreateTableIfNotExists<Lookup>();
            }

      
         
    }
        [Given(@"following user is logged in as admin")]
        public void GivenFollowingUserIsLoggedInAsAdmin(Table table)
        {
            _driver.CheckAndLogIfUserAdmin(table);
        }

        [When(@"User Input new lookup")]
        public void WhenUserInputNewLookup(Table table)
        {
            _context.Add("LookupInputTable", table.CreateDynamicSet());
            _driver.InputNewLookupSet(table);
        }

        [Then(@"The new lookup saved")]
        public void ThenTheNewLookupSaved()
        {

            dbDriver.CheckIsLookupExist(_context.Get<IEnumerable<dynamic>>("LookupInputTable"));
 
            //  Console.WriteLine()
        }

        [Given(@"Exist Lookup data in following")]
        public void GivenExistLookupDataInFollowing(Table table)
        {
            dbDriver.AddLookupIfNotExist(table);
        }

        [Given(@"User Change lookup with LookupType ""([^""]*)"" and value ""([^""]*)"" into")]
        public void GivenUserChangeLookupWithLookupTypeAndValueInto(string lookuptype, string lookupvalue, Table table)
        {
         Lookup selectedLookup=   dbDriver.GetLookupByTypeAndValue(lookuptype, lookupvalue);
            dynamic changeValues = table.CreateDynamicInstance();
            selectedLookup.LookupText = changeValues.LookupText;
            selectedLookup.LookupValue = changeValues.LookupValue;
            selectedLookup.LookupType=Enum.Parse<LOOKUPTYPE>(changeValues.LookupType,true);
           
            _context.Add("ModifiedLookup",selectedLookup);
        }

        [When(@"User Save Lookup")]
        public void WhenUserSaveLookup()
        {
            _driver.UpdateLookup(_context.Get<Lookup>("ModifiedLookup"));
        }

        [Then(@"The Lookup is changed successfully")]
        public void ThenTheLookupIsChangedSuccessfully()
        {
            var modifedLookup = _context.Get<Lookup>("ModifiedLookup");
            Lookup lookup = dbDriver.GetLookupById(modifedLookup.Id);
            lookup.LookupType.Should().Be(modifedLookup.LookupType);
            lookup.LookupText.Should().Be(modifedLookup.LookupText);
            lookup.LookupValue.Should().Be(modifedLookup.LookupValue);
        }

        [When(@"User Delete lookup data with type ""([^""]*)"" and value ""([^""]*)""")]
        public void WhenUserDeleteLookupDataWithTypeAndValue(string type, string value)
        {
            var lookup = dbDriver.GetLookupByTypeAndValue(type, value);
            _context.Add("DeletedLookupId", lookup.Id);
            _driver.DeleteLookupById(lookup.Id);
        }

        [Then(@"following lookup is not exists")]
        public void ThenFollowingLookupIsNotExists(Table table)
        {
            dbDriver.GetLookupById(_context.Get<int>("DeletedLookupId")).Should().BeNull();
        }

    }
}
