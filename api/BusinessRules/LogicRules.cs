using GDIApps.ServiceModel.Types.Tables;
using ServiceStack.Auth;
using ServiceStack.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.OrmLite;
namespace BusinessRules
{
    public class LogicRules
    {
        IDbConnectionFactory _db;
        IUserAuth _user;
        IExternalData _externalData;
        public LogicRules(IDbConnectionFactory db,IUserAuth userAuth,IExternalData externalData) 
        {
            _db= db;
            _user = userAuth;
            _externalData=externalData;
        }

        public string CreateNewOvertimeDraft(DateTime otDate, string employeeId)
        {
          
               
                var emp = _externalData.GetEmployeeById(employeeId);
                Overtime data = new Overtime();
                data.STATUS = "DRAFT";
                data.CreatedBy = _user.UserName;
                data.CreatedDate = DateTime.Now;
                data.EMPLOYEE_ID = emp.EMPLOYEE_ID;
                data.OT_DATE = otDate;
                data.NAME = emp.NAME;
                data.POS_DEPT = emp.POS_DEPT;
                data.POSITION_ID= emp.POSITION_ID;
            data.OT_NUMBER = GenerateOtNumbers(employeeId);
                using(var cn = _db.Open())
                {
                    cn.Insert<Overtime>(data);
                }
            return data.OT_NUMBER;
            
        }

        private string GenerateOtNumbers(string employeeId)
        {
           throw new NotImplementedException();
        }

        public void sendEmail(string recipient,string subject,string body,string cc )
        {

        }
    }
}
