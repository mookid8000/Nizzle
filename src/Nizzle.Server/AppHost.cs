﻿using Funq;
using Nizzle.Server.Routes;
using ServiceStack.Common.Web;
using ServiceStack.WebHost.Endpoints;

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
            Routes
                .Add<HelloRequest>("/hello");
        }
    }
}