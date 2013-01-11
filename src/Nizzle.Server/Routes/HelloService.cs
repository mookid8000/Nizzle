using Nizzle.Messages;
using ServiceStack.ServiceInterface;

namespace Nizzle.Server.Routes
{
    public class HelloService : Service
    {
        private readonly IFormatHellos _helloFormatter;

        public HelloService(IFormatHellos helloFormatter)
        {
            _helloFormatter = helloFormatter;
        }

        public HelloResponse Get(HelloRequest request)
        {
            return new HelloResponse { Text = _helloFormatter.FormatIt("Hello {0}", request.Name) };
        }
    }

    public interface IFormatHellos
    {
         string FormatIt(string format, params object[] input);
    }

    public class StandardHelloFormatter : IFormatHellos
    {
        public string FormatIt(string format, params object[] input)
        {
             return string.Format(format, input);
        }
    }

    public class SunshineHelloFormatter : IFormatHellos
    {
        public string FormatIt(string format, params object[] input)
        {
            return string.Format(string.Format("☀  {0}  ☀", format), input);
        }
    }
}