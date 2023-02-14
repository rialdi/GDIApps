using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessRules.CommonServiceData
{
    public class Employee
    {
        /// <summary>
        /// "Employee ID ex:0000004307"
        /// </summary>
        public string EMPLOYEE_ID { get; set; }
        /// <summary>
        /// Name employee
        /// </summary>
        public string NAME { get; set; }
        /// <summary>
        /// Position ID ex: EMESC0, FPBCR2,..etc
        /// </summary>
        public string POSITION_ID { get; set; }

        /// <summary>
        /// Name Position of employee, ex: SURVEY COORDINATOR
        /// </summary>
        public string POSITION { get; set; }
        /// <summary>
        ///   [Description("level group info ex: LT - (F) TECHNICAL/PROFESSIONAL , LO - (H) OPERATION , ..etc")]
        /// </summary>
        public string LEVEL_GROUP { get; set; }
        /// <summary>
        ///   [Description("ex: M4103 - MINE SURVEY, P3201 - FURNACE OVER, ... etc")]
        /// </summary>
        public string COST_CENTRE { get; set; }

        /// <summary>
        ///  [Description("pattern (badge no) - (Name). ex: 0000007216 - GIA NOOR BAHAGIA")]
        /// </summary>
        public string SUPERIOR_NAME { get; set; }

        /// <summary>
        /// pattern (CODE POS) - (pos nam). ex: EMENG7 - MGR OF SURVEY
        /// </summary>
        public string SUPERIOR_POS { get; set; }
        /// <summary>
        /// [Description("pattern (badge no) - (name)")]
        /// </summary>
        public string MOR_NAME { get; set; }
        /// <summary>
        /// (code pos) - (pos name). ex: DMNGM3 - MGR MINE ENGINEERING.
        /// </summary>
        public string MOR_POS { get; set; }
        /// <summary>
        /// sub dept name
        /// </summary>
        public string POS_SUB_DEPT { get; set; }
        /// <summary>
        /// dept name
        /// </summary>
        public string POS_DEPT { get; set; }
        /// <summary>
        /// sub dept owner. pattern (badge no) - (name) . ex: 0000006595 - YUSRAM RANTESALU
        /// </summary>
        public string SUB_DEPT_OWNER { get; set; }
        /// <summary>
        ///  pattern (badge no) - (name) . ex: 0000006595 - YUSRAM RANTESALU
        /// </summary>
        public string SUPERIOR_MOR { get; set; }

        public string SUPERIOR_MOR_POS { get; set; }
        /// <summary>
        /// ex: L2 - L2, L1 - L1
        /// </summary>
        public string LEVEL_GROUP_SUPERIOR_MOR { get; set; }

      


    }
}
