using GDIApps.ServiceModel.Types;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.Dapper;
using System;
using System.Collections.Generic;
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

        internal void CheckIfOTExisitWithStatus(string status, Table table)
        {
            throw new NotImplementedException();
        }
    }
}
