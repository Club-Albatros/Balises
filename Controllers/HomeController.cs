using System.Web.Mvc;
using Albatros.DNN.Modules.Balises.Common;

namespace Albatros.DNN.Modules.Balises.Controllers
{
    public class HomeController: BalisesMvcController
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View(BalisesModuleContext.Settings.View);
        }
    }
}