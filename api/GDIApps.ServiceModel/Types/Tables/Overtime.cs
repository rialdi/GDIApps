using ServiceStack;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GDIApps.ServiceModel.Types.Tables
{
    [DataContract]
    [CompositeIndex(true, nameof(OT_NUMBER))]
    public class Overtime: AuditBase
    {
        [AutoIncrement]
        public int Id { get; set; }
        [DataMember(Name ="OT_NUMBER")]
        public string OT_NUMBER { get; set; }
        [DataMember(Name = "EMPLOYEE_ID")]
        public string EMPLOYEE_ID { get; set; }

        [DataMember(Name  = "NAME")]
        public string NAME { get; set; }

        [DataMember(Name ="OT_DATE")]
        public DateTime OT_DATE { get; set; }

        [DataMember(Name = "OT_HOUR")]
        public decimal OT_HOUR { get; set; }
        [DataMember(Name = "OT_REASON")]
        public string OT_REASON { get; set; }
        [DataMember(Name = "OT_REASON_CODE")]
        public string OT_REASON_CODE { get; set; }

        [DataMember(Name = "POS_DEPT")] 
        public string POS_DEPT { get; set; }

        [DataMember(Name = "POSITION_ID")]
        public string POSITION_ID { get; set; }

        [DataMember(Name = "FIRST_APPROVER_ID")]
        public string FIRST_APPROVER_ID { get; set; }

        [DataMember(Name = "FIRST_APPROVER_NAME")]
        public string FIRST_APPROVER_NAME { get; set; }

        [DataMember(Name = "SECONDARY_APPROVER_ID")]
        public string SECONDARY_APPROVER_ID { get; set; }

        [DataMember(Name = "SECONDARY_APPROVER_NAME")]
        public string SECONDARY_APPROVER_NAME { get; set; }

        [DataMember(Name = "STATUS")]
        public string STATUS { get; set; }
    }
}
