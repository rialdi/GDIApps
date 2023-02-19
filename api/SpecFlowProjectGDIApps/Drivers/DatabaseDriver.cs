using GDIApps.ServiceModel.Types;
using GDIApps.ServiceModel.Types.Tables;
using Org.BouncyCastle.Crypto.Digests;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.Dapper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow.Assist;

namespace SpecFlowProjectGDIApps.Drivers
{
    public class DatabaseDriver
    {
        IDbConnectionFactory _dbConn;
        public DatabaseDriver(IDbConnectionFactory dbCon)
        {
            _dbConn = dbCon;
        }

        internal void AddLookupIfNotExist(Table table)
        {
            IEnumerable<dynamic> setData = table.CreateDynamicSet(doTypeConversion: false);
            List<Lookup> lokups = new List<Lookup>();
            using (var cn = _dbConn.Open())
            {
                lokups = cn.Select<Lookup>();

            }
            foreach (var data in setData)
            {
                var lookupType = Enum.Parse<LOOKUPTYPE>(data.LookupType, true);
                if(!lokups.Exists(x=>x.LookupValue==data.LookupValue && x.LookupText == data.LookupText && x.LookupType.ToString() == data.LookupType))
                {
                    Lookup newLookup = new Lookup();
                    newLookup.LookupType = lookupType;
                    newLookup.IsActive = true;
                    newLookup.LookupText=data.LookupText;
                    newLookup.LookupValue = data.LookupValue;
                    newLookup.CreatedBy = "test system";
                    newLookup.CreatedDate = DateTime.Now;
                    newLookup.ModifiedDate = newLookup.CreatedDate;
                    newLookup.ModifiedBy = newLookup.CreatedBy;
                    using(var cn = _dbConn.Open())
                    {
                        cn.Insert<Lookup>(newLookup);

                    }
                }
            }
        }
        internal void AddLookupIfNotExist(IEnumerable<dynamic> setData,LOOKUPTYPE lookuptype)
        {

          
            List<Lookup> lokups = new List<Lookup>();
            using (var cn = _dbConn.Open())
            {
                cn.CreateTableIfNotExists<Lookup>();
                lokups = cn.Select<Lookup>();

            }
            foreach (var data in setData)
            {
              
                if (!lokups.Exists(x => x.LookupValue == data.LookupValue && x.LookupText == data.LookupText && x.LookupType.ToString() == data.LookupType))
                {
                    Lookup newLookup = new Lookup();
                    newLookup.LookupType = lookuptype;
                    newLookup.IsActive = true;
                    newLookup.LookupText = data.LookupText;
                    newLookup.LookupValue = data.LookupValue;
                    newLookup.CreatedBy = "test system";
                    newLookup.CreatedDate = DateTime.Now;
                    newLookup.ModifiedDate = newLookup.CreatedDate;
                    newLookup.ModifiedBy = newLookup.CreatedBy;
                    using (var cn = _dbConn.Open())
                    {
                        cn.Insert<Lookup>(newLookup);

                    }
                }
            }
        }

        internal void CheckIsLookupExist(IEnumerable<dynamic> setData)
        {
            List<Lookup> lokups = new List<Lookup>();
            using (var cn = _dbConn.Open())
            {
                lokups = cn.Select<Lookup>();

            }
            foreach (dynamic data in setData)
            {
                var lookupType = Enum.Parse<LOOKUPTYPE>(data.LookupType, true);
                lokups.FirstOrDefault(l=>l.LookupText==data.LookupText && l.LookupValue==data.LookupValue && l.LookupType== lookupType).Should().NotBeNull();
               
            }
        }
        internal void CheckIsLookupExist(IEnumerable<dynamic> setData,LOOKUPTYPE lktype)
        {
            List<Lookup> lokups = new List<Lookup>();
            using (var cn = _dbConn.Open())
            {
                lokups = cn.Select<Lookup>();

            }
            foreach (dynamic data in setData)
            {
                var lookupType = Enum.Parse<LOOKUPTYPE>(data.LookupType, true);
                lokups.FirstOrDefault(l => l.LookupText == data.LookupText && l.LookupType == lktype).Should().NotBeNull();

            }
        }

        public Lookup GetLookupByTypeAndValue(string lookuptype, string lookupvalue)
        {
            Lookup currentLookup = null;
            using (var cn = _dbConn.Open())
            {
                var lookupType = Enum.Parse<LOOKUPTYPE>(lookuptype, true);
              currentLookup= cn.Single<Lookup>(l => l.LookupType.ToString() == lookuptype.ToString() && l.LookupValue == lookupvalue);

            }
            currentLookup.Should().NotBeNull();
            return currentLookup;
        }

        public Lookup GetLookupById(int id)
        {
            Lookup currentLookup = null;
            using (var cn = _dbConn.Open())
            {
                
                currentLookup = cn.SingleById<Lookup>(id);


            }
            return currentLookup;
        }

        internal void CheckIfOTExisitWithStatus(string status, Table table,bool checkApprover=false,bool checkentry=false)
        {
           var listExpected=table.CreateDynamicSet(doTypeConversion:false);
            foreach (var input in listExpected)
            {
                Overtime readlData = null;
                string otnumber = input.OT_NUMBER;
                var date = DateTime.ParseExact(input.OT_DATE, "dd-MM-yyyy", CultureInfo.GetCultureInfo("en-us"));
                using var db = _dbConn.Open();
                readlData = db.Select<Overtime>().FirstOrDefault();

                readlData.STATUS.Should().Be(status);
                readlData.EMPLOYEE_ID.Should().Be(input.EMPLOYEE_ID);
                readlData.NAME.Should().Be(input.NAME);
                readlData.POSITION_ID.Should().Be(input.POSITION_ID);
                readlData.POS_DEPT.Should().Be(input.POS_DEPT);
                readlData.OT_DATE.Should().Be(date);
                if (checkApprover)
                {
                    readlData.FIRST_APPROVER_ID.Should().Be(input.FIRST_APPROVER_ID);
                    readlData.FIRST_APPROVER_NAME.Should().Be(input.FIRST_APPROVER_NAME);
                    readlData.SECONDARY_APPROVER_ID.Should().Be(input.SECONDARY_APPROVER_ID);
                    readlData.SECONDARY_APPROVER_NAME.Should().Be(input.SECONDARY_APPROVER_NAME);
                }

                if (checkentry) 
                { 
                    readlData.OT_REASON.Should().Be(input.OT_REASON);
              //  readlData.OT_REASON_CODE.Should().Be(input.OT_REASON_DESC);
                    readlData.OT_HOUR.Should().Be(input.OT_HOUR);
            }
               
              
           
            }
        }
    }
}
