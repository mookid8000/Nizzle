using System.IO;
using System.Net;
using System.Reflection;
using ServiceStack.Common.Web;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;
using log4net;

namespace Nizzle.Server.Routes
{
    public class AgentService : Service
    {
        static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public object Post(UploadedFile request)
        {
            Log.InfoFormat("Will send file to agent {0}", request.AgentId);

            var tempFileName = @"c:\temp\testfile.bin";
            
            using (var fileStream = File.OpenWrite(tempFileName))
            {
                request.RequestStream.Copy(fileStream);
            }

            return new HttpResult(HttpStatusCode.OK);
        }

        public void Get(UploadedFile request)
        {
            Log.InfoFormat("Will send file to agent {0}", request.AgentId);
        }
    }

    public static class StreamExtender
    {
        public static void Copy(this Stream instance, Stream target)
        {
            int bytesRead;
            var copyBuf = new byte[8192];

            while ((bytesRead = instance.Read(copyBuf, 0, copyBuf.Length)) > 0)
            {
                target.Write(copyBuf, 0, bytesRead);
            }
        }
    }

    public class UploadedFile : IRequiresRequestStream
    {
        public int AgentId { get; set; }
        public Stream RequestStream { get; set; }
    }
}