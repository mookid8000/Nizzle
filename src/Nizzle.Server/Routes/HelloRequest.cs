using ServiceStack.ServiceInterface;

namespace Nizzle.Server.Routes
{
    public class HelloRequest
    {
        public string Name { get; set; }
    }

    public class HelloResponse
    {
        public string Text { get; set; }   
    }

    public class HelloService : Service
    {
        public HelloResponse Any(HelloRequest request)
        {
            return new HelloResponse {Text = string.Format("Hello {0}", request.Name)};
        }
    }
}