using Microsoft.Owin.Hosting;
using Microsoft.Practices.Unity;
using NLog;
using Owin;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.OData.Builder;
using System.Web.Http.OData.Extensions;
using System.Web.Http.Tracing;
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

            config.EnableCors(new EnableCorsAttribute("*", "*", "*"));

            /// Trace
            /// config.Services.Replace(typeof(ITraceWriter), new CustomTracer());

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
            container.RegisterType<ICurrencyRepository, CurrencyRepository>(new InjectionConstructor(dbContext));
            container.RegisterType<IAddressRepository, AddressRepository>(new InjectionConstructor(dbContext));
            container.RegisterType<IEmployeeRepository, EmployeeRepository>(new InjectionConstructor(dbContext));
            container.RegisterType<IOrderRepository, OrderRepository>(new InjectionConstructor(dbContext));
            container.RegisterType<IOrderLineRepository, OrderLineRepository>(new InjectionConstructor(dbContext));
            container.RegisterType<IOrderLineDetailRepository, OrderLineDetailRepository>(new InjectionConstructor(dbContext));
            container.RegisterType<IUserRepository, UserRepository>(new InjectionConstructor(dbContext));
            container.RegisterType<IContactRepository, ContactRepository>(new InjectionConstructor(dbContext));
            container.RegisterType<IContactDetailRepository, ContactDetailRepository>(new InjectionConstructor(dbContext));
            container.RegisterType<IPersonRepository, PersonRepository>(new InjectionConstructor(dbContext));

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
            builder.EntitySet<ProductCategory>("ProductCategories");
            builder.EntitySet<Currency>("Currencies");
            builder.EntitySet<Entity>("Entities");
            builder.EntitySet<Location>("Locations");
            builder.EntitySet<Order>("Orders");
            builder.EntitySet<Person>("Persons");
            builder.EntitySet<Role>("Roles");
            builder.EntitySet<Uom>("Uoms");
            builder.EntitySet<Address>("Addresses");
            builder.EntitySet<Employee>("Employees");
            builder.EntitySet<Order>("Orders");
            builder.EntitySet<OrderLine>("OrderLines");
            builder.EntitySet<OrderLineDetail>("OrderLineDetails");
            builder.EntitySet<User>("Users");
            builder.EntitySet<Contact>("Contacts");

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

    //public class CustomTracer : ITraceWriter
    //{
    //    private readonly Logger logger;

    //    private static readonly Lazy<Dictionary<TraceLevel, Action<string>>> loggingMap =
    //        new Lazy<Dictionary<TraceLevel, Action<string>>>(() => new Dictionary<TraceLevel, Action<string>>
    //            {
    //                {TraceLevel.Info, LogManager.GetCurrentClassLogger().Info },
    //                {TraceLevel.Debug, LogManager.GetCurrentClassLogger().Debug},
    //                {TraceLevel.Error, LogManager.GetCurrentClassLogger().Error},
    //                {TraceLevel.Fatal, LogManager.GetCurrentClassLogger().Fatal},
    //                {TraceLevel.Warn, LogManager.GetCurrentClassLogger().Warn}
    //            });


    //    public CustomTracer()
    //    {
    //        logger = LogManager.GetCurrentClassLogger();
    //    }

    //    public void Trace(HttpRequestMessage request, string category, TraceLevel level,
    //        Action<TraceRecord> traceAction)
    //    {
    //        TraceRecord rec = new TraceRecord(request, category, level);

    //        traceAction(rec);
    //        WriteTrace(rec);
    //    }

    //    protected void WriteTrace(TraceRecord rec)
    //    {
    //        var message = string.Format("{0};{1};{2}",
    //            rec.Operator, rec.Operation, rec.Message);
    //        System.Diagnostics.Trace.WriteLine(message, rec.Category);

    //        var log = new LogEventInfo(LogLevel.Info, "", message);

    //        log.Properties["CorrelationId"] = rec.Request.GetCorrelationId();

    //        if (rec.Level == TraceLevel.Info)
    //            logger.Info(message);
    //    }
    //}

    //public interface IEventWriter
    //{
    //    string CorrelationId { get; set; }

    //    void Log();
    //    void Trace();
    //    void Event();
    //    void Exception();
    //    void Profile();
    //}

}