using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GDIApps.ServiceModel.Types.Tables;
using ServiceStack;

namespace GDIApps.ServiceModel
{
      [Route("/createclaim", "POST")]
    public class CreateOvertimeDraft: IPost, IReturn<CRUDClaimItemResponse>
    {
        public string OtDate { get; set; }
        public List<string> EmployeeIds { get; set; }=new List<string>();
    }
    public class CRUDClaimItemResponse
    {
        public bool Success { get; set; }
        public string OtNumber { get; set; }
        public string ErrorMessage { get; set;}
    }
    public class CreateOvertimeResponse
    {
        public List<CRUDClaimItemResponse> Items { get; set; }=new List<CRUDClaimItemResponse>();
    }

    [Route("/submitclaim", "POST")]
    public class SubmitClaimRequest:IReturn<CRUDClaimItemResponse>
    {
        public string OtNumber { get; set; }
      
    }


    
 public class UpdateClaimRequest : IReturn<CRUDClaimItemResponse>
    {
        public string OTNumber { get; set; }
        public decimal? OTHour { get; set; }
        public string ReasonCode { get; set; }
    }


    public class DeleteClaimRequest:IReturn<CRUDClaimItemResponse>
    {
        public string OTNumber { get; set; }
    }
 
    [Route("/OvertimeDraft", "GET")]
    public class QueryOvertimeDraft : QueryDb<Overtime>
    {
        public string Id { get; set; }
    }
}