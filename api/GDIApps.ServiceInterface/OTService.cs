using BusinessRules;
using GDIApps.ServiceModel;
using GDIApps.ServiceModel.Types.Tables;
using GDIApps.ValeCommonRules;
using ServiceStack;
using ServiceStack.Auth;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDIApps.ServiceInterface
{
    public class OTService:BaseService
    {
        public CreateNewOTResponse Any(CreateNewOTRequest request)
        {
         //   _rules.CreateSimpleOvertime(request);
            var dbcnf = this.Resolve<IDbConnectionFactory>();
            List<SimpleOvertime> resultItems = new List<SimpleOvertime>();
            using(var cn = dbcnf.Open())
            {
                var ot = new SimpleOvertime();
                ot.UserName= request.UserName;
                ot.Reason= request.Reason;
                ot.Status = "Submitted";
                ot.Task= request.Task;
                ot.OvertimeDate = DateTime.ParseExact(request.OvertimeDate, "dd-MMM-yyyy", CultureInfo.GetCultureInfo("en-US"));
                ot.OTNumber = GenerateOtNumbers(ot.OvertimeDate);
            var udx=   cn.Insert<SimpleOvertime>(ot);
                resultItems.Add(ot);
               // cn.Insert<SimpleOvertime>(request.)
            }

            return new CreateNewOTResponse() { Items = resultItems };
           // throw new NotImplementedException();
        }
        private string GenerateOtNumbers( DateTime date)
        {
            DateTime startDate = new DateTime(date.Year, date.Month, 1);
            DateTime endDate = startDate.AddMonths(1).AddDays(-1);
            string seqFormat = "";
            var dbcnf = this.Resolve<IDbConnectionFactory>();
            using (var cn = dbcnf.Open())
            {
                var cntr = cn.Select<SimpleOvertime>(o =>  o.OvertimeDate >= startDate && o.OvertimeDate <= endDate).Count;
                seqFormat = string.Format("{0:0000}", cntr+1);
            }
            return string.Format("OTS-{0}-{1:00}-{2}", seqFormat, startDate.Month, startDate.Year);
        }

      
    }
}
