using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Albatros.DNN.Modules.Balises.Common;
using System.Collections.Generic;
using Albatros.Balises.Core.Repositories;
using Albatros.Balises.Core.Common;
using System.Globalization;

namespace Albatros.DNN.Modules.Balises.Api
{

    public struct Site
    {
        public string label { get; set; }
        public string value { get; set; }
        public string coords { get; set; }
    }

    public partial class SitesController : BalisesApiController
    {

        [HttpGet]
        [BalisesAuthorize(SecurityLevel = SecurityAccessLevel.Pilot)]
        public HttpResponseMessage Takeoffs(string SearchString)
        {
            var res = SitesRepository.Instance.GetTakeoffSiteList(PortalSettings.PortalId).Values.Where(s => s.Name.ToLowerInvariant().Contains(SearchString.ToLowerInvariant())).Select(s => new Site() { label = s.Name, value = s.Name, coords = s.Coords });
            return Request.CreateResponse(HttpStatusCode.OK, res);
        }

        [HttpGet]
        [BalisesAuthorize(SecurityLevel = SecurityAccessLevel.Pilot)]
        public HttpResponseMessage Landings(string SearchString)
        {
            var res = SitesRepository.Instance.GetLandingSiteList(PortalSettings.PortalId).Values.Where(s => s.Name.ToLowerInvariant().Contains(SearchString.ToLowerInvariant())).Select(s => new Site() { label = s.Name, value = s.Name, coords = s.Coords });
            return Request.CreateResponse(HttpStatusCode.OK, res);
        }

        [HttpGet]
        [BalisesAuthorize(SecurityLevel = SecurityAccessLevel.Pilot)]
        public HttpResponseMessage ClosestTakeoff(string Coords)
        {
            var point = Coords.ToPoint();
            if (point != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, SitesRepository.Instance.GetClosestTakeoffSites(PortalSettings.PortalId, point.Latitude, point.Longitude, BalisesModuleContext.Settings.BeaconPassDistanceMeters));
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, new List<Albatros.Balises.Core.Models.Sites.Site>());
            }
        }

        [HttpGet]
        [BalisesAuthorize(SecurityLevel = SecurityAccessLevel.Pilot)]
        public HttpResponseMessage ClosestLanding(string Coords)
        {
            var point = Coords.ToPoint();
            if (point != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, SitesRepository.Instance.GetClosestLandingSites(PortalSettings.PortalId, point.Latitude, point.Longitude, BalisesModuleContext.Settings.BeaconPassDistanceMeters));
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK, new List<Albatros.Balises.Core.Models.Sites.Site>());
            }
        }

    }
}

