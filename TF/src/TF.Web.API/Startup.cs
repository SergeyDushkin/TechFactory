using Microsoft.Owin.Hosting;
using Microsoft.Practices.Unity;
using NLog;
using Owin;
using System;
using System.Web.Http;
using System.Web.Http.OData.Builder;
using System.Web.Http.OData.Extensions;
using TF.DAL;
using TF.Data.Business;
using TF.Data.Business.WMS;
using TF.Data.Systems;
using TF.Data.Systems.Security;

namespace TF.Web.API
{
    internal class Startup
    {
        private IDisposable application;

        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();

            /// Регистрация зависимостей
            Startup.RegisterProductDependency(config);

            /// Регистрация маршрутов odata
            Startup.RegisterOdataRoutes(config);

            ///// Подключаем модуль web api
            app.UseWebApi(config);
        }

        private static void RegisterProductDependency(HttpConfiguration config)
        {
            var container = new UnityContainer();
            var dbContext = new NoodleDbContext("NoodleDb");

            container.RegisterType<IUnitRepository, UnitRepository>(new InjectionConstructor(dbContext));
            container.RegisterType<ICategoryService, CategoryTreeService>(new InjectionConstructor(dbContext));
            container.RegisterType<IProductRepository, ProductRepository>(new InjectionConstructor(dbContext));
            container.RegisterType<IProductCategoryService, ProductCategoryService>(new InjectionConstructor(dbContext));
            container.RegisterType<IProductPriceService, ProductPriceService>(new InjectionConstructor(dbContext));
            container.RegisterType<IUomRepository, UomRepository>(new InjectionConstructor(dbContext));
            container.RegisterType<ILocationRepository, LocationRepository>(new InjectionConstructor(dbContext));
            
            container.RegisterType<ILogger, Logger>(new InjectionFactory(x => LogManager.GetCurrentClassLogger()));

            config.DependencyResolver = new UnityResolver(container);
        }

        private static void RegisterOdataRoutes(HttpConfiguration config)
        {
            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();

            builder.EntitySet<Unit>("Units");
            builder.EntitySet<Product>("Products");
            builder.EntitySet<Category>("Categories");
            builder.EntitySet<ProductPrice>("ProductPrices");
            builder.EntitySet<Currency>("Currencies");
            builder.EntitySet<Entity>("Entities");
            builder.EntitySet<Location>("Locations");
            builder.EntitySet<Order>("Orders");
            builder.EntitySet<Person>("Persons");
            builder.EntitySet<Role>("Roles");
            builder.EntitySet<Uom>("Uoms");

            config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
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