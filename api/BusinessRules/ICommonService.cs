using BusinessRules.CommonServiceData;

namespace BusinessRules
{
    public interface ICommonService
    {
        T ExecutequeryByParam<T>(string code, string paramurl);
        ActiveDirectoryUser GetADUserById(string id);
        string GetBaseWebAPI();
        Employee GetEmployeeById(string id);
        TappingData GetTappingData(string id, DateTime date);
    }
}