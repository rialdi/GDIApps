using BusinessRules;
using ServiceStack.Auth;
using ServiceStack.Data;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GDIApps.ServiceModel;
using System.Globalization;

namespace GDIApps.ServiceInterface
{
    public class OvertimeService:Service
    {
        public CreateClaimResponse Post(CreateOvertimeDraft request)
        {
            CreateClaimResponse response = new CreateClaimResponse();
            foreach (var empId in request.EmployeeIds)
            {
                var itemResponse = new CreateClaimItemResponse();
                try
                {
                    DateTime dt = DateTime.ParseExact(request.OtDate, "dd-MMM-yyyy", CultureInfo.GetCultureInfo("en-US"));
                    string newOtNuumber = _rules.CreateNewOvertimeDraft(dt, empId);
                    itemResponse.Success = true;
                    itemResponse.OtNumber = newOtNuumber;

                }
                catch (Exception ex)
                {
                    itemResponse.Success = false;
                    itemResponse.ErrorMessage = ex.Message;
                }
                response.Items.Add(itemResponse);
            }
            return response;

        }
        LogicRules _rules = null;
    private LogicRules Rules
    {
        get
        {
            if (_rules == null)
            {

                var dbcnf = this.Resolve<IDbConnectionFactory>();
                var session = SessionAs<AuthUserSession>();
                IUserAuth user2 = AuthRepository.GetUserAuth(session.UserAuthId);
                var authSession = this.GetSession();
                var externalData = this.TryResolve<IExternalData>();
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
