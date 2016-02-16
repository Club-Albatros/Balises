using System.Web.Mvc;
using DotNetNuke.Common;
using Albatros.DNN.Modules.Balises.Common;
using Albatros.Balises.Core.Repositories;
using DotNetNuke.Services.FileSystem;
using DotNetNuke.Entities.Users;

namespace Albatros.DNN.Modules.Balises.Controllers
{
    public class VerifierController : BalisesMvcController
    {

        [HttpGet]
        [BalisesAuthorize(SecurityLevel = SecurityAccessLevel.Verifier)]
        public ActionResult FlightList()
        {
            return View();
        }

    }
}