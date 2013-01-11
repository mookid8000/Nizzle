using System;
using System.Configuration;
using System.Reflection;
using EasyHttp.Http;
using Nizzle.Messages;
using log4net;

namespace Nizzle.Agent
{
    class Program
    {
        static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        static int Main()
        {
            var serverUri = ConfigurationManager.AppSettings["serverUri"];

            if (string.IsNullOrWhiteSpace(serverUri))
            {
                Log.Error("Please configure the server URI in the appSettings section of the Agent's app.config under the serverUri key");
                return 1;
            }

            var client = new HttpClient(serverUri) {Request = {Accept = HttpContentTypes.ApplicationJson}};
            var response = client.Get("hello", new HelloRequest
                                                   {
                                                       Name = "Mogens"
                                                   }).StaticBody<HelloResponse>();
            Console.WriteLine("Got response: {0}", response.Text);

            return 0;
        }
    }
}
