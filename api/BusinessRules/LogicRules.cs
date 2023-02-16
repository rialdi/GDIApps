using GDIApps.ServiceModel.Types.Tables;
using ServiceStack.Auth;
using ServiceStack.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.OrmLite;
using BusinessRules.CommonServiceData;

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
            data.OT_NUMBER = GenerateOtNumbers(employeeId,data.CreatedDate);
            data.CreatedBy = _user.UserName;
            data.ModifiedBy= _user.UserName;
            data.ModifiedDate = DateTime.Now;
                using(var cn = _db.Open())
                {
                    cn.Insert<Overtime>(data);
                }
            return data.OT_NUMBER;
            
        }

        private string GenerateOtNumbers(string employeeId,DateTime date)
        {
            DateTime startDate = new DateTime(date.Year, date.Month, 1);
            DateTime endDate = startDate.AddMonths(1).AddDays(-1);
            string seqFormat = "";
          using(var cn = _db.Open())
            {
             var cntr=   cn.Select<Overtime>(o => o.EMPLOYEE_ID == employeeId && o.OT_DATE >= startDate && o.OT_DATE <= endDate).Count;
                seqFormat = string.Format("{0:0000}", cntr);
            }
            return string.Format("OTS-{0}-{1}-{2}-{3}", seqFormat, startDate.Month, startDate.Year, employeeId.TrimStart('0'));
        }

        public void sendEmail(string recipient,string subject,string body,string cc )
        {

        }

        public List<Employee> GetEmployeeSelections()
        {
           return _externalData.ExecutequeryByParam<List<Employee>>("QRY_EMP_APPROVAL", "");
        }
    }
}
