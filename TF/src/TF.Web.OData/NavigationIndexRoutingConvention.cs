using Microsoft.OData.Edm;
using System.Linq;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.OData.Routing;
using System.Web.OData.Routing.Conventions;

namespace TF.Web.OData
{
    internal class NavigationIndexRoutingConvention : EntitySetRoutingConvention
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