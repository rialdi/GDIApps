using ServiceStack;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDIApps.ServiceModel.Types.Tables
{
    [CompositeIndex(true, nameof(OT_NUMBER))]
    public class Overtime: AuditBase
    {
        [AutoIncrement]
        public int Id { get; set; }
      
        public string OT_NUMBER { get; set; }
        public string EMPLOYEE_ID { get; set; }
        public string NAME { get; set; }
        public DateTime OT_DATE { get; set; }
        public decimal OT_HOUR { get; set; }
        public string OT_REASON { get; set; }
        public string OT_REASON_CODE { get; set; }
        public string POS_DEPT { get; set; }
        public string POSITION_ID { get; set; }
        public string FIRST_APPROVER_ID { get; set; }
        public string FIRST_APPROVER_NAME { get; set; }
        public string SECONDARY_APPROVER_ID { get; set; }   
        public string SECONDARY_APPROVER_NAME { get; set; }
        public string STATUS { get; set; }
    }
}
