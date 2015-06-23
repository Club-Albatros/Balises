using Albatros.DNN.Modules.Balises.Common.Settings;
using DotNetNuke.Web.Api;

namespace Albatros.DNN.Modules.Balises.Common
{
    public class BalisesApiController : DnnApiController
    {
        private ContextSecurity _security;
        public ContextSecurity Security
        {
            get { return _security ?? (_security = new ContextSecurity(ActiveModule)); }
        }

        private ModuleSettings _settings;
        public ModuleSettings Settings
        {
            get { return _settings ?? (_settings = ModuleSettings.GetSettings(ActiveModule)); }
        }

    }
}