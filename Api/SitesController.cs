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
            var res = SitesRepository.Instance.GetTakeoffSiteList(PortalSettings.PortalId).Values.Where(s => s.Name.ToLowerInvariant().Contains(SearchString.ToLowerInvariant())).Select(s => new Site() { label = s.Name, value = s.Name, coords = SwissProjection.ToSwissCoordinates(s.Latitude, s.Longitude) });
            return Request.CreateResponse(HttpStatusCode.OK, res);
        }

        [HttpGet]
        [BalisesAuthorize(SecurityLevel = SecurityAccessLevel.Pilot)]
        public HttpResponseMessage Landings(string SearchString)
        {
            var res = SitesRepository.Instance.GetLandingSiteList(PortalSettings.PortalId).Values.Where(s => s.Name.ToLowerInvariant().Contains(SearchString.ToLowerInvariant())).Select(s => new Site() { label = s.Name, value = s.Name, coords = SwissProjection.ToSwissCoordinates(s.Latitude, s.Longitude) });
            return Request.CreateResponse(HttpStatusCode.OK, res);
        }

    }
}

