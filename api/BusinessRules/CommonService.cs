using BusinessRules.CommonServiceData;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessRules
{

    public class CommonService : IExternalData
    {
        const string PARAM_FOR_EMPLOYEE_ID = "@badgeno";
         const string PARAM_FOR_TAP_DATE_START = "@date_start";
         const string PARAM_FOR_TAP_DATE_END = "@date_end";
        public T ExecutequeryByParam<T>(string code, string paramurl)
        {
            var url = string.Format("{0}/executequerybyparam/{1}", GetBaseWebAPI(), code);
            if (!string.IsNullOrEmpty(paramurl))
                url += "?" + paramurl;
            var releases = url.GetJsonFromUrl().FromJson<T>();
            return releases;
        }

        public ActiveDirectoryUser GetADUserById(string id)
        {
            var result = ExecutequeryByParam<List<ActiveDirectoryUser>>("QRY_ADLIST_EX", "$filter=EMPLOYEE_ID eq '" + id + "'");
            return result.FirstOrDefault();
        }

        public string GetBaseWebAPI()
        {
            return "http://sorappsrvp04/PTVI.CommonServices/api/externaldata/";
        }

        public Employee GetEmployeeById(string id)
        {
            var result = ExecutequeryByParam<List<Employee>>("QRY_EMP_APPROVAL", "$filter=EMPLOYEE_ID eq '" + id + "'");
            return result.FirstOrDefault();
        }

        public TappingData GetTappingData(string id, DateTime date)
        {
            throw new NotImplementedException();
        }
    }
}
