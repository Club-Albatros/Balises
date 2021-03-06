using System.Web.Mvc;
using DotNetNuke.Web.Mvc.Helpers;
using DotNetNuke.Web.Mvc.Framework;
using System.Web;
using Albatros.Balises.Core.Common;

namespace Albatros.DNN.Modules.Balises.Common
{
    public abstract class BalisesWebPage : DnnWebViewPage
    {

        public ContextHelper BalisesModuleContext { get; set; }

        public override void InitHelpers()
        {
            Ajax = new AjaxHelper<object>(ViewContext, this);
            Html = new DnnHtmlHelper<object>(ViewContext, this);
            Url = new DnnUrlHelper(ViewContext);
            Dnn = new DnnHelper<object>(ViewContext, this);
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

        public string GetModuleUrl(string relativeUrl, bool addModTabIds)
        {
            if (addModTabIds)
            {
                relativeUrl += relativeUrl.Contains("?") ? "&" : "?";
                relativeUrl += string.Format("moduleId={0}&tabId={1}", Dnn.ActiveModule.ModuleID, Dnn.ActiveModule.TabID);

            }
            if (relativeUrl.StartsWith("API/"))
            {

                return Dnn.PortalSettings.PortalAlias.ResolveUrl("~/DesktopModules/Albatros/Balises/" + relativeUrl);
            }
            else
            {
                return DotNetNuke.Common.Globals.ResolveUrl("~/DesktopModules/Albatros/Balises/" + relativeUrl);
            }
        }

    }
}