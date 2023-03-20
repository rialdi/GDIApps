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
using ServiceStack.OrmLite;
using GDIApps.ServiceModel.Types.Tables;
using System.Reflection;
using ServiceStack.Text;

namespace GDIApps.ServiceInterface
{
    public class OvertimeService:Service
    {
        public IAutoQueryDb AutoQuery { get; set; }
        public CreateOvertimeResponse Post(CreateOvertimeDraft request)
        {
            CreateOvertimeResponse response = new CreateOvertimeResponse();
            foreach (var empId in request.EmployeeIds)
            {
                var itemResponse = new CRUDClaimItemResponse();
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
        public CRUDClaimItemResponse Post(SubmitClaimRequest request)
        {
            var response = new CRUDClaimItemResponse();
            response.OtNumber = request.OtNumber;
            try
            {
                Rules.SubmitClaim(request.OtNumber);
                response.Success = true;
             

            }catch(Exception ex)
            {
                response.Success = false;
                response.ErrorMessage = ex.Message;
             
            }
            return response;

        }
        public CRUDClaimItemResponse Any(UpdateClaimRequest request)
        {
            var response = new CRUDClaimItemResponse();
            try 
            {
                Rules.UpdateClaimEntries(request.OTNumber, request.OTHour, request.ReasonCode);
                response.Success = true;
            }
            catch(Exception ex)
            {
                response.Success=false;
                response.ErrorMessage = ex.Message;
            }
            return response;
        }
        public CRUDClaimItemResponse Any(DeleteClaimRequest request)
        {
            var response = new CRUDClaimItemResponse();
            try
            {
                Rules.DeleteClaim(request.OTNumber);
                response.Success = true;
                return response;
            }catch(Exception ex)
            {
                response.Success=false;
                response.ErrorMessage = ex.Message;
                return response;
            }
        }
        public object Any(QueryOvertimeDraft query)
        {
            using var db = AutoQuery.GetDb(query, base.Request);
            var q = AutoQuery.CreateQuery(query, base.Request, db);
            q.Where(x => x.STATUS == "DRAFT");

            //  q.And<Overtime>(c => c.STATUS == "DRAFT",)
            return AutoQuery.Execute(query, q, Request);

        }
        public EmployeeSelectionsResponse Get(EmployeeSelections request)
        {
            var employees = Rules.GetEmployeeSelections(request.ODataParam);
            var res = new EmployeeSelectionsResponse()
            {
                Employees = employees.Select(d => new EmployeeOption()
                {
                    EMPLOYEE_ID = d.EMPLOYEE_ID,
                    NAME = d.NAME,
                    POS_DEPT = d.POS_DEPT
                }).ToList()
            };
           
            return res;
        
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
