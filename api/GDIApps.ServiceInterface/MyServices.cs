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
        public object Any(Hello request) {
            return new HelloResponse { Result = $"Hello, {request.Name}!" };
        }
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
    }
}
