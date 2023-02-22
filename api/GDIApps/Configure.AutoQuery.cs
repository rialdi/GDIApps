using ServiceStack;
using ServiceStack.Data;
using GDIApps.ServiceModel.Types;
using ServiceStack.Text;
using ServiceStack.IO;
using ServiceStack.Web;

[assembly: HostingStartup(typeof(GDIApps.ConfigureAutoQuery))]

namespace GDIApps;

public class ConfigureAutoQuery : IHostingStartup
{
    public void Configure(IWebHostBuilder builder) => builder
        .ConfigureServices(services => {
            // Enable Audit History
            services.AddSingleton<ICrudEvents>(c =>
                new OrmLiteCrudEvents(c.Resolve<IDbConnectionFactory>()));
        })
        .ConfigureAppHost(appHost => {

            var country = File.ReadAllText("App_Data/csv/62/countries.csv").FromCsv<List<Country>>();
            var provinces = File.ReadAllText("App_Data/csv/62/provinces.csv").FromCsv<List<Province>>();
            var cities = File.ReadAllText("App_Data/csv/62/cities.csv").FromCsv<List<City>>();
            var districts = File.ReadAllText("App_Data/csv/62/subDistricts.csv").FromCsv<List<District>>();
            var villages = File.ReadAllText("App_Data/csv/62/villages.csv").FromCsv<List<Village>>();
            

            var autoQueryDateFeature = new AutoQueryDataFeature { IncludeTotal = true };

            // For TodosService
            appHost.Plugins.Add(autoQueryDateFeature
                .AddDataSource(ctx => ctx.MemorySource(country))
                .AddDataSource(ctx => ctx.MemorySource(provinces))
                .AddDataSource(ctx => ctx.MemorySource(cities))
                .AddDataSource(ctx => ctx.MemorySource(districts))
                .AddDataSource(ctx => ctx.MemorySource(villages))
            );

            // appHost.Plugins.Add(new CsvFormat()); //added by default

            // For Bookings https://github.com/NetCoreApps/BookingsCrud
            appHost.Plugins.Add(new AutoQueryFeature {
                MaxLimit = 1000,
                IncludeTotal = true
            });

            appHost.Resolve<ICrudEvents>().InitSchema();

            // List<Country> country = "~/App_Data/csv/62/countries.csv".MapHostAbsolutePath()
            //     .ReadAllText().FromCsv<List<Country>>();

            // // 

            // appHost.Plugins.Add(new AutoQueryDataFeature()
            //     .AddDataSource(ctx => ctx.MemorySource(country)));
        });
}

public class CsvFormat : IPlugin
    {
        public void Register(IAppHost appHost)
        {
            appHost.ContentTypes.Register(MimeTypes.Csv,
                SerializeToStream, 
                CsvSerializer.DeserializeFromStream);

            //ResponseFilter to add 'Content-Disposition' header for browsers to open in Spreadsheet
            appHost.GlobalResponseFilters.Add((req, res, dto) => {
                if (req.ResponseContentType == MimeTypes.Csv) {
                    var fileName = req.OperationName + ".csv";
                    res.AddHeader(HttpHeaders.ContentDisposition, 
                        $"attachment;{HttpExt.GetDispositionFileName(fileName)}");
                }
            });
        }

        void SerializeToStream(IRequest req, object request, Stream stream) =>
            CsvSerializer.SerializeToStream(request, stream);
    }
