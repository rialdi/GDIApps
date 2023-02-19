using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDIApps.ServiceModel.Types.Tables
{
    [CompositeIndex(true, nameof(OTNumber))]
    public class SimpleOvertime
    {
        [AutoIncrement]
        public int Id { get; set; }
        public string OTNumber { get; set; }
public string UserName { get; set; }
        public DateTime OvertimeDate { get; set; }
        public string Status { get; set; }
        public string Task { get; set; }
        public  string Reason { get; set; }
    }
}
