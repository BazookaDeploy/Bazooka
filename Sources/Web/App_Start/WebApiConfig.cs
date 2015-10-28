using System.Net.Http;
using System.Web.Http;

namespace Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            GlobalConfiguration.Configuration.Routes.MapHttpRoute(
                    name: "DefaultApi",
                    routeTemplate: "api/{controller}/{action}/{id}",
                    defaults: new { id = RouteParameter.Optional },
                    constraints: new { action = @"^\d*[a-zA-Z][a-zA-Z0-9]*$" });

            GlobalConfiguration.Configuration.Routes.MapHttpRoute(
                 name: "DefaultApiGet",
                 routeTemplate: "api/{controller}/{id}",
                 defaults: new { action = "Get", id = RouteParameter.Optional },
                 constraints: new { httpMethod = new System.Web.Http.Routing.HttpMethodConstraint(HttpMethod.Get) });

            GlobalConfiguration.Configuration.Routes.MapHttpRoute(
                name: "DefaultApiPost",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { action = "Post", id = RouteParameter.Optional },
                constraints: new { httpMethod = new System.Web.Http.Routing.HttpMethodConstraint(HttpMethod.Post) });

            GlobalConfiguration.Configuration.Routes.MapHttpRoute(
                name: "DefaultApiPut",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { action = "Put", id = RouteParameter.Optional },
                constraints: new { httpMethod = new System.Web.Http.Routing.HttpMethodConstraint(HttpMethod.Put) });

            GlobalConfiguration.Configuration.Routes.MapHttpRoute(
                name: "DefaultApiDelete",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { action = "Delete", id = RouteParameter.Optional },
                constraints: new { httpMethod = new System.Web.Http.Routing.HttpMethodConstraint(HttpMethod.Delete) });
            
        }
    }
}
