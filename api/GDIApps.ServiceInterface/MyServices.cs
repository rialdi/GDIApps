using System;
using ServiceStack;
using GDIApps.ServiceModel;

namespace GDIApps.ServiceInterface
{
    public class MyServices : Service
    {
        public object Any(Hello request)
        {
            return new HelloResponse { Result = $"Hello, {request.Name}!" };
        }
    }
}
