using BusinessRules.CommonServiceData;

namespace BusinessRules
{
    public interface IExternalData
    {
        T ExecutequeryByParam<T>(string code, string paramurl);
        string GetBaseWebAPI();
        Employee GetEmployeeById(string id);
        ActiveDirectoryUser GetADUserById(string id);
        TappingData GetTappingData(string id, DateTime date);
    }
}