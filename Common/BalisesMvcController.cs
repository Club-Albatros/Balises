using DotNetNuke.Web.Mvc.Framework.Controllers;
using DotNetNuke.Web.Mvc.Routing;
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

    }
}