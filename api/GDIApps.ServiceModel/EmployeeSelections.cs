using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GDIApps.ServiceModel
{
    [Route("/employeeoptions")]
    public class EmployeeSelections : IReturn<EmployeeSelectionsResponse>
    {
        public string ODataParam { get; set; } = "";
    }
    [DataContract]
    public class EmployeeOption
    {
        [DataMember(Name = "EMPLOYEE_ID")]
        public string EMPLOYEE_ID { get; set; }
        [DataMember(Name = "NAME")]
        public string NAME { get; set; }
        [DataMember(Name = "POS_DEPT")]
        public string POS_DEPT { get; set; }
    }
    public class EmployeeSelectionsResponse
    {
        public List<EmployeeOption> Employees { get; set; }
    }
}
