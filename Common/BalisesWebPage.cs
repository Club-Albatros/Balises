using System.Web.WebPages;
using Albatros.DNN.Modules.Balises.Common.Settings;
using DotNetNuke.Web.Razor;
using DotNetNuke.Web.Razor.Helpers;

namespace Albatros.DNN.Modules.Balises.Common
{
    public abstract class BalisesWebPage : DotNetNukeWebPage
    {
        private ContextSecurity _security;
        public ContextSecurity Security
        {
            get { return _security ?? (_security = new ContextSecurity(Dnn.Module)); }
        }

        private ModuleSettings _settings;
        public ModuleSettings Settings
        {
            get { return _settings ?? (_settings = ModuleSettings.GetSettings(Dnn.Module)); }
        }

        public new DnnHelper Dnn
        {
            get
            {
                return base.Dnn;
            }
        }
        public new HtmlHelper Html
        {
            get
            {
                return base.Html;
            }
        }
        public new UrlHelper Url
        {
            get
            {
                return base.Url;
            }
        }
    }
    public abstract class BalisesWebPage<T> : DotNetNukeWebPage<T>
    {
        private ContextSecurity _security;
        public ContextSecurity Security
        {
            get { return _security ?? (_security = new ContextSecurity(Dnn.Module)); }
        }

        private ModuleSettings _settings;
        public ModuleSettings Settings
        {
            get { return _settings ?? (_settings = ModuleSettings.GetSettings(Dnn.Module)); }
        }

        protected override void ConfigurePage(WebPageBase parentPage)
        {
            base.ConfigurePage(parentPage);
            Context = parentPage.Context;
            BalisesWebPage parent = (BalisesWebPage)parentPage;
            _dnn = parent.Dnn;
            _html = parent.Html;
            _url = parent.Url;
        }
        private DnnHelper _dnn;
        public new DnnHelper Dnn
        {
            get
            {
                if (base.Dnn == null)
                {
                    return _dnn;
                }
                else
                {
                    return base.Dnn;
                }
            }
        }

        private HtmlHelper _html;
        public new HtmlHelper Html
        {
            get
            {
                if (base.Html == null)
                {
                    return _html;
                }
                else
                {
                    return base.Html;
                }
            }
        }

        private UrlHelper _url;
        public new UrlHelper Url
        {
            get
            {
                if (base.Url == null)
                {
                    return _url;
                }
                else
                {
                    return base.Url;
                }
            }
        }

    }
}