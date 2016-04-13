using DotNetNuke.Web.Mvc.Framework.Controllers;
using DotNetNuke.Web.Mvc.Routing;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web.Mvc;
using System.Web.Routing;

namespace Albatros.DNN.Modules.Balises.Common
{
    public class BalisesMvcController : DnnController
    {

        private ContextHelper _balisesModuleContext;
        public ContextHelper BalisesModuleContext
        {
            get { return _balisesModuleContext ?? (_balisesModuleContext = new ContextHelper(this)); }
        }

        public string GetRouteParameter()
        {
            if (ControllerContext.HttpContext.Request.Params["ret"] == null)
            {
                return "";
            }
            else
            {
                return ControllerContext.HttpContext.Request.Params["ret"];
            }
        }

        public ActionResult ReturnRoute(int? id, ActionResult defaultRoute)
        {
            RouteValueDictionary routeValues = new RouteValueDictionary();
            switch (GetRouteParameter())
            {
                case "p-v":
                    routeValues["controller"] = "Pilot";
                    routeValues["action"] = "View";
                    routeValues.Add("UserId", id);
                    return Redirect(ModuleRoutingProvider.Instance().GenerateUrl(routeValues, ModuleContext));
                case "home":
                    return RedirectToDefaultRoute();
            }
            return defaultRoute;
        }

        public ActionResult RedirectToAction(string action, string controller, IDictionary<string,string> routeValues)
        {
            RouteValueDictionary newRouteValues = new RouteValueDictionary();
            newRouteValues["controller"] = controller;
            newRouteValues["action"] = action;
            foreach(var p in routeValues)
            {
                newRouteValues[p.Key] = p.Value;
            }
            return Redirect(ModuleRoutingProvider.Instance().GenerateUrl(newRouteValues, ModuleContext));
        }

    }
}