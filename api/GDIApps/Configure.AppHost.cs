using Funq;
using ServiceStack;
// using ServiceStack.Admin;
using ServiceStack.Configuration;
using ServiceStack.Script;
using ServiceStack.Text;
using GDIApps.ServiceInterface;
using GDIApps.ServiceModel.Types;
using GDIApps.ServiceModel;
using ServiceStack.IO;
using ServiceStack.Web;

[assembly: HostingStartup(typeof(GDIApps.AppHost))]

namespace GDIApps;

public class AppHost : AppHostBase, IHostingStartup
{
    public void Configure(IWebHostBuilder builder) => builder
        .ConfigureServices((context, services) => {
            services.ConfigureNonBreakingSameSiteCookies(context.HostingEnvironment);
        });

    public AppHost() : base("GDIApps", typeof(MyServices).Assembly) {}

    public override void Configure(Container container)
    {
        SetConfig(new HostConfig {
        });
        
        Plugins.Add(new AdminDatabaseFeature());

        Plugins.Add(new SpaFeature {
            EnableSpaFallback = true
        });
        Plugins.Add(new CorsFeature(allowOriginWhitelist:new[]{ 
            "http://localhost:5000",
            "http://localhost:3000",
            "https://localhost:5005",
            "https://apps.ptgdi.com",
            "http://apps.ptgdi.com",
            "https://" + Environment.GetEnvironmentVariable("DEPLOY_CDN")
        }, allowCredentials:true));

        ConfigurePlugin<UiFeature>(feature => {
            feature.Info.BrandIcon.Uri = "/assets/img/logo.svg";
            feature.Info.BrandIcon.Cls = "inline-block w-8 h-8 mr-2";
        });

        container.Register<IEmailer>(c => new SmtpEmailer());
        var appSettings = HostContext.AppSettings;
        
        container.Register(appSettings.Get("SmtpConfig",
            new SmtpConfig {
                Host = "smtphost",
                Port = 587,
                UserName = "ADD_USERNAME",
                Password = "ADD_PASSWORD"
            })
        );

        container.RegisterAs<SmtpEmailer, IEmailer>().ReusedWithin(ReuseScope.Request);
        
        var wwwrootVfs = GetVirtualFileSource<FileSystemVirtualFiles>();

        var appFs = new FileSystemVirtualFiles(
            ContentRootDirectory.RealPath.CombineWith("App_Data").AssertDir()
        );

        Plugins.Add(new FilesUploadFeature(
            // User profiles
            new UploadLocation("invoiceAttachments", appFs,
                resolvePath: ctx => ctx.GetLocationPath(( ctx.Dto is CreateInvoiceAttachment create
                    ? $"[{create.InvoiceId}]-" 
                    : $"app/{ctx.Dto.GetId()}/") + $"{ctx.FileName}")),

            new UploadLocation("projectDocs", appFs,
                resolvePath: ctx => ctx.GetLocationPath(( ctx.Dto is CreateProjectDoc create
                    ? $"[{create.ProjectId}]-" 
                    : $"app/{ctx.Dto.GetId()}/") + $"{ctx.FileName}")),
                    
            new UploadLocation("userprofile", wwwrootVfs, allowExtensions: FileExt.WebImages,
                resolvePath: ctx => $"/assets/media/users/{ctx.UserAuthId}/profile/{ctx.FileName}"),
            // User Cover Photo    
            new UploadLocation("usercover", wwwrootVfs, allowExtensions: FileExt.WebImages,
                resolvePath: ctx => $"/assets/media/users/{ctx.UserAuthId}/cover/{ctx.FileName}"),
            new UploadLocation("tempUploadFiles", appFs, allowExtensions: FileExt.WebImages,
                resolvePath: ctx => $"/tempUploadFiles/{ctx.FileName}")
        ));

        // static string ResolveUploadPath(FilesUploadContext ctx) {
        //     var path = string.Empty;
        //     if(ctx.Dto is CreateInvoiceAttachment create)
        //     {
        //         path = $"[{create.InvoiceId}]-" + $"{ctx.FileName}";
        //     } else 
        //     {
        //         path = $"app/{ctx.Dto.GetId()}/" + $"{ctx.FileName}";
        //     }
        //     return path;
        // };
    }
}
