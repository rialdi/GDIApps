using BusinessRules;
using GDIApps.ValeCommonRules;
using ServiceStack;
using ServiceStack.Auth;
using ServiceStack.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDIApps.ServiceInterface
{
    public  class BaseService:Service
    {
        LogicRules _rules;
        protected LogicRules Rules
        {
            get
            {
                if (_rules == null)
                {

                    var dbcnf = this.Resolve<IDbConnectionFactory>();
                    var session = SessionAs<AuthUserSession>();
                    IUserAuth user2 = AuthRepository.GetUserAuth(session.UserAuthId);
                    var authSession = this.GetSession();
                    var externalData = this.TryResolve<ICommonService>();
                    if (authSession != null)
                    {
                        var user = this.GetSession().GetUserAuthName();
                        var userData = AuthRepository.GetUserAuthByUserName(user);
                        _rules = new LogicRules(dbcnf, userData, externalData);
                    }
                    else
                    {
                        _rules = new LogicRules(dbcnf, null, externalData);
                    }

                }
                return _rules;
            }
        }
    }
}
