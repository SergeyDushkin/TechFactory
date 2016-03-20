using Topshelf;

namespace TF.Web.API
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
                        System.Diagnostics.Process.Start(uri + "odata/");
                    });
                    s.WhenStopped(svc => svc.Stop());
                });

                x.RunAsLocalSystem();

                x.SetDescription("TechFactory Noodle WebAPI selfhosting Windows Service");
                x.SetDisplayName("TechFactory Noodle WebAPI");
                x.SetServiceName("TechFactory.Noodle.WebAPI");
            });

        }
    }
}
