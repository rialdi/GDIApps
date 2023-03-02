using System;
using ServiceStack;
using ServiceStack.OrmLite;
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
    }
}
