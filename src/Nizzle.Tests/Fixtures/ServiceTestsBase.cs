using System;
using System.Reflection;
using EasyHttp.Http;
using Funq;
using NUnit.Framework;
using Nizzle.Server.Routes;
using ServiceStack.Common.Web;
using ServiceStack.ServiceClient.Web;
using ServiceStack.ServiceHost;
using ServiceStack.Text;
using ServiceStack.WebHost.Endpoints;

namespace Nizzle.Tests.Fixtures
{
    public class TestServiceAppHost : AppHostHttpListenerBase
    {
        public TestServiceAppHost()
            : base("Test Service", typeof(HelloService).Assembly)
        {
        }

        public override void Configure(Container container)
        {
            SetConfig(new EndpointHostConfig
            {
                DefaultContentType = ContentType.Json
            });
        }
    }

    [TestFixture]
    public class ServiceTestsBase
    {
        protected const string HttpLocalhost = "http://localhost:7357/";
        private TestServiceAppHost _appHost;
        private HttpClient _serviceClient;

        protected HttpClient ServiceClient
        {
            get { return _serviceClient; }
        }

        protected TResponse Get<TResponse>(object request)
        {
            return ServiceClient.Get(GetUrl(request), request).StaticBody<TResponse>("application/json");
        }

        private string GetUrl(object request)
        {
            return string.Format("/{0}/syncreply/{1}".Fmt("json", request.GetType().Name));
        }

        [TestFixtureSetUp]
        public void SetupFixture()
        {
            _appHost = new TestServiceAppHost();
            _appHost.Init();
            _appHost.Start(HttpLocalhost);
            Container = _appHost.Container;
        }

        [SetUp]
        public void SetUp()
        {
            _serviceClient = new HttpClient(HttpLocalhost) { Request = { Accept = HttpContentTypes.ApplicationJson } };
        }

        protected Container Container { get; set; }

        [TearDown]
        public void TearDown()
        {
            Container.Dispose();
        }

        [TestFixtureTearDown]
        public void TearDownFixture()
        {
            _appHost.Dispose();
        }
    }
}
