using System.Web.Mvc;
using Albatros.DNN.Modules.Balises.Common;

namespace Albatros.DNN.Modules.Balises.Controllers
{
    public class PilotController : BalisesMvcController
    {

        [HttpGet]
        [BalisesAuthorize(SecurityLevel = SecurityAccessLevel.View)]
        public ActionResult View(int UserId)
        {
            return View();
        }

        [HttpGet]
        [BalisesAuthorize(SecurityLevel = SecurityAccessLevel.View)]
        public ActionResult Rankings()
        {
            return View();
        }

    }
}