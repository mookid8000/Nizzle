using System.Reflection;
using Funq;
using Nizzle.Messages;
using Nizzle.Server.Routes;
using ServiceStack.Common.Web;
using ServiceStack.ServiceHost;
using ServiceStack.WebHost.Endpoints;
using log4net;

namespace Nizzle.Server
{
    public class AppHost : AppHostHttpListenerBase
    {
        public AppHost() : base("Nizzle Server", typeof(HelloService).Assembly)
        {
            SetConfig(new EndpointHostConfig
                          {
                              DefaultContentType = ContentType.Json
                          });
        }

        public override void Configure(Container container)
        {
            //RequestBinders.Add(typeof(UploadedFile), DoYourThing);

            Routes.Add<HelloRequest>("/hello");
            Routes.Add<EmptyRequest>("/overview");
            Routes.Add<UploadedFile>("/agent/{agentId}/upload", "POST");


            container.RegisterAs<StandardHelloFormatter, IFormatHellos>();
        }

        static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        object DoYourThing(IHttpRequest httpReq)
        {
            Log.InfoFormat("URI: {0}", httpReq.AbsoluteUri);

            int a = 2;

            return new UploadedFile();
        }
    }
}