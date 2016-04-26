using Microsoft.OData.Edm;
using System.Linq;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.OData.Routing;
using System.Web.OData.Routing.Conventions;

namespace TF.Web.API.OData
{
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
