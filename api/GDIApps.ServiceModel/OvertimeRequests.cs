using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDIApps.ServiceModel
{
    [Route("/createclaim", "POST")]
    public class CreateOvertimeDraft: IPost, IReturn<CreateClaimResponse>
    {
        public string OtDate { get; set; }
        public List<string> EmployeeIds { get; set; }
    }
    public class CreateClaimItemResponse
    {
        public bool Success { get; set; }
        public string OtNumber { get; set; }
        public string ErrorMessage { get; set;}
    }
    public class CreateClaimResponse
    {
        public List<CreateClaimItemResponse> Items { get; set; }=new List<CreateClaimItemResponse>();
    }
}
