using GDIApps.ServiceModel.Types.Tables;
using ServiceStack.Auth;
using ServiceStack.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceStack.OrmLite;
using GDIApps.ValeCommonRules;
using GDIApps.ValeCommonRules.CommonServiceData;
using ServiceStack;
using Microsoft.Extensions.DependencyInjection;
using GDIApps.ServiceModel.Types;

namespace BusinessRules
{
    public class LogicRules
    {
        IDbConnectionFactory _db;
        IUserAuth _user;
        ICommonService _externalData;
        public LogicRules(IDbConnectionFactory db,IUserAuth userAuth,ICommonService externalData) 
        {
            _db= db;
            _user = userAuth;
            _externalData=externalData;
        }

        public string CreateNewOvertimeDraft(DateTime otDate, string employeeId,DateTime? createdDate=null)
        {
                if (createdDate == null) 
                    createdDate = DateTime.Now; 
                var emp = _externalData.GetEmployeeById(employeeId);
            Overtime data = CreateAuditBase<Overtime>();
                data.STATUS = "DRAFT";
             
                data.EMPLOYEE_ID = emp.EMPLOYEE_ID;
                data.OT_DATE = otDate;
                data.NAME = emp.NAME;
                data.POS_DEPT = emp.POS_DEPT;
                data.POSITION_ID= emp.POSITION_ID;
                data.OT_NUMBER = GenerateOtNumbers(employeeId,createdDate.Value);
             
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
                if (cntr > 0)
                    cntr++;
                seqFormat = string.Format("{0:0000}", cntr);
            }
            return string.Format("OTS-{0}-{1:00}-{2}-{3}", seqFormat, startDate.Month, startDate.Year, employeeId.TrimStart('0'));
        }

        public void sendEmail(string recipient,string subject,string body,string cc )
        {

        }

        public List<Employee> GetEmployeeSelections()
        {
           return _externalData.ExecutequeryByParam<List<Employee>>("QRY_EMP_APPROVAL", "");
        }

        public void SubmitClaim(string otNumber, string reasonCode, decimal otHour)
        {
            using var cn = _db.Open();
            var overtimeData=cn.Select<Overtime>(o=>o.OT_NUMBER==otNumber).FirstOrDefault();
            if (overtimeData == null)
                throw new Exception("OT Number: " + otNumber + " not found");
            overtimeData.STATUS = "WAITING FOR FIRST APPROVAL";
            overtimeData.OT_HOUR= otHour;
            overtimeData.OT_REASON_CODE = reasonCode;
            var reason = cn.Select<Lookup>(l => l.LookupType == LOOKUPTYPE.OT_REASON && l.LookupValue==reasonCode).FirstOrDefault();
            overtimeData.OT_REASON = reason.LookupText;
            FillApprover(overtimeData);
            UpdateAudit<Overtime>(overtimeData);
            cn.Save(overtimeData);

        }

        private void FillApprover(Overtime overtimeData)
        {
            var employee = _externalData.GetEmployeeById(overtimeData.EMPLOYEE_ID);
            var superiorInfo = employee.SUPERIOR_NAME.Split("-");
            var managerInfo=employee.MOR_NAME.Split("-");

            overtimeData.FIRST_APPROVER_ID = superiorInfo[0].Trim();
            overtimeData.FIRST_APPROVER_NAME = superiorInfo[1].Trim();
            overtimeData.SECONDARY_APPROVER_ID = managerInfo[0].Trim();
            overtimeData.SECONDARY_APPROVER_NAME= managerInfo[1].Trim();
        }

        private T CreateAuditBase<T>() where T : AuditBase, new()
        {
            var obj = new T();
            var username = _user == null ? "anonymous" : _user.UserName;

            obj.CreatedBy = username;
            obj.CreatedDate = DateTime.Now;
            obj.ModifiedBy = username; ;
            obj.ModifiedDate = obj.CreatedDate;
            return obj;
        }
        private void UpdateAudit<T>(T data) where T : AuditBase
        {
            var username = _user == null ? "anonymous" : _user.UserName;
            data.ModifiedBy = username;
            data.ModifiedDate = DateTime.Now;
        }
        private void DeleteAudit<T>(T data) where T : AuditBase
        {
            var username = _user == null ? "anonymous" : _user.UserName;
            data.DeletedBy = username;
            data.DeletedDate = DateTime.Now;
        }
    }
}
