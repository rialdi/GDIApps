
using GDIApps.ServiceModel;
using GDIApps.ServiceModel.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFlowProjectGDIApps.Drivers
{
    public interface IDriver
    {
        void CheckAndLogIfUserAdmin(Table table);
        void CheckHost(string p0);
        void CreateDraftClaim(string p0, Table table);
        void DeleteLookupById(int id);
        List<EmployeeOption> GetEmployeeOptionsByLoggedUser();
        void InputNewLookupSet(Table table);
        void SubmitOvertime(string otnumber, Table table);
        void UpdateLookup(Lookup lookup);
    }
}
