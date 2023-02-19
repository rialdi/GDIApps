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
using GDIApps.ValeCommonRules;

namespace GDIApps.ServiceInterface
{
    public class OvertimeService:Service
    {
        public CreateOvertimeResponse Post(CreateOvertimeDraft request)
        {
            CreateOvertimeResponse response = new CreateOvertimeResponse();
            foreach (var empId in request.EmployeeIds)
            {
                var itemResponse = new CreateClaimItemResponse();
                try
                {
                    DateTime dt = DateTime.ParseExact(request.OtDate, "dd-MMM-yyyy", CultureInfo.GetCultureInfo("en-US"));
                    string newOtNuumber = Rules.CreateNewOvertimeDraft(dt, empId);
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
        public SubmitClaimResponse Post(SubmitClaimRequest request)
        {
            var response = new SubmitClaimResponse();
            response.OtNumber = request.OtNumber;
            try
            {
                Rules.SubmitClaim(request.OtNumber, request.ReasonCode, request.OtHour);
                response.Success = true;
             

            }catch(Exception ex)
            {
                response.Success = false;
                response.ErrorMessage = ex.Message;
             
            }
            return response;

        }

        public EmployeeSelectionsResponse Get(EmployeeSelections request)
        {
         var employees=  Rules.GetEmployeeSelections();
            return new EmployeeSelectionsResponse()
            {
                Employees = employees.Select(d => new EmployeeOption()
                {
                    EMPLOYEE_ID = d.EMPLOYEE_ID,
                    NAME = d.NAME,
                    POS_DEPT = d.POS_DEPT
                }).ToList()
            };
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
