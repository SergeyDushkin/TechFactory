using Microsoft.Owin.Hosting;
using Microsoft.Practices.Unity;
using NLog;
using Owin;
using System;
using System.Web.Http;

/*
using System.Web.Http.OData.Builder;
using System.Web.Http.OData.Routing;
using System.Web.Http.OData.Routing.Conventions;
using System.Web.Http.OData.Extensions;
*/
/**/
using System.Web.OData.Builder;
using System.Web.OData.Routing;
using System.Web.OData.Extensions;
using System.Web.OData.Routing.Conventions;

using TF.DAL;
using TF.Data.Business;
using TF.Data.Business.WMS;
using TF.Data.Systems;
using TF.Data.Systems.Security;
using System.Web.Http.Controllers;
using System.Linq;
using System.Net.Http;
using Microsoft.OData.Edm;
using System.Web.Http.Cors;

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

            //builder.EntitySet<Product>("Products").HasManyBinding<Category>(t => t.Categories, "Categories");
            //config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());

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

        public void Start(string baseAddress)
        {
            application = WebApp.Start<Startup>(url: baseAddress);
        }

        public void Stop()
        {
            application.Dispose();
        }
    }

    public class NavigationIndexRoutingConvention : EntitySetRoutingConvention
    {
        private readonly IEdmModel model;

        public NavigationIndexRoutingConvention(IEdmModel model)
        {
            this.model = model;
        }

        public override string SelectAction(ODataPath odataPath, HttpControllerContext context,
            ILookup<string, HttpActionDescriptor> actionMap)
        {
            if (context.Request.Method == HttpMethod.Get &&
                odataPath.PathTemplate == "~/entityset/key/navigation/key")
            {
                string navigationEntityName = model.FindDeclaredEntitySet((odataPath.Segments[2] as NavigationPathSegment).NavigationPropertyName).EntityType().Name;

                //NavigationPathSegment navigationSegment = odataPath.Segments[2] as NavigationPathSegment;
                //var declaringType = model.FindDeclaredEntitySet(navigationSegment.NavigationPropertyName);

                //IEdmNavigationProperty navigationProperty = navigationSegment.NavigationProperty.Partner;
                //IEdmEntityType declaringType = navigationProperty.DeclaringType as IEdmEntityType;
                //string actionName = "Get" + declaringType.Name;

                string actionName = "Get" + navigationEntityName + "ByRelatedKey";
                if (actionMap.Contains(actionName))
                {
                    // Add keys to route data, so they will bind to action parameters.
                    KeyValuePathSegment keyValueSegment = odataPath.Segments[1] as KeyValuePathSegment;
                    context.RouteData.Values[ODataRouteConstants.Key] = keyValueSegment.Value;

                    KeyValuePathSegment relatedKeySegment = odataPath.Segments[3] as KeyValuePathSegment;
                    context.RouteData.Values[ODataRouteConstants.RelatedKey] = relatedKeySegment.Value;

                    return actionName;
                }
            }

            /*
            if (context.Request.Method == HttpMethod.Post &&
                odataPath.PathTemplate == "~/entityset/key/navigation")
            {
                string entityName = ((EntitySetPathSegment)(odataPath.Segments[0])).EntitySetName;
                string navigationPropertyName = (odataPath.Segments[2] as NavigationPathSegment).NavigationPropertyName;
                //string navigationEntityName = model.FindDeclaredEntitySet((odataPath.Segments[2] as NavigationPathSegment).NavigationPropertyName).Name;

                var navigationPropertyBinding =
                ((Microsoft.OData.Edm.Library.EdmNavigationPropertyBinding)
                (model.FindDeclaredEntitySet(entityName)
                    .NavigationPropertyBindings
                    .FirstOrDefault(r => r.NavigationProperty.Name == navigationPropertyName)));

                var navigationPropertyType = navigationPropertyBinding
                    .Target
                    .EntityType();

                string actionName = "Post" + navigationPropertyName + "To" + entityName;
                if (actionMap.Contains(actionName))
                {
                    KeyValuePathSegment keyValueSegment = odataPath.Segments[1] as KeyValuePathSegment;
                    context.RouteData.Values[ODataRouteConstants.Key] = keyValueSegment.Value;

                    var content = context.Request.Content.ReadAsStringAsync().Result;
                    var entity = Newtonsoft.Json.JsonConvert.DeserializeObject(content, Type.GetType(navigationPropertyType.FullTypeName()));
                    context.RouteData.Values["entity"] = entity;

                    return actionName;
                }
            }
            */
            /*
            if (context.Request.Method == HttpMethod.Put &&
                odataPath.PathTemplate == "~/entityset/key/navigation/key")
            {
                string entityName = ((EntitySetPathSegment)(odataPath.Segments[0])).EntitySetName;
                string navigationEntityName = model.FindDeclaredEntitySet((odataPath.Segments[2] as NavigationPathSegment).NavigationPropertyName).EntityType().Name;
                string navigationPropertyName = (odataPath.Segments[2] as NavigationPathSegment).NavigationPropertyName;

                //string entityName = model.FindDeclaredEntitySet(((EntitySetPathSegment)(odataPath.Segments[0])).EntitySetName).EntityType().Name;
                //NavigationPathSegment navigationSegment = odataPath.Segments[2] as NavigationPathSegment;
                //IEdmNavigationProperty navigationProperty = navigationSegment.NavigationProperty;

                //var declaringType = model.FindDeclaredEntitySet(navigationSegment.NavigationPropertyName);
                //var entityType = declaringType.EntityType();
                //IEdmEntityType declaringType = navigationProperty.DeclaringType as IEdmEntityType;
                //string actionName = "Put" + declaringType.Name;

                var navigationPropertyBinding =
                ((Microsoft.OData.Edm.Library.EdmNavigationPropertyBinding)
                (model.FindDeclaredEntitySet(entityName)
                    .NavigationPropertyBindings
                    .FirstOrDefault(r => r.NavigationProperty.Name == navigationPropertyName)));

                var navigationPropertyType = navigationPropertyBinding
                    .Target
                    .EntityType();

                string actionName = "Update" + navigationEntityName;
                //string actionName = "Put" + navigationEntityName + "To" + entityName;

                if (actionMap.Contains(actionName))
                {
                    // Add keys to route data, so they will bind to action parameters.
                    KeyValuePathSegment keyValueSegment = odataPath.Segments[1] as KeyValuePathSegment;
                    context.RouteData.Values[ODataRouteConstants.Key] = keyValueSegment.Value;

                    KeyValuePathSegment relatedKeySegment = odataPath.Segments[3] as KeyValuePathSegment;
                    context.RouteData.Values[ODataRouteConstants.RelatedKey] = relatedKeySegment.Value;

                    var content = context.Request.Content.ReadAsStringAsync().Result;
                    var entity = Newtonsoft.Json.JsonConvert.DeserializeObject(content, Type.GetType(navigationPropertyType.FullTypeName()));
                    context.RouteData.Values["entity"] = entity;

                    return actionName;
                }
            }
            */

            if (context.Request.Method == HttpMethod.Put &&
                odataPath.PathTemplate == "~/entityset/key/navigation/key")
            {
                string navigationPropertyName = (odataPath.Segments[2] as NavigationPathSegment).NavigationPropertyName;
                string actionName = "Update" + navigationPropertyName;

                if (actionMap.Contains(actionName))
                {
                    // Add keys to route data, so they will bind to action parameters.
                    KeyValuePathSegment keyValueSegment = odataPath.Segments[1] as KeyValuePathSegment;
                    context.RouteData.Values[ODataRouteConstants.Key] = keyValueSegment.Value;

                    KeyValuePathSegment relatedKeySegment = odataPath.Segments[3] as KeyValuePathSegment;
                    context.RouteData.Values[ODataRouteConstants.RelatedKey] = relatedKeySegment.Value;

                    return actionName;
                }
            }

            if (context.Request.Method == HttpMethod.Put &&
                odataPath.PathTemplate == "~/entityset/key/navigation")
            {
                string navigationPropertyName = (odataPath.Segments[2] as NavigationPathSegment).NavigationPropertyName;
                string actionName = "Update" + navigationPropertyName;

                if (actionMap.Contains(actionName))
                {
                    // Add keys to route data, so they will bind to action parameters.
                    KeyValuePathSegment keyValueSegment = odataPath.Segments[1] as KeyValuePathSegment;
                    context.RouteData.Values[ODataRouteConstants.Key] = keyValueSegment.Value;

                    return actionName;
                }
            }

            if (context.Request.Method == HttpMethod.Post &&
                odataPath.PathTemplate == "~/entityset/key/navigation")
            {
                NavigationPathSegment segment = (odataPath.Segments[2] as NavigationPathSegment);
                string navigationPropertyName = segment.NavigationPropertyName;
                string actionPrefix = "Create";

                var type = segment.NavigationProperty.Type.Definition.TypeKind;
                if (type == EdmTypeKind.Collection)
                    actionPrefix = "AddTo";

                string actionName = actionPrefix + navigationPropertyName;

                if (actionMap.Contains(actionName))
                {
                    // Add keys to route data, so they will bind to action parameters.
                    KeyValuePathSegment keyValueSegment = odataPath.Segments[1] as KeyValuePathSegment;
                    context.RouteData.Values[ODataRouteConstants.Key] = keyValueSegment.Value;

                    return actionName;
                }
            }

            // Not a match.
            return null;
        }
    }

}