using Microsoft.Owin;
using Microsoft.Practices.Unity;
using NLog;
using Owin;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.OData.Builder;
using System.Web.OData.Extensions;
using System.Web.OData.Routing;
using System.Web.OData.Routing.Conventions;
using TF.DAL;
using TF.Data.Business;
using TF.Data.Business.WMS;
using TF.Data.Systems;
using TF.Data.Systems.Security;

[assembly: OwinStartup(typeof(TF.Web.OData.Startup))]
namespace TF.Web.OData
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();

            /// Регистрация зависимостей
            Startup.RegisterProductDependency(config);

            /// Регистрация маршрутов odata
            Startup.RegisterOdataRoutes(config);

            config.EnableCors(new EnableCorsAttribute("*", "*", "*"));

            /// Регистрируем инсталлятор базы данных
            app.Map("/install", Installer.UseInstaller);

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
            container.RegisterType<ILinkRepository, LinkRepository>(new InjectionConstructor(dbContext));
            container.RegisterType<IProductSpecificationRepository, ProductSpecificationRepository>(new InjectionConstructor(dbContext));

            container.RegisterType<ILogger, Logger>(new InjectionFactory(x => LogManager.GetCurrentClassLogger()));

            config.DependencyResolver = new UnityResolver(container);
        }

        private static void RegisterOdataRoutes(HttpConfiguration config)
        {
            ODataModelBuilder builder = new ODataConventionModelBuilder();

            builder.EntitySet<Unit>("Units");
            builder.EntitySet<Product>("Products");
            builder.EntitySet<Category>("Categories");
            builder.EntitySet<ProductPrice>("ProductPrices");
            builder.EntitySet<ProductCategory>("ProductCategories");
            builder.EntitySet<Currency>("Currencies");
            builder.EntitySet<Entity>("Entities");
            builder.EntitySet<Location>("Locations");
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
            builder.EntitySet<Link>("Links");
            builder.EntitySet<ProductSpecification>("ProductSpecifications");

            builder.Namespace = "NoodleService";
            builder.EntityType<Order>()
                .Action("Confirm");

            var model = builder.GetEdmModel();

            var conventions = ODataRoutingConventions.CreateDefault();
            conventions.Insert(0, new NavigationIndexRoutingConvention(model));

            config.MapODataServiceRoute(routeName: "ODataRoute",
                 routePrefix: "odata",
                 model: model,
                 pathHandler: new DefaultODataPathHandler(),
                 routingConventions: conventions);

            config.AddODataQueryFilter();
        }
    }
}