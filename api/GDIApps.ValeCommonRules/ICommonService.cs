using GDIApps.ValeCommonRules.CommonServiceData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDIApps.ValeCommonRules
{
    public interface ICommonService
    {
        T ExecutequeryByParam<T>(string code, string paramurl);
        string GetBaseWebAPI();
        ActiveDirectoryUser GetADUserById(string id);
       
        Employee GetEmployeeById(string id);
        TappingData GetTappingData(string id, DateTime date);

    }
}
