using Albatros.DNN.Modules.Balises.Common.Settings;
using DotNetNuke.Framework;
using DotNetNuke.Framework.JavaScriptLibraries;
using DotNetNuke.Web.Client.ClientResourceManagement;
using DotNetNuke.Web.Razor;

namespace Albatros.DNN.Modules.Balises.Common
{
    public class ModuleBase : RazorModuleBase
    {

        #region Properties
        private ContextSecurity _security;
        public ContextSecurity Security
        {
            get { return _security ?? (_security = new ContextSecurity(ModuleContext.Configuration)); }
        }

        private ModuleSettings _settings;
        public ModuleSettings Settings
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