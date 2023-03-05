using GDIApps.ServiceModel.Types;
using GDIApps.ServiceModel.Types.Tables;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System.Reflection.Metadata;

[assembly: HostingStartup(typeof(GDIApps.ConfigureDb))]

namespace GDIApps;

// Database can be created with "dotnet run --AppTasks=migrate"   
public class ConfigureDb : IHostingStartup
{
    // public void Configure(IWebHostBuilder builder) => builder
    //     .ConfigureServices((context,services) => services.AddSingleton<IDbConnectionFactory>(new OrmLiteConnectionFactory(
    //         context.Configuration.GetConnectionString("MainConn") 
    //         ?? "Server=68.183.230.123;Database=GDIAPPSDB;User Id=SA;Password=Formula_01;MultipleActiveResultSets=True;",
    //         SqlServer2012Dialect.Provider
    //     )));

     public void Configure(IWebHostBuilder builder) => builder
        .ConfigureServices((context,services) => services.AddSingleton<IDbConnectionFactory>(new OrmLiteConnectionFactory(
            context.Configuration.GetConnectionString("DefaultConnection") ?? "App_Data/db.sqlite",
            SqliteDialect.Provider)))
         .ConfigureAppHost(afterConfigure: appHost => {
             appHost.ScriptContext.ScriptMethods.Add(new DbScriptsAsync());

             // Create non-existing Table and add Seed Data Example
             using var db = appHost.Resolve<IDbConnectionFactory>().Open();
             db.CreateTableIfNotExists<Overtime>();
             // var isExistTable = false;

             // db.DropAndCreateTable<AppUser>();                

             //if (db.CreateTableIfNotExists<Lookup>())
             //{
             //    db.Insert(new Lookup
             //    {
             //        LookupType = LOOKUPTYPE.WORKGROUP,
             //        LookupValue = "NEW",
             //        LookupText = "NEW",
             //        IsActive = true,
             //        CreatedBy = "ADMIN",
             //        ModifiedBy = "ADMIN",
             //        CreatedDate = DateTime.Now,
             //        ModifiedDate = DateTime.Now
             //    });
             //}
             //db.CreateTableIfNotExists<Equipment>();
             //db.CreateTableIfNotExists<MaintenanceReq>();

             //db.CreateTableIfNotExists<WorkExecution>();
             //db.CreateTableIfNotExists<WEActivity>();
             //db.CreateTableIfNotExists<WEWorkOrder>();

             //db.CreateTableIfNotExists<RoleWorkgroup>();
             //db.CreateTableIfNotExists<GENERALPARAMETER>();

         });
}
