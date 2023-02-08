using ServiceStack.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFlowProjectGDIApps.Drivers
{
    public class DatabaseDriver
    {
        IDbConnectionFactory _dbConn;
        public DatabaseDriver(IDbConnectionFactory dbCon)
        {
            _dbConn = dbCon;
        }
    }
}
