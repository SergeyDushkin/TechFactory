using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace TF.Web.Proxy
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = System.Configuration.ConfigurationManager.AppSettings["host"];
            var port = System.Configuration.ConfigurationManager.AppSettings["port"];

            var uri = "http://" + host + ":" + port + "/";

            HostFactory.Run(x =>
            {
                x.Service<Startup>(s =>
                {
                    s.ConstructUsing(name => new Startup());
                    s.WhenStarted(svc =>
                    {
                        svc.Start(uri);
                    });
                    s.WhenStopped(svc => svc.Stop());
                });

                x.RunAsLocalSystem();

                x.SetDescription("TechFactory Proxy Application selfhosting Windows Service");
                x.SetDisplayName("TechFactory Proxy Application");
                x.SetServiceName("TechFactory.Proxy.Application");
            });

        }
    }
}
