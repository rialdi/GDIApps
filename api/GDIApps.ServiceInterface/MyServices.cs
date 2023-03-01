using System;
using ServiceStack;
using ServiceStack.OrmLite;
using ServiceStack.IO;
using GDIApps.ServiceModel;
using GDIApps.ServiceModel.Types;
using ServiceStack.VirtualPath;

namespace GDIApps.ServiceInterface
{
    // public class IAppHost
    // {
    //     // Read/Write Virtual FileSystem. Defaults to Local FileSystem.
    //     IVirtualFiles VirtualFiles { get; set; }
        
    //     // Cascading file sources, inc. Embedded Resources, File System, In Memory, S3.
    //     IVirtualPathProvider VirtualFileSources { get; set; }
    // }

    public  class MyServices : Service
    {
        // public IVirtualPathProvider VirtualPathProvider;
        // public abstract IVirtualFiles vfs;
        // public  IVirtualPathProvider GetPathProvider();
        public IAutoQueryDb AutoQuery { get; set; }
        // public IVirtualFiles VirtualFiles { get; set; }
        // public IVirtualPathProvider VirtualFileSources { get; }
        public object Any(Hello request)
        {
            return new HelloResponse { Result = $"Hello, {request.Name}!" };
        }

        // public void Any(CreateInvoiceAttachment request)
        // {
        //     using var db = AutoQuery.GetDb<InvoiceAttachment>(Request);
        //     var newInvAtt = new InvoiceAttachment(){
        //         InvoiceId = request.InvoiceId,
        //         FileName = request.FileName,
        //         AttachmentUrl = request.AttachmentUrl
        //     };

        //     db.Insert<InvoiceAttachment>(newInvAtt);

        //     // var currInvoiceAttachment = db.Select<InvoiceAttachment>( w => w.Id == request.Id).FirstNonDefault();

        //     // if(currInvoiceAttachment != null) {
        //     //     var allFiles = VirtualFiles.GetAllFiles();
        //     //     var attachmentUrl = currInvoiceAttachment.AttachmentUrl.Replace("uploads","App_Data");
        //     //     // var file = VirtualFiles.GetFile(attachmentUrl);
        //     //     VirtualFiles.DeleteFile(attachmentUrl);
        //     // }

        //     // db.Delete(currInvoiceAttachment);
        // }

        public void Any(DeleteInvoiceAttachment request)
        {
            using var db = AutoQuery.GetDb<InvoiceAttachment>(Request);

            var currInvoiceAttachment = db.Select<InvoiceAttachment>( w => w.Id == request.Id).FirstNonDefault();

            if(currInvoiceAttachment != null) {
                var allFiles = VirtualFiles.GetAllFiles();
                var attachmentUrl = currInvoiceAttachment.AttachmentUrl.Replace("uploads","App_Data");
                // var file = VirtualFiles.GetFile(attachmentUrl);
                VirtualFiles.DeleteFile(attachmentUrl);
            }

            db.Delete(currInvoiceAttachment);
        }
    }

    
}
