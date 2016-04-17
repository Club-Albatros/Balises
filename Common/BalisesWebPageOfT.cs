using System.Web.Mvc;
using DotNetNuke.Web.Mvc.Helpers;
using DotNetNuke.Web.Mvc.Framework;
using System.Web;
using Albatros.Balises.Core.Common;

namespace Albatros.DNN.Modules.Balises.Common
{
    public abstract class BalisesWebPage<TModel> : DnnWebViewPage<TModel>
    {

        public ContextHelper BalisesModuleContext { get; set; }

        public override void InitHelpers()
        {
            Ajax = new AjaxHelper<TModel>(ViewContext, this);
            Html = new DnnHtmlHelper<TModel>(ViewContext, this);
            Url = new DnnUrlHelper(ViewContext);
            Dnn = new DnnHelper<TModel>(ViewContext, this);
            BalisesModuleContext = new ContextHelper(ViewContext);
        }

        public string SerializedResources()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(DotNetNuke.Services.Localization.LocalizationProvider.Instance.GetCompiledResourceFile(Dnn.PortalSettings, "/DesktopModules/MVC/Albatros/Balises/App_LocalResources/ClientResources.resx",
                    System.Threading.Thread.CurrentThread.CurrentCulture.Name));
        }

        public string Locale
        {
            get
            {
                return System.Threading.Thread.CurrentThread.CurrentCulture.Name;
            }
        }

        public string GetModuleUrl(string component, string relativeUrl)
        {
            if (component.StartsWith("API/"))
            {
                relativeUrl += relativeUrl.Contains("?") ? "&" : "?";
                relativeUrl += string.Format("moduleId={0}&tabId={1}", Dnn.ActiveModule.ModuleID, Dnn.ActiveModule.TabID);
                return Dnn.PortalSettings.PortalAlias.ResolveUrl("~/DesktopModules/FormaMed/" + component + "/" + relativeUrl);
            }
            else
            {
                return DotNetNuke.Common.Globals.ResolveUrl("~/DesktopModules/FormaMed/" + component + "/" + relativeUrl);
            }
        }

    }
}