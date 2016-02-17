using System.Web.Mvc;
using DotNetNuke.Common;
using Albatros.DNN.Modules.Balises.Common;
using Albatros.Balises.Core.Repositories;
using DotNetNuke.Services.FileSystem;
using DotNetNuke.Entities.Users;

namespace Albatros.DNN.Modules.Balises.Controllers
{
    public class PilotController : BalisesMvcController
    {

        [HttpGet]
        [BalisesAuthorize(SecurityLevel = SecurityAccessLevel.View)]
        public ActionResult View(int id)
        {
            return View();
        }

        [HttpGet]
        [BalisesAuthorize(SecurityLevel = SecurityAccessLevel.View)]
        public ActionResult Log()
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