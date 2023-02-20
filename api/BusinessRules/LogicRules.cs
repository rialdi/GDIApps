
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



        public void sendEmail(string recipient,string subject,string body,string cc )
        {

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
