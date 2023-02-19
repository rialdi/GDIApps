using GDIApps.ValeCommonRules.CommonServiceData;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDIApps.ValeCommonRules
{
    public class CommonService : ICommonService
    {
        string _baseuri;
        public CommonService(string baseuri)
        {
            _baseuri= baseuri;
        }
        public CommonService()
        {
            _baseuri = "http://sorappsrvp04/PTVI.CommonServices/api/externaldata/";
        }
        public CommonService(CommonServiceConfig setting)
        {
            _baseuri = setting.BaseUrl;
        }
        public T ExecutequeryByParam<T>(string code, string paramurl)
        {
            var url = string.Format("{0}/executequerybyparam/{1}", GetBaseWebAPI(), code);
            if (!string.IsNullOrEmpty(paramurl))
                url += "?" + paramurl;
            var releases = url.GetJsonFromUrl().FromJson<T>();
            return releases;
        }

        public string GetBaseWebAPI()
        {
            return _baseuri;
        }
        public ActiveDirectoryUser GetADUserById(string id)
        {
            var result = ExecutequeryByParam<List<ActiveDirectoryUser>>("QRY_ADLIST_EX", "$filter=EMPLOYEE_ID eq '" + id + "'");
            return result.FirstOrDefault();
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
