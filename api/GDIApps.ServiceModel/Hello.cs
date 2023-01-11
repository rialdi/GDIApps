using ServiceStack;

namespace GDIApps.ServiceModel
{
    [Route("/hello")]
    [Route("/hello/{Name}")]
    public class Hello : IReturn<HelloResponse>
    {
        public string Name { get; set; } = string.Empty;
    }

    public class HelloResponse
    {
        public string Result { get; set; } = string.Empty;
    }
}
