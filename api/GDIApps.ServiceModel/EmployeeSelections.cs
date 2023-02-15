using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDIApps.ServiceModel
{
    [Route("/employeeoptions")]
    public class EmployeeSelections : IReturn<EmployeeSelectionsResponse>
    {
        
    }
    public class EmployeeOption
    {
        public string EMPLOYEE_ID { get; set; }
        public string NAME { get; set; }

        public string POS_DEPT { get; set; }
    }
    public class EmployeeSelectionsResponse
    {
        public List<EmployeeOption> Employees { get; set; }
    }
}
