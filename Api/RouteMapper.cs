using DotNetNuke.Web.Api;

namespace Albatros.DNN.Modules.Balises.Api
{
    public class RouteMapper : IServiceRouteMapper
    {
        public void RegisterRoutes(IMapRoute mapRouteManager)
        {
            mapRouteManager.MapHttpRoute("Albatros/Balises", "BalisesMap1", "{controller}/{action}", null, null, new[] { "Albatros.DNN.Modules.Balises.Api" });
            mapRouteManager.MapHttpRoute("Albatros/Balises", "BalisesMap2", "{controller}/{action}/{id}", null, new { id = "-?\\d+" }, new[] { "Albatros.DNN.Modules.Balises.Api" });
        }
    }
}