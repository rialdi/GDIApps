using System;
using ServiceStack;
using ServiceStack.OrmLite;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ServiceStack.IO;
using GDIApps.ServiceModel;
using GDIApps.ServiceModel.Types;
using ServiceStack.VirtualPath;

namespace GDIApps.ServiceInterface
{
    public  class MyServices : Service
    {
        public IAutoQueryDb AutoQuery { get; set; }
        
        public object Any(DeleteInvoiceAttachment request) {
            using var db = AutoQuery.GetDb<InvoiceAttachment>(Request);
            var currInvoiceAttachment = db.Select<InvoiceAttachment>( w => w.Id == request.Id).FirstNonDefault();
            if(currInvoiceAttachment != null) {
                var allFiles = VirtualFiles.GetAllFiles();
                var attachmentUrl = currInvoiceAttachment.AttachmentUrl.Replace("uploads","App_Data");
                VirtualFiles.DeleteFile(attachmentUrl);
            }
            return db.Delete(currInvoiceAttachment);
        }

        public object Any(GetAppMenuByRole request) {
            using var db = AutoQuery.GetDb<AppRole>(Request);
            var currRole = db.Select<AppRole>( w => w.RoleName == request.RoleName).FirstNonDefault();
            List<AppMenu> appMenuResult = new List<AppMenu>();
            if(currRole != null)
            {
                var appMenuList = db.Select<AppMenu>( w => w.AppRoleId == currRole.Id && w.AppMenuId == 1).ToList();
                appMenuResult = GetCurrAppMenu(db, currRole.Id, appMenuList);
            }
            return appMenuResult;
        }

        private List<AppMenu> GetCurrAppMenu(System.Data.IDbConnection db, int? roleId, List<AppMenu> subAppMenuList)
        {
            List<AppMenu> appMenuResult = new List<AppMenu>();
            foreach(var subMenu in subAppMenuList)
            {
                var appMenuList = db.Select<AppMenu>( w => w.AppRoleId == subMenu.AppRoleId && w.AppMenuId == subMenu.Id).ToList();
                if(appMenuList.Count > 0) {
                    subMenu.Sub = GetCurrAppMenu(db, subMenu.AppRoleId, appMenuList);
                }
                appMenuResult.Add(subMenu);
            }
            return appMenuResult;
        }

        public DateTime? GetWorkingDay(DateTime dt, int skipWorkingDays = 0, Func<DateTime, bool> holidays=null)
        {
            if (holidays == null) holidays = (dt) => false;
            IEnumerable<DateTime> NextWorkingDay(DateTime dt)
            {
                while (true)
                {
                    var day = dt.DayOfWeek;
                    if (day != DayOfWeek.Saturday && day != DayOfWeek.Sunday 
                        && !holidays.Invoke(dt)) yield return dt;
                    dt = dt.AddDays(1);
                }
            }

            if (skipWorkingDays<0) return null;
            if (skipWorkingDays==0) return NextWorkingDay(dt).First();
            var nextXDays = NextWorkingDay(dt).Take(skipWorkingDays).ToList();
            var endDate = nextXDays.OrderByDescending(d => d).First();
            return endDate;
        }
    
    
        public object Any(UpdateProjectPlanInBatch request) {
            using var db = AutoQuery.GetDb<ProjectPlan>(Request);

            var holidays = new List<DateTime>() {new(2020,1,1), 
                   new(2020,12,24), new(2020,12,25), new(2020,12,26)};
            // a function checking if the date is a public holiday
            Func<DateTime, bool> isHoliday = (dt) => holidays.Any(a=>a==dt);
            
            // the start date
            // var dt = new DateTime(2020, 07, 13);
            // end date, 5 working days later
            // var endDate = GetWorkingDay(dt, 5, isHoliday);

            foreach(var projectPlanToUpdate in request.ProjectPlanList)
            {
                if(projectPlanToUpdate.StartDate != null)
                {
                    var durationDays = (int) projectPlanToUpdate.DurationDays;
                    var startDate = (DateTime) projectPlanToUpdate.StartDate;
                    if(durationDays > 0)
                    {
                        var endDate = GetWorkingDay(startDate, durationDays, isHoliday);
                        projectPlanToUpdate.EndDate = endDate;
                    }
                }

            }

            return "Success";
        }
        
    
    }
}
