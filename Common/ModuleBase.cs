using Albatros.DNN.Modules.Balises.Common.Settings;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Framework;
using DotNetNuke.Framework.JavaScriptLibraries;
using DotNetNuke.Web.Client.ClientResourceManagement;

namespace Albatros.DNN.Modules.Balises.Common
{
    public class ModuleBase : PortalModuleBase
    {

        #region Properties
        private ContextSecurity _security;
        public ContextSecurity Security
        {
            get { return _security ?? (_security = new ContextSecurity(ModuleContext.Configuration)); }
        }

        private ModuleSettings _settings;
        public new ModuleSettings Settings
        {
            get { return _settings ?? (_settings = ModuleSettings.GetSettings(ModuleContext.Configuration)); }
        }
        #endregion

        #region Public Methods
        public void AddService()
        {
            if (Context.Items["BalisesServiceAdded"] == null)
            {
                JavaScript.RequestRegistration(CommonJs.DnnPlugins);
                ServicesFramework.Instance.RequestAjaxScriptSupport();
                ServicesFramework.Instance.RequestAjaxAntiForgerySupport();
                AddJavascriptFile("Albatros.Balises.js", 70);
                string script = "(function($){$(document).ready(function(){ moduleBalisesService = new ModuleBalisesService($, {}, " + ModuleContext.ModuleId + ") })})(jQuery);";
                Page.ClientScript.RegisterClientScriptBlock(script.GetType(), ID + "_service", script, true);
                Context.Items["BalisesServiceAdded"] = true;
            }

        }

        public void AddGoogleMaps()
        {
            if (Context.Items["GoogleMapsAdded"] == null)
            {
                ClientResourceManager.RegisterScript(Page, "http://maps.googleapis.com/maps/api/js", 70);
                Context.Items["GoogleMapsAdded"] = true;
            }

        }

        public void AddJavascriptFile(string jsFilename, int priority)
        {
            ClientResourceManager.RegisterScript(Page, ResolveUrl("~/DesktopModules/Albatros/Balises/js/" + jsFilename) + "?_=" + Settings.Version, priority);
        }

        public void AddCssFile(string cssFileName)
        {
            ClientResourceManager.RegisterStyleSheet(Page, ResolveUrl("~/DesktopModules/Albatros/Balises/css/" + cssFileName) + "?_=" + Settings.Version);
        }
        #endregion

    }
}