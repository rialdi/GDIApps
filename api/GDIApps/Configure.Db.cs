using ServiceStack.Data;
using ServiceStack.OrmLite;

[assembly: HostingStartup(typeof(GDIApps.ConfigureDb))]

namespace GDIApps;

// Database can be created with "dotnet run --AppTasks=migrate"   
public class ConfigureDb : IHostingStartup
{
    public void Configure(IWebHostBuilder builder) => builder
        .ConfigureServices((context,services) => services.AddSingleton<IDbConnectionFactory>(new OrmLiteConnectionFactory(
            context.Configuration.GetConnectionString("MainConn") 
            ?? "Server=68.183.230.123;Database=GDIAPPSDB;User Id=SA;Password=Formula_01;MultipleActiveResultSets=True;",
            SqlServer2012Dialect.Provider
        )));
}
