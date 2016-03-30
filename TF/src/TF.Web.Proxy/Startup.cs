using Microsoft.Owin.Hosting;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Practices.Unity;
using NLog;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Http;

namespace TF.Web.Proxy
{
    internal class Startup
    {
        private IDisposable application;

        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();

            /// Регистрация зависимостей
            Startup.RegisterProductDependency(config);

            ///// Подключаем модуль web api
            app.UseWebApi(config);

            app.UseOAuthBearerAuthentication(new Microsoft.Owin.Security.OAuth.OAuthBearerAuthenticationOptions()
            {
                Provider = new OAuthBearerAuthenticationProvider(),
                
            });
        }

        private static void RegisterProductDependency(HttpConfiguration config)
        {
            var container = new UnityContainer();

            container.RegisterType<ILogger, Logger>(new InjectionFactory(x => LogManager.GetCurrentClassLogger()));

            config.DependencyResolver = new UnityResolver(container);
        }

        public void Start(string baseAddress)
        {
            application = WebApp.Start<Startup>(url: baseAddress);
        }

        public void Stop()
        {
            application.Dispose();
        }
    }
}
