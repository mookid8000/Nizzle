using ServiceStack.ServiceInterface;

namespace Nizzle.Server.Routes
{
    public class HelloRequest
    {
    }

    public class HelloResponse
    {
        public string Text { get; set; }   
    }

    public class HelloService : Service
    {
        public HelloResponse Any(HelloRequest request)
        {
            return new HelloResponse {Text = "wootamafook!!"};
        }
    }
}