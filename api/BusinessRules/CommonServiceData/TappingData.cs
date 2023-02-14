using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessRules.CommonServiceData
{
    public class TappingData
    {
        public string EMPLOYEE_ID { get; set; }


        public DateTime? CHECKIN_DATETIME { get; set; }
        public DateTime? CHECKOUT_DATETIME { get; set; }
        public string CHECKIN_POINT_NAME { get; set; }
        public string CHECKOUT_POINT_NAME { get; set; }
        public decimal? DURATION_HOURS { get; set; }


    }
   
}
