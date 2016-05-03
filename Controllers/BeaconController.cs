using System.Web.Mvc;
using DotNetNuke.Common;
using Albatros.DNN.Modules.Balises.Common;
using Albatros.Balises.Core.Repositories;
using Albatros.Balises.Core.Models.Flights;
using Albatros.Balises.Core.Common;
using System.Collections.Generic;

namespace Albatros.DNN.Modules.Balises.Controllers
{
    public class BeaconController : BalisesMvcController
    {
        private readonly IBeaconRepository _repository;

        public BeaconController() : this(BeaconRepository.Instance) { }

        public BeaconController(IBeaconRepository repository)
        {
            Requires.NotNull(repository);
            _repository = repository;
        }

        [HttpGet]
        [BalisesAuthorize(SecurityLevel = SecurityAccessLevel.View)]
        public ActionResult Map()
        {
            return View(_repository.GetBeacons(PortalSettings.PortalId));
        }

    }
}