using GDIApps.ServiceModel.Types.Tables;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDIApps.ServiceModel
{
    public class CreateNewOTRequest:IReturn<CreateNewOTResponse>
    {
        //| UserName | Reason    | Task   | OvertimeDate |
        public string UserName { get; set; }
        public string Reason { get; set; }
        public string Task { get; set; }
        public string OvertimeDate { get; set; }
    }
    public class CreateNewOTResponse
    {
        public List<SimpleOvertime> Items { get; set; }=new List<SimpleOvertime>();
    }
}
