using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nizzle.Messages;
using Nizzle.Server.Routes;
using Nizzle.Tests.Fixtures;
using NUnit.Framework;
using Should.Fluent;

namespace Nizzle.Tests
{
    [TestFixture]
    public class HelloServiceTests : ServiceTestsBase
    {
        [Test]
        public void CanSayHelloWithStandardFormatter()
        {
            Container.RegisterAs<StandardHelloFormatter, IFormatHellos>();

            var response = Get<HelloResponse>(new HelloRequest { Name = "Nizzle" });

            response.Text.Should().Equal("Hello Nizzle");
        }

        [Test]
        public void CanSayHelloWithSunshineFormatter()
        {
            Container.RegisterAs<SunshineHelloFormatter, IFormatHellos>();

            var response = Get<HelloResponse>(new HelloRequest { Name = "Nizzle" });

            response.Text.Should().Equal("☀  Hello Nizzle  ☀");
        }
    }
}
