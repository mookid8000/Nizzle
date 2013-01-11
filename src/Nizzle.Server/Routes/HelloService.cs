using Nizzle.Messages;
using ServiceStack.ServiceInterface;

namespace Nizzle.Server.Routes
{
    public class HelloService : Service
    {
        public HelloResponse Get(HelloRequest request)
        {
            return new HelloResponse {Text = string.Format("Hello {0}", request.Name)};
        }
    }
}