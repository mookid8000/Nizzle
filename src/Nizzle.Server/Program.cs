using System;
using System.Configuration;
using System.IO;
using System.Reflection;
using Topshelf;
using log4net;
using log4net.Config;

namespace Nizzle.Server
{
    class Program
    {
        static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        AppHost host;

        static void Main()
        {
            XmlConfigurator.ConfigureAndWatch(new FileInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log4net.config")));

            HostFactory
                .Run(c =>
                         {
                             c.UseLog4Net();
                             c.RunAsNetworkService();
                             c.SetDescription("Nizzle Server - this is the hub");
                             c.SetServiceName("Nizzle.Server");
                             c.Service<Program>(s =>
                                                    {
                                                        s.WhenStarted(p => p.Start());
                                                        s.WhenStopped(p => p.Stop());
                                                        s.ConstructUsing(() => new Program());
                                                    });
                         });
        }

        void Start()
        {
            var listenUriString = GetListenUriString();

            Log.InfoFormat("Starting host on {0}", listenUriString);

            host = new AppHost();
            host.Init();
            host.Start(listenUriString);

            Log.Info("Started!");
        }

        static string GetListenUriString()
        {
            var uriString = ConfigurationManager.AppSettings["listenUri"] ?? "http://*:4040";
            
            return uriString.EndsWith("/") ? uriString : uriString + "/";
        }

        void Stop()
        {
            Log.Info("Stopping");

            host.Stop();
            host.Dispose();

            Log.Info("Stopped!");
        }
    }
}
