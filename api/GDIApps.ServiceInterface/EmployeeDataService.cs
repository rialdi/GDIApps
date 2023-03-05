using GDIApps.ServiceModel;
using GDIApps.ValeCommonRules;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ServiceStack.Svg;

namespace GDIApps.ServiceInterface
{
    public class EmployeeDataService:BaseService
    {
        public IAutoQueryData AutoQuery { get; set; }
        public object Get(EmployeeQuery query)
        {
            var externalData = this.TryResolve<ICommonService>();
            if (externalData != null)
            {
                string odataparam = "";
                if (query.ODataParams != null)
                {
                    odataparam = query.ODataParams.Join("&");
                   
                }
                if (query.Take.HasValue)
                {
                    odataparam += "&$top=" + query.Take.Value.ToString();

                }
                if (query.Skip.HasValue)
                {
                    odataparam += "&$skip=" + query.Skip.Value.ToString();

                }
                if (string.IsNullOrEmpty(odataparam)) {
                    odataparam = "$top=100";
                }
                var empList = externalData.ExecutequeryByParam<List<EmployeePOCO>>("QRY_EMP_APPROVAL",odataparam);
                PocoDataSource<EmployeePOCO> dataPoco=new PocoDataSource<EmployeePOCO>(empList);
                var db = dataPoco.ToDataSource(query, Request);
                var response= AutoQuery.Execute(query, AutoQuery.CreateQuery(query, Request, db), db);
                
                response.Total = 100;
                return response;
           }
            return null;
         
        }
    }
}
