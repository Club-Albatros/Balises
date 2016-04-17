using System.Net;
using System.Net.Http;
using System.Web.Http;
using Albatros.DNN.Modules.Balises.Common;
using System.Net.Http.Headers;

namespace Albatros.DNN.Modules.Balises.Api
{

    public partial class BeaconsController : BalisesApiController
	{

		[HttpGet()]
		[BalisesAuthorize(SecurityLevel = SecurityAccessLevel.Pilot)]
		public HttpResponseMessage CompeGps()
		{
            var wptFile = new CompeGpsFile(ActiveModule.PortalID);
            var res = new HttpResponseMessage(HttpStatusCode.OK);
            res.Content = new StringContent(wptFile.Content);
            res.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            res.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
            res.Content.Headers.ContentDisposition.FileName = "Balises.wpt";
            return res;
		}

	}
}

